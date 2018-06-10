using Models;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICnpjService
    {
        Task<Company> GetCompanyByCnpj(string cnpj);
    }
}