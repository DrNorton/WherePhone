using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Models;
using WherePhone.Api.Models.Base;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class RegisterPhoneRequest:BaseParamRequest
    {
        public override string Controller
        {
            get { return "device"; }
        }

        public override string MethodName
        {
            get { return "register"; }
        }

        public RegisterPhoneRequest(Device device)
        {
            base.Params = device.ToDictionary();
            base.Type=HttpMethod.Post;
        }

    }
}
