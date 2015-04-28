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
        [JsonProperty("error")]
        public string ErrorMessage { get; set; }
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
