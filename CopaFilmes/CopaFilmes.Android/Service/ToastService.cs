using Android.Widget;
using CopaFilmes.Droid.Service;
using CopaFilmes.Services.Abstract;

[assembly: Xamarin.Forms.Dependency(typeof(ToastService))]
namespace CopaFilmes.Droid.Service
{
    public class ToastService : IToastService
    {
        public void DisplayMessage(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}