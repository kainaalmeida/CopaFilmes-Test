using Xamarin.Essentials;

namespace CopaFilmes.Utils
{
    public static class VerifyConnection
    {
        public static bool IsConnected { get; set; }

        public static bool GetConnectionStatus()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
                return false;
            return true;
        }
    }
}
