using System.Threading.Tasks;
using Teste.Classes;
using Teste.Models;

namespace Teste.Services.Api
{
    public interface IAPIService
    {
        Task<ServiceResult<Moeda>> GetMoedas();
    }
}