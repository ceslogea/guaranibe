using Models;
using Newtonsoft.Json;
using Repository.Context;
using Repository.Repos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CoinTypeService : ICoinTypeSerrvice
    {
        private static HttpClient client = new HttpClient();
        private const string URL_AUCTION = @"https://economia.awesomeapi.com.br/all";
        private ICoinRepo _repo;
        private RootCoin _coins;

        public CoinTypeService(CompanyContext context)
        {
            _repo = new CoinRepo(context);
        }

        public async Task<RootCoin> GetCoins()
        {
            await Get();
            return _coins;
        }

        public async Task<string> Get()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(URL_AUCTION);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _coins = JsonConvert.DeserializeObject<RootCoin>(result);
                    await _repo.Create(_coins);
                    //throw new Exception("teste");
                    return result;
                }
            }
            catch (Exception e)
            {
                //Retorna ultimo valor do DB
                var lastStableResult = _repo.Last();
                if (lastStableResult != null)
                    return JsonConvert.SerializeObject(lastStableResult);

                throw new Exception("Falha no serviço.", e);
            }
            return string.Empty;
        }

    }
}
