using WherePhone.Api.Models;

namespace WherePhone.Api
{
    public interface IApiSettings
    {
        string BaseUrl { get; }

      
        Token SavedToken { get; set; }
     
    }
}
