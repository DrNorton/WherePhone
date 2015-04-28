using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WherePhone.Api.Models;

namespace WherePhone.Api.Facade
{
    public interface IApiFacade
    {
        Task<List<Phone>> GetPhoneList();
    }
}