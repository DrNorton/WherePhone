using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class DeletePhoneRequest:BaseParamRequest
    {
        public override string Controller
        {
            get { return "device"; }
        }

        public override string MethodName
        {
            get { return "unregister"; }
        }

        public DeletePhoneRequest(string guid)
        {
            base.Params.Add("Guid", guid);
            base.Type=HttpMethod.Post;
        }
    }
}
