using Newtonsoft.Json;

namespace WherePhone.Api.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string Value { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiredIn { get; set; }
    }
}
