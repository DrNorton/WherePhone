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
            get { return "delete"; }
        }

        public DeletePhoneRequest(string uid)
        {
            var obj = new JObject();
            obj.Add("udid",uid);
            base.Params.Add("device",obj);
            base.Type=HttpMethod.Post;
        }
    }
}
