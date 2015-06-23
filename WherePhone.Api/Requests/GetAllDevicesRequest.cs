using System.Collections.Generic;
using System.Net.Http;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class GetAllDevicesRequest : BaseParamRequest
    {
        public override string Controller
        {
            get { return "device"; }

        }
        public override string MethodName
        {
            get { return ""; }
        }

        public GetAllDevicesRequest()
        {
            base.Type = HttpMethod.Get;
        }
    }
}
