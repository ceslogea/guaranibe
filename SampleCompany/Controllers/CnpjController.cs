using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace SampleCompany.Controllers
{
    [Produces("application/json")]
    [Route("api/Cnpj")]
    public class CnpjController : Controller
    {
        private ICnpjService _service;

        public CnpjController(ICnpjService cnpjService)
        {
            _service = cnpjService;
        }

        [HttpPost]
        [HttpGet]
        public async Task<Company> Get(string cnpj)
        {
            return await _service.GetCompanyByCnpj(cnpj);
        }

      
    }
}