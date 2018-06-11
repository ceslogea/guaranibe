using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using SampleCompany.Infra;

namespace SampleCompany.Controllers
{
    [Produces("application/json")]
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        private ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return await _service.GetAllNoLazyLoad();
        }

        // GET: api/Company/5
        [HttpGet("{id}", Name = "Get")]
        public Company Get(string id)
        {
            return _service.GetNoLazyLoad(id);
        }
        
        // POST: api/Company
        [HttpPost]
        [ValidationActionFilter]
        public async Task<Company> Post([FromBody]Company value)
        {
           return await _service.Add(value);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
