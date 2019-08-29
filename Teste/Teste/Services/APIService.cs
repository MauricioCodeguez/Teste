using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Teste.Classes;
using Teste.Models;
using Xamarin.Essentials;

namespace Teste.Services
{
    public class APIService : IAPIService
    {
        private readonly string _endPoint;

        public APIService()
        {
            _endPoint = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json";
        }

        public async Task<ServiceResult<Moeda>> GetMoedas()
        {
            ServiceResult<Moeda> retorno = new ServiceResult<Moeda>();

            var network = Connectivity.NetworkAccess;

            if (network == NetworkAccess.Internet)
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri(_endPoint));

                    if (response.IsSuccessStatusCode)
                    {
                        retorno = JsonConvert.DeserializeObject<ServiceResult<Moeda>>(await response.Content.ReadAsStringAsync());
                        retorno.Status = Enum.ServiceResultStatus.Ok;
                    }
                }
            }
            else
                retorno.Status = Enum.ServiceResultStatus.SemConexao;

            return retorno;
        }
    }
}