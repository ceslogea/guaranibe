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

        public CompanyService(CompanyContext dbContext, ICoinTypeSerrvice coinService)
        {
            _coinService = coinService;
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
            var alreadyUpdatedFromWS = false;
            var results = _repo.GetAllNoLazyLoad().ToList();
            foreach (var item in results)
                if ((DateTime.Now - item.CurrentRootCoinValues.LastModifiedDate).TotalMinutes > 20)
                {
                    alreadyUpdatedFromWS = await UpdateOnceFromWebService(alreadyUpdatedFromWS);
                    await UpdateCoinStatus(item);
                }

            return results;
        }

        private async Task<bool> UpdateOnceFromWebService(bool alreadyUpdatedFromWS)
        {
            if (!alreadyUpdatedFromWS)
            {
                await _coinService.Get();
                alreadyUpdatedFromWS = true;
            }

            return alreadyUpdatedFromWS;
        }

        private async Task UpdateCoinStatus(Company item)
        {
            var coins = await _coinService.GetCoins();
            item.CurrentRootCoinValues = coins.GetType().GetProperty(item.CurrentRootCoinValues.code).GetValue(coins) as RootCoinValues;
        }

    }
}
