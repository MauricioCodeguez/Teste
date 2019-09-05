using Xamarin.Essentials;

namespace Teste
{
    public static class AppSettings
    {
        const string root = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json";

        static AppSettings()
        {
        }

        public static string EndPoint
        {
            get => root;
        }
    }
}