using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WherePhone.Api.Models
{
    public class User
    {
        [JsonProperty("commonName")]
        public string CommonName { get; set; }
          [JsonProperty("id")]
        public int Id { get; set; }
         [JsonProperty("firstName")]
        public string FirstName { get; set; }
       [JsonProperty("lastName")]
        public string LastName { get; set; }
          [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("mail")]
        public string Email { get; set; }
    }
}
