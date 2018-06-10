using Models;
using Repository;
using Repository.Context;
using Repository.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepo _repo;

        public CompanyService(CompanyContext dbContext)
        {
            _repo = new CompanyRepo(dbContext);
        }

        public Company GetNoLazyLoad(string id)
        {
            return _repo.GetNoLazyLoad(Guid.Parse(id));
        }

        public async Task<Company> Add(Company newCompany)
        {
            if (newCompany == null)
                throw new Exception("Valor não pode ser nulo para empresa");

            await _repo.Create(newCompany);
            return newCompany;
        }

        public IEnumerable<Company> GetAll()
        {
            return _repo.GetAll().AsEnumerable();
        }

        public IEnumerable<Company> GetAllNoLazyLoad()
        {
            return _repo.GetAllNoLazyLoad().AsEnumerable();
        }
    }
}
