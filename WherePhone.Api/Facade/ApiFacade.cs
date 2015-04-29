using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WherePhone.Api.ExceptionRouter;
using WherePhone.Api.Executer;
using WherePhone.Api.Models;
using WherePhone.Api.Requests;
using WherePhone.Api.Requests.Base;

namespace WherePhone.Api.Facade
{
    public class ApiFacade : IApiFacade
    {
        private readonly IApiExceptionRouter _exceptionRouter;
        private IApiExecuter _apiExecuter;
        private readonly IApiSettings _apiSettings;
        

        public ApiFacade(IApiExecuter apiExecuter,IApiSettings apiSettings)
        {
            _apiExecuter = apiExecuter;
            _apiSettings = apiSettings;
   
        }

        public Task<List<Phone>> GetPhoneList()
        {
            var getPostRequest = new GetDeviceListRequest();
            getPostRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<Phone>>(getPostRequest);
        }

        public Task<List<User>> GetUsers()
        {
            var getPostRequest = new GetUsersRequest();
            getPostRequest.BaseUrl = _apiSettings.UserApiUrl;
            return ExecuteWithErrorHandling<List<User>>(getPostRequest);
        }



        private Task<T> ExecuteWithErrorHandling<T>(IRequest request)
        {
            try
            {
                request.Token = _apiSettings.SavedToken;//Добавляем токен
               return _apiExecuter.Execute<T>(request);
            }
            catch (ApiException ex)
            {
             
               throw ex;
            }
        }


     
    }
}
