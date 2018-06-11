using Models;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICoinTypeSerrvice
    {
        Task<string> Get();
        Task<RootCoin> GetCoins();
    }
}