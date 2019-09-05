using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Models;
using Teste.Services.Api;

namespace Teste.Repositories.Moedas
{
    public sealed class MoedaRepository : IMoedaRepository
    {
        private readonly IAPIService _api;

        public MoedaRepository(IAPIService api)
        {
            _api = api;
        }

        public async Task<IEnumerable<Moeda>> GetMoedasAsync()
        {
            var result = await _api.GetMoedas();
            return result.Data;
        }
    }
}
