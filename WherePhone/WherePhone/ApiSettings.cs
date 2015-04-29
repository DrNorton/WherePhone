using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api;
using WherePhone.Api.Models;

namespace WherePhone
{
    public class ApiSettings :IApiSettings
    {
        private Token _token;
        public string BaseUrl
        {
            get { return "http://where-phone.appspot.com"; }
        }
        public Api.Models.Token SavedToken
        {
            get { return _token; }
            set { _token = value; }
        }


        public string UserApiUrl
        {
            get { return "http://10.3.2.6:82"; }
        }
    }
}
