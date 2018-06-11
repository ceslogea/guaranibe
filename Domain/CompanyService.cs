using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Newtonsoft.Json;
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
        private ICoinTypeSerrvice _coinService;
        private ICompanyRepo _repo;
        public IConfiguration Configuration { get; }

        public CompanyService(CompanyContext dbContext, IServiceProvider serviceProvider)
        {
            _coinService = serviceProvider.GetService<ICoinTypeSerrvice>();
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

        public async Task<IEnumerable<Company>> GetAllNoLazyLoad()
        {
            var results = _repo.GetAllNoLazyLoad().ToList();
            foreach (var item in results)
            {
                var dataBid = GetDateTimeFromUnixTimeStamp(uint.Parse(item.CurrentRootCoinValues.timestamp));
                
                if ((dataBid - DateTime.Now).TotalHours > 20)
                {
                    var coins = await _coinService.Get();
                    dynamic jsonResponse = JsonConvert.DeserializeObject<RootCoin>(coins);
                }
            }
            return results;
        }

        public DateTime GetDateTimeFromUnixTimeStamp(uint timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp);
        }
    }
}
