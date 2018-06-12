using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repos
{
    public class CompanyRepo : BaseRepo<Company>, ICompanyRepo
    {
        public CompanyRepo(CompanyContext dbContext) : base(dbContext)
        {
        }

        public Company GetNoLazyLoad(Guid id)
        {
            return _dbContext.Companys.Include(r => r.Address).Include(r => r.CurrentRootCoinValues).FirstOrDefault(r => r.Id.Equals(id));
        }

        public IQueryable<Company> GetAllNoLazyLoad()
        {
            return _dbContext.Companys.Include(r=>r.Address).Include(r => r.CurrentRootCoinValues).AsQueryable();
        }
    }
}
