using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class GetBorrowRequest:BaseParamRequest
    {
        public override string Controller
        {
            get { return "borrowing"; }
        }

        public override string MethodName
        {
            get { return "list"; }
        }

        public GetBorrowRequest(string phoneId)
        {
            base.Params.Add("udid", phoneId);
            base.Type = HttpMethod.Post;
        }
    }
}
