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
    public class AddBorrowRequest : BaseParamRequest
    {
        public override string Controller
        {
            get { return "borrow"; }
        }

        public override string MethodName
        {
            get { return "addborrow"; }
        }

        public AddBorrowRequest(AddBorrow borrow)
        {
            base.Params = borrow.ToDictionary();
            base.Type=HttpMethod.Post;
        }
    }
}
