using Newtonsoft.Json;

namespace Models
{
    public class Address : BaseObject
    {
        [JsonProperty("uf")]
        public string City { get; set; }
        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [JsonProperty("numero")]
        public string Number { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("complemento")]
        public string AdditionalInfo { get; set; }

        public Address()
        {

        }

        public Address(string city, string neighborhood, string street, string number, string cep, string additionalInfo)
        {
            this.City = city;
            this.Neighborhood = neighborhood;
            this.Street = street;
            this.Number = number;
            this.CEP = cep;
            this.AdditionalInfo = additionalInfo;
        }
    }
}