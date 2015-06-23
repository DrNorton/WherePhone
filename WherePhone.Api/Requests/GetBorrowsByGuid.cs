using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class GetBorrowsByGuid:BaseParamRequest
    {
        public override string Controller
        {
            get { return "borrow"; }
        }

        public override string MethodName
        {
            get { return ""; }
        }

        public GetBorrowsByGuid(string deviceId)
        {
            base.Params.Add("Guid", deviceId);
            this.Type = HttpMethod.Get;
        }
    }
}
