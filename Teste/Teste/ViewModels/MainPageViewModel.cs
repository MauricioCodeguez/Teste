using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Teste.Models;
using Teste.Services;

namespace Teste.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IAPIService _api;

        public ObservableCollection<Moeda> ListaMoedas { get; private set; }

        public MainPageViewModel(INavigationService navigationService, IAPIService api)
            : base(navigationService)
        {
            Title = "Main Page";
            _api = api;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var result = await _api.GetMoedas();

            if (result != null)
                ListaMoedas = new ObservableCollection<Moeda>(result.Data);
        }
    }
}