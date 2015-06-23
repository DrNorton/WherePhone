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
            get { return "http://wherephone.azurewebsites.net/api"; }
        }
        public Api.Models.Token SavedToken
        {
            get { return _token; }
            set { _token = value; }
        }


    }
}
