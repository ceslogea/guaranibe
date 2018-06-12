using Models;
using System;
using System.Linq;

namespace Repository.Repos
{
    public interface ICompanyRepo : IBaseRepo<Company>
    {
        Company GetNoLazyLoad(Guid id);
        IQueryable<Company> GetAllNoLazyLoad();
    }
}