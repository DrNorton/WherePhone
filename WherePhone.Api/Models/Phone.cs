using System;
using Newtonsoft.Json;

namespace WherePhone.Api.Models
{
    public class Phone
    {
        [JsonProperty("udid")]
        public string Udid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

       
    }
}
