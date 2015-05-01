using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WherePhone.Api.Models
{
    public class DeviceId
    {
        [JsonProperty("device")]
        public DeviceIdentity Device { get; set; }
    }

    public class DeviceIdentity
    {
        public string Udid { get; set; }
    }
}
