using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Requests
{
    public class GetBorrowListRequest:BaseParamRequest
    {
        public override string Controller
        {
            get { return "borrowing"; }
        }

        public override string MethodName
        {
            get { return "list"; }
        }
    }
}
