using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WherePhone.Api.Models;

namespace WherePhone.Api.Facade
{
    public interface IApiFacade
    {
        Task<List<Phone>> GetPhoneList();
        Task<List<User>> GetUsers();
        Task<Phone> GetPhone(string id);
        Task<List<BorrowTicket>> GetBorrowTickets();
        Task<BorrowTicket> GetBorrow(string id);
        Task<Phone> AddPhone(Phone newPhone);
        Task<Phone> DeletePhone(Phone deletedPhone);
    }
}