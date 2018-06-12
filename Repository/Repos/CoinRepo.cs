using Models;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repos
{
    public class CoinRepo : BaseRepo<RootCoin>,  ICoinRepo
    {
        public CoinRepo(CompanyContext dbContext) : base(dbContext)
        {
        }

        public RootCoin Last()
        {
            return _dbContext.RootCoin.LastOrDefault();
        }
    }
}
