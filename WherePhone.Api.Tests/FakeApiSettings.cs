using WherePhone.Api.Models;

namespace WherePhone.Api.Tests
{
    public class FakeApiSettings : IApiSettings
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
    }
}