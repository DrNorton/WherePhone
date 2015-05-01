using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class GetDeviceRequest :BaseParamRequest
    {

        public override string Controller
        {
            get { return "device"; }
        }

        public override string MethodName
        {
            get { return "list"; }
        }

        public GetDeviceRequest(string phoneId)
        {
            base.Params.Add("udid",phoneId);
            base.Type=HttpMethod.Post;
        }
    }
}
