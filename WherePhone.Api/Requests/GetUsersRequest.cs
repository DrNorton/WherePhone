using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class GetUsersRequest:IRequest
    {
        private string _baseUrl;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        public string Controller
        {
            get { return "api"; }
        }

        public string MethodName
        {
            get { return "users"; }
        }

        public System.Net.Http.HttpMethod Type
        {
            get
            {
                return HttpMethod.Get;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Models.Token Token
        {
            get { return null; }
            set
            {
               
            }
        }

        public Dictionary<string, object> Params
        {
            get { return new Dictionary<string, object>();}
        }
    }
}
