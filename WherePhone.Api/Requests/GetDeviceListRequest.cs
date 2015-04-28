using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class GetDeviceListRequest : BaseParamRequest
    {
        public override string Controller
        {
            get { return "device"; }

        }
        public override string MethodName
        {
            get { return "list"; }
        }
    }
}
