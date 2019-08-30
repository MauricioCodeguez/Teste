using System;
using System.Threading.Tasks;
using Teste.Classes;
using Teste.Models;
using Teste.Services.Request;

namespace Teste.Services
{
    public class APIService : IAPIService
    {
        private readonly IRequestService _requestService;

        public APIService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<ServiceResult<Moeda>> GetMoedas()
        {
            var builder = new UriBuilder(AppSettings.EndPoint);
            var uri = builder.ToString();

            return await _requestService.GetAsync<ServiceResult<Moeda>>(uri);
        }
    }
}