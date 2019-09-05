using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Models;

namespace Teste.Repositories.Moedas
{
    public interface IMoedaRepository
    {
        Task<IEnumerable<Moeda>> GetMoedasAsync();
    }
}