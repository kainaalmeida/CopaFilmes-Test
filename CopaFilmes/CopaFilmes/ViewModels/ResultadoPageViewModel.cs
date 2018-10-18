using CopaFilmes.Models;
using CopaFilmes.Utils;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        private Filmes firstMovie;
        public Filmes FirstMovie
        {
            get { return firstMovie; }
            set { SetProperty(ref firstMovie, value); }
        }

        private Filmes secondMovie; 
        public Filmes SecondMovie
        {
            get { return secondMovie; }
            set { SetProperty(ref secondMovie, value); }
        }


        public ResultadoPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Filmes = new ObservableCollection<Filmes>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            Filmes = (ObservableCollection<Filmes>)parameters["filmes"];

            Played01();
        }

        void Played01()
        {
            var firstMoved = new List<Filmes>();
            var qtd = Filmes.Count - 1;

            for (int i = 0; i <= (Filmes.Count / 2) - 1; i++)
            {
                if (Filmes[i].nota > Filmes[qtd - i].nota)
                {
                    firstMoved.Add(Filmes[i]);
                }
                else if (Filmes[i].nota < Filmes[qtd - i].nota)
                {
                    firstMoved.Add(Filmes[qtd - i]);
                }
                else
                {
                    if (string.Compare(Filmes[i].titulo, Filmes[qtd - i].titulo) == -1)
                        firstMoved.Add(Filmes[i]);
                    if (string.Compare(Filmes[i].titulo, Filmes[qtd - i].titulo) == 1)
                        firstMoved.Add(Filmes[qtd - 1]);
                }
            }

            Played02(firstMoved);
        }

        void Played02(List<Filmes> temp)
        {
            var secondMoved = new List<Filmes>();

            for (int i = 0; i <= (temp.Count / 2); i += 2)
            {
                if (temp[i].nota > temp[i + 1].nota)
                {
                    secondMoved.Add(temp[i]);
                }
                else if (temp[i].nota < temp[i + 1].nota)
                {
                    secondMoved.Add(temp[i + 1]);
                }
                else
                {
                    if (string.Compare(temp[i].titulo, temp[i + 1].titulo) == -1)
                        secondMoved.Add(temp[i]);
                    if (string.Compare(temp[i].titulo, temp[i + 1].titulo) == 1)
                        secondMoved.Add(temp[i + 1]);
                }
            }

            Played03(secondMoved);
        }

        void Played03(List<Filmes> temp)
        {
            Filmes.Clear();
            if (temp[0].nota > temp[1].nota)
            {
                FirstMovie = temp[0];
                SecondMovie = temp[1];
            }
            else if (temp[0].nota < temp[1].nota)
            {
                FirstMovie = temp[1];
                SecondMovie = temp[0];
            }
            else
            {
                if (string.Compare(temp[0].titulo, temp[1].titulo) == -1)
                {
                    FirstMovie = temp[0];
                    SecondMovie = temp[1];
                }
                if (string.Compare(temp[0].titulo, temp[1].titulo) == 1)
                {
                    FirstMovie = temp[1];
                    SecondMovie = temp[0];
                }
            }
        }
    }
}
