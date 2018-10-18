using CopaFilmes.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace CopaFilmes.ViewModels
{
    public class ResultadoPageViewModel : ViewModelBase
    {

        private ObservableCollection<Filmes> filmes;
        public ObservableCollection<Filmes> Filmes
        {
            get { return filmes; }
            set { SetProperty(ref filmes, value); }
        }

        private List<Filmes> temp;

        public ResultadoPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Filmes = new ObservableCollection<Filmes>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            Filmes = (ObservableCollection<Filmes>)parameters["filmes"];

            CompareMovies();
        }

        void CompareMovies()
        {
            temp = new List<Filmes>();
            var qtd = Filmes.Count - 1;

            for (int i = 0; i <= (Filmes.Count / 2) - 1; i++)
            {
                if (Filmes[i].nota > Filmes[qtd - i].nota)
                {
                    temp.Add(Filmes[i]);
                }
                else if (Filmes[i].nota < Filmes[qtd - i].nota)
                {
                    temp.Add(Filmes[qtd - i]);
                }
                else
                {
                    if (string.Compare(Filmes[i].titulo, Filmes[qtd - i].titulo) == -1)
                        temp.Add(Filmes[i]);
                    if (string.Compare(Filmes[i].titulo, Filmes[qtd - i].titulo) == 1)
                        temp.Add(Filmes[qtd - 1]);
                }
            }
        }
    }
}
