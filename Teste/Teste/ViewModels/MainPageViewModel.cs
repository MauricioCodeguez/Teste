using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Teste.Extensions;
using Teste.Models;
using Teste.Repositories.Cotacoes;

namespace Teste.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly IPageDialogService _pageDialog;

        public ObservableCollection<Cotacao> ListaCotacao { get; private set; }

        private Cotacao cotacaoSelecionada;
        public Cotacao CotacaoSelecionada
        {
            get => cotacaoSelecionada;
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

        public MainPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialog,
            ICotacaoRepository cotacaoRepository)
            : base(navigationService)
        {
            Title = "Minhas Cotações";
            NovaCotacaoCommand = new DelegateCommand(async () => await NovaCotacao());
            EditarCommand = new DelegateCommand<Cotacao>(async (a) => await Editar(a));
            ExcluirCommand = new DelegateCommand<Cotacao>(async (a) => await Excluir(a));
            _cotacaoRepository = cotacaoRepository;
            _pageDialog = pageDialog;
            ListaCotacao = new ObservableCollection<Cotacao>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var listaCotacao = _cotacaoRepository.GetAllCotacao().ToObservable();

            foreach (var item in listaCotacao)
                ListaCotacao.Add(item);
        }

        private async Task NovaCotacao() => await NavigationService.NavigateAsync("AdicionarCotacaoView");

        private async Task Editar(Cotacao cot) => await NavigationService.NavigateAsync($"AdicionarCotacaoView?cotacao={cot.CodigoCotacao}");

        private async Task Excluir(Cotacao cot)
        {
            if (cot != null)
            {
                var resp = await _pageDialog.DisplayAlertAsync("Excluir", "Deseja excluir o item?", "Sim", "Não");

                if (resp)
                {
                    _cotacaoRepository.DeletarCotacao(cot);
                    ListaCotacao.Remove(cot);
                }
            }
        }
    }
}