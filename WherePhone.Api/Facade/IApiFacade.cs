using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WherePhone.Api.Models;

namespace WherePhone.Api.Facade
{
    public interface IApiFacade
    {
        Task<List<Device>> GetPhoneList();
        Task<List<User>> GetUsers();
        Task<Device> GetPhone(string id);
        Task<Borrow> GetCurrentBorrow(string deviceId);
        Task<List<Borrow>> GetBorrows(string deviceId);
        Task<object> RegisterPhone(Device newDevice);
        Task<Device> DeletePhone(Device deletedDevice);
        Task<object> AddBorrow(AddBorrow borrow);
    }
}