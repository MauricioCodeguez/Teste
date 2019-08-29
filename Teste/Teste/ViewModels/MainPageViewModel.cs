using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Models;
using Teste.Repositories;

namespace Teste.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly IPageDialogService _pageDialog;

        private List<Cotacao> listaCotacao;
        public List<Cotacao> ListaCotacao
        {
            get { return listaCotacao; }
            set { SetProperty(ref listaCotacao, value); }
        }

        private Cotacao cotacaoSelecionada;
        public Cotacao CotacaoSelecionada
        {
            get { return cotacaoSelecionada; }
            set
            {
                if (SetProperty(ref cotacaoSelecionada, value))
                {
                    Editar(cotacaoSelecionada);
                    CotacaoSelecionada = null;
                }
            }
        }

        public DelegateCommand NovaCotacaoCommand { get; private set; }
        public DelegateCommand<Cotacao> EditarCommand { get; private set; }
        public DelegateCommand<Cotacao> ExcluirCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService pageDialog,
            ICotacaoRepository cotacaoRepository)
            : base(navigationService)
        {
            Title = "Minhas Cotações";
            NovaCotacaoCommand = new DelegateCommand(NovaCotacao);
            EditarCommand = new DelegateCommand<Cotacao>(async (a) => await Editar(a));
            ExcluirCommand = new DelegateCommand<Cotacao>(async (a) => await Excluir(a));
            _cotacaoRepository = cotacaoRepository;
            _pageDialog = pageDialog;
            listaCotacao = new List<Cotacao>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            IsBusy = true;
            ListaCotacao = _cotacaoRepository.GetAllCotacao().ToList();
            IsBusy = false;
        }

        private void NovaCotacao() => NavigationService.NavigateAsync("AdicionarCotacaoView");

        private async Task Editar(Cotacao cot) => await NavigationService.NavigateAsync($"AdicionarCotacaoView?cotacao={cot.CodigoCotacao}");

        private async Task Excluir(Cotacao cot)
        {
            if (cot != null)
            {
                var resp = await _pageDialog.DisplayAlertAsync("Excluir", "Deseja excluir o item?", "Sim", "Não");

                if (resp)
                {
                    _cotacaoRepository.DeletarCotacao(cot);

                    if (ListaCotacao.Remove(cot))
                        ListaCotacao = _cotacaoRepository.GetAllCotacao().ToList();
                }
            }
        }
    }
}