using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class CnpjService : ICnpjService
    {
        private static HttpClient client = new HttpClient();
        private const string URL_RECEITA_WS = "https://www.receitaws.com.br/v1/cnpj/";

        public async Task<Company> GetCompanyByCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                throw new Exception("Parametro cnpj não pode ser nulo ou em branco.");

            cnpj = RemoveSpecialCharacters(cnpj);

            if (string.IsNullOrEmpty(cnpj))
                throw new Exception("Parametro cnpj não pode ser nulo ou em branco.");

            try
            {
                HttpResponseMessage response = await client.GetAsync(URL_RECEITA_WS + cnpj);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var company = JsonConvert.DeserializeObject<Company>(result);
                    var address = JsonConvert.DeserializeObject<Address>(result);
                    company.Address = address;
                    if (company.IsCnpjQuerySucess())
                        return company;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Serviço insdisponível no momento", e);
            }

            throw new Exception("Empresa não encontrada");
        }

        public string RemoveSpecialCharacters(string str)
        {
            
            return Regex.Replace(str, @"[^\d]", "");
           
        }

    }
}
