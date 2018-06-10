using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace SampleCompany.Controllers
{
    [Produces("application/json")]
    [Route("api/CoinType")]
    public class CoinTypeController : Controller
    {
        private ICoinTypeSerrvice _service;

        public CoinTypeController(ICoinTypeSerrvice service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ContentResult> Get(
            [FromServices]IConfiguration config,
            [FromServices]IMemoryCache cache)
        {
            string valorJSON = await cache.GetOrCreateAsync<string>(
                "Cotacoes", async context =>
                {
                    context.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
                    context.SetPriority(CacheItemPriority.High);
                    var result = await _service.Get();
                    return result;
                });

            return Content(valorJSON, "application/json");
        }
    }
}