using System.Threading.Tasks;
using Teste.Classes;
using Teste.Models;

namespace Teste.Services
{
    public interface IAPIService
    {
        Task<ServiceResult<Moeda>> GetMoedas();
    }
}