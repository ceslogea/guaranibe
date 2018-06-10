using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICompanyService
    {
        Task<Company> Add(Company newCompany);
        IEnumerable<Company> GetAll();
        Company GetNoLazyLoad(string id);
        IEnumerable<Company> GetAllNoLazyLoad();
    }
}
