using CopaFilmes.Models;
using CopaFilmes.Services.Abstract;
using CopaFilmes.Utils;
using CopaFilmes.Views;
using Polly;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;

namespace CopaFilmes.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        private ObservableCollection<Filmes> filmes;
        public ObservableCollection<Filmes> Filmes
        {
            get { return filmes; }
            set { SetProperty(ref filmes, value); }
        }

        private string filmesSelecionados;
        public string FilmesSelecionados
        {
            get { return filmesSelecionados; }
            set { SetProperty(ref filmesSelecionados, value); }
        }


        public DelegateCommand<Filmes> DeleteCmd { get; set; }
        public DelegateCommand GerarCmd { get; set; }

        IService _service;
        IPageDialogService _pageDialogService;
        public MainPageViewModel(INavigationService navigationService, IService service, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            _service = service;
            Filmes = new ObservableCollection<Filmes>();
            _pageDialogService = pageDialogService;


            DeleteCmd = new DelegateCommand<Filmes>(ExecuteDeleteCmd);
            GerarCmd = new DelegateCommand(ExecuteGerarCmd);
        }

        private async void ExecuteGerarCmd()
        {
            var parametros = new NavigationParameters();
            parametros.Add("filmes", Filmes);
            await NavigationService.NavigateAsync($"{nameof(ResultadoPage)}", parametros);
        }

        private async void ExecuteDeleteCmd(Filmes obj)
        {
            if (Filmes.Count > 8)
            {
                Filmes.Remove(obj);
                SetMessage(Filmes.Count);
            }
            else
                await _pageDialogService.DisplayAlertAsync("Aviso!", "Você atingiu o número máximo de filmes removidos.", "OK");
        }

        void SetMessage(int qtdFilmesSelecionados)
        {
            FilmesSelecionados = $"Selecionados {qtdFilmesSelecionados} de 8 Filmes";
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            if (!VerifyConnection.GetConnectionStatus())
                Xamarin.Forms.DependencyService.Get<IToastService>().DisplayMessage("Sem conexão.");

            this.IsBusy = true;

            await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryForeverAsync
                (
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
                .ExecuteAsync(async () => await _service.GetFilmes())
                .ContinueWith(t =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Filmes.Clear();
                        Filmes.AddRange(t.Result);

                        SetMessage(Filmes.Count);
                        this.IsBusy = false;
                    });
                });
        }
    }
}
