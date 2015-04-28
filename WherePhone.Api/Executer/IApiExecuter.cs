using System.Threading.Tasks;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Executer
{
    public interface IApiExecuter
    {
        Task<T> Execute<T>(IRequest request);
    }
}