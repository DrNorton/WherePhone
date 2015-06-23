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

        public Task<List<Device>> GetPhoneList()
        {
            var getDeviceListRequest = new GetAllDevicesRequest();
            getDeviceListRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<Device>>(getDeviceListRequest);
        }

        public async Task<Device> GetPhone(string id)
        {
            var getDeviceRequest = new GetDeviceRequest(id);
            getDeviceRequest.BaseUrl = _apiSettings.BaseUrl;
            var phones= await ExecuteWithErrorHandling<Device>(getDeviceRequest);
            return phones;
        }

        public async Task<object> RegisterPhone(Device newDevice)
        {
            var addPhoneRequest = new RegisterPhoneRequest(newDevice);
            addPhoneRequest.BaseUrl = _apiSettings.BaseUrl;
            var result = await ExecuteWithErrorHandling<Device>(addPhoneRequest);
            return result;
        }

        public async Task<Device> DeletePhone(Device deletedDevice)
        {
            var deletePhoneRequest = new DeletePhoneRequest(deletedDevice.Guid);
            deletePhoneRequest.BaseUrl = _apiSettings.BaseUrl;
            var result = await ExecuteWithErrorHandling<Device>(deletePhoneRequest);
            return result;
        }

        public Task<List<User>> GetUsers()
        {
            var getUsersRequest = new GetUsersRequest();
            getUsersRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<User>>(getUsersRequest);
        }

        public Task<object> AddBorrow(AddBorrow borrow)
        {
            var addBorrowRequest = new AddBorrowRequest(borrow);
            addBorrowRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<object>(addBorrowRequest);
        }

        public Task<Borrow> GetCurrentBorrow(string deviceId)
        {
            var borrowListRequest = new GetCurrentBorrow(deviceId);
            borrowListRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<Borrow>(borrowListRequest);
        }

        public async Task<List<Borrow>> GetBorrows(string deviceId)
        {
            var getBorrowRequest = new GetBorrowsByGuid(deviceId);
            getBorrowRequest.BaseUrl = _apiSettings.BaseUrl;
            var phones = await ExecuteWithErrorHandling<List<Borrow>>(getBorrowRequest);
            if (phones == null) return null;
            return phones;
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
