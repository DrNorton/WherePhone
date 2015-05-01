using System;
using System.Collections.Generic;
using System.Linq;
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
            var getDeviceListRequest = new GetDeviceListRequest();
            getDeviceListRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<Phone>>(getDeviceListRequest);
        }

        public async Task<Phone> GetPhone(string id)
        {
            var getDeviceRequest = new GetDeviceRequest(id);
            getDeviceRequest.BaseUrl = _apiSettings.BaseUrl;
            var phones= await ExecuteWithErrorHandling<List<Phone>>(getDeviceRequest);
            if (phones == null) return null;
            return phones.FirstOrDefault();
        }

        public async Task<Phone> AddPhone(Phone newPhone)
        {
            var addPhoneRequest = new AddPhoneRequest(newPhone);
            addPhoneRequest.BaseUrl = _apiSettings.BaseUrl;
            var result = await ExecuteWithErrorHandling<Phone>(addPhoneRequest);
            return result;
        }

        public async Task<Phone> DeletePhone(Phone deletedPhone)
        {
            var deletePhoneRequest = new DeletePhoneRequest(deletedPhone.Udid);
            deletePhoneRequest.BaseUrl = _apiSettings.BaseUrl;
            var result = await ExecuteWithErrorHandling<Phone>(deletePhoneRequest);
            return result;
        }

        public Task<List<User>> GetUsers()
        {
            var getUsersRequest = new GetUsersRequest();
            getUsersRequest.BaseUrl = _apiSettings.UserApiUrl;
            return ExecuteWithErrorHandling<List<User>>(getUsersRequest);
        }

        public Task<List<BorrowTicket>> GetBorrowTickets()
        {
            var borrowListRequest = new GetBorrowListRequest();
            borrowListRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<BorrowTicket>>(borrowListRequest);
        }

        public async Task<BorrowTicket> GetBorrow(string id)
        {
            var getBorrowRequest = new GetBorrowRequest(id);
            getBorrowRequest.BaseUrl = _apiSettings.BaseUrl;
            var phones = await ExecuteWithErrorHandling<List<BorrowTicket>>(getBorrowRequest);
            if (phones == null) return null;
            return phones.FirstOrDefault();
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
