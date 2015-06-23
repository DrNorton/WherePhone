using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WherePhone.Api.Models
{
    public class ApiResponse<T>
    {
        public int ErrorCode { get; set; }
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("Result")]
        public T Result { get; set; }
    }
}
