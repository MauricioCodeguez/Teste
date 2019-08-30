using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Teste.Exceptions;
using Teste.Models;
using Teste.Repositories.Cotacoes;
using Teste.Services.Api;

namespace Teste.ViewModels
{
    public class AdicionarCotacaoViewModel : ViewModelBase
    {
        private readonly IAPIService _api;
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly IPageDialogService _dialogService;

        private Cotacao cotacao;

        private IEnumerable<Moeda> moedas;
        public IEnumerable<Moeda> Moedas
        {
            get => moedas;
            set => SetProperty(ref moedas, value);
        }

        private DateTime dataCotacao = DateTime.Now;
        public DateTime DataCotacao
        {
            get => dataCotacao;
            set => SetProperty(ref dataCotacao, value);
        }

        private decimal valorVenda;
        public decimal ValorVenda
        {
            get => valorVenda;
            set => SetProperty(ref valorVenda, value);
        }

        private decimal valorCompra;
        public decimal ValorCompra
        {
            get => valorCompra;
            set => SetProperty(ref valorCompra, value);
        }

        private Moeda moedaSelecionada;
        public Moeda MoedaSelecionada
        {
            get => moedaSelecionada;
            set => SetProperty(ref moedaSelecionada, value);
        }

        private bool excluirVisivel;
        public bool ExcluirVisivel
        {
            get => excluirVisivel;
            set => SetProperty(ref excluirVisivel, value);
        }

        public DelegateCommand AlterarMoedaCommand { get; private set; }
        public DelegateCommand ExcluirMoedaCommand { get; private set; }

        public AdicionarCotacaoViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IAPIService api,
            ICotacaoRepository cotacaoRepository)
            : base(navigationService)
        {
            Title = "Nova Cotação";
            _api = api;
            _dialogService = dialogService;
            _cotacaoRepository = cotacaoRepository;
            moedas = new List<Moeda>();
            moedaSelecionada = new Moeda();
            excluirVisivel = false;

            AlterarMoedaCommand = new DelegateCommand(async () => await Salvar());
            ExcluirMoedaCommand = new DelegateCommand(async () => await Excluir());
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (parameters.GetNavigationMode() == NavigationMode.New)
                {
                    var moedas = await _api.GetMoedas();
                    Moedas = moedas.Data.ToList();

                    if (parameters.Count > 0)
                    {
                        ExcluirVisivel = true;

                        var codigoCotacao = Convert.ToDecimal(parameters["cotacao"]);

                        cotacao = _cotacaoRepository.GetCotacao(codigoCotacao);

                        DataCotacao = cotacao.DataCotacao;
                        ValorCompra = cotacao.ValorCompra;
                        ValorVenda = cotacao.ValorVenda;
                        MoedaSelecionada = Moedas?.FirstOrDefault(a => a.Simbolo == cotacao.SimboloMoeda);
                    }
                }
            }
            catch (ConnectivityException cex)
            {
                Debug.WriteLine($"Connectivity Error: {cex}");
                await _dialogService.DisplayAlertAsync("Aviso Conexão", "Você está sem conexão para carregar o combo de Moedas.", "Ok");
            }
        }

        private async Task Excluir()
        {
            if (cotacao != null)
            {
                var resp = await _dialogService.DisplayAlertAsync("Excluir", "Deseja excluir o item?", "Sim", "Não");

                if (resp)
                {
                    _cotacaoRepository.DeletarCotacao(cotacao);
                    await NavigationService.GoBackAsync();
                }
            }
        }

        private async Task Salvar()
        {
            if (ValidarCampos())
            {
                bool voltar = false;

                if (cotacao == null)
                {
                    cotacao = new Cotacao()
                    {
                        DataCotacao = dataCotacao,
                        SimboloMoeda = moedaSelecionada.Simbolo,
                        NomeFormatado = moedaSelecionada.NomeFormatado,
                        ValorCompra = valorCompra,
                        ValorVenda = ValorVenda
                    };
                }
                else
                {
                    voltar = true;
                    cotacao.DataCotacao = dataCotacao;
                    cotacao.SimboloMoeda = moedaSelecionada.Simbolo;
                    cotacao.NomeFormatado = moedaSelecionada.NomeFormatado;
                    cotacao.ValorCompra = valorCompra;
                    cotacao.ValorVenda = ValorVenda;
                }

                if (_cotacaoRepository.Save(cotacao))
                {
                    await _dialogService.DisplayAlertAsync("Aviso", "Cotacao salva com sucesso", "OK");
                    LimparCampos();

                    if (voltar)
                        await NavigationService.GoBackAsync();
                }
            }
        }

        private bool ValidarCampos()
        {
            if (MoedaSelecionada == null)
            {
                _dialogService.DisplayAlertAsync("Atenção", "Selecione uma moeda", "OK");
                return false;
            }

            if (ValorCompra == 0 && ValorVenda == 0)
            {
                _dialogService.DisplayAlertAsync("Atenção", "Digite pelo menos um valor (Compra ou Venda)", "OK");
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            cotacao = null;
            DataCotacao = DateTime.Now;
            MoedaSelecionada = null;
            ValorVenda = 0;
            ValorCompra = 0;
        }
    }
}
