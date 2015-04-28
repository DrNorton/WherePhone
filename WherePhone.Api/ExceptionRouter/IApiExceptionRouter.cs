
namespace WherePhone.Api.ExceptionRouter
{
    public interface IApiExceptionRouter
    {
        void Route(ApiException exception);
    }
}
