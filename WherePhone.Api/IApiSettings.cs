using WherePhone.Api.Models;

namespace WherePhone.Api
{
    public interface IApiSettings
    {
        string BaseUrl { get; }

        string UserApiUrl { get; }
        Token SavedToken { get; set; }
     
    }
}
