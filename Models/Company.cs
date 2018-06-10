using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Company : BaseObject
    {
        [Required]
        [JsonProperty("nome")]
        public string Name { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public virtual Address Address { get; set; }
        [Required]
        public virtual RootCoinValues CurrentRootCoinValues { get; set; }

        public Company()
        {
                
        }

        public Company(string Name, string CNPJ, string Email, Address Adress)
        {
            this.Name = Name;
            this.CNPJ = CNPJ;
            this.Email = Email;
            this.Address = Adress;
        }

        public bool IsCnpjQuerySucess()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(CNPJ);
        }
    }
}
