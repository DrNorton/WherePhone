using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;

namespace WherePhone.ViewModels
{
    public class PhoneLocatorDetailViewModel:BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
        private Device _phone;

        public PhoneLocatorDetailViewModel(IApiFacade apiFacade)
        {
            _apiFacade = apiFacade;
        }

        public Device Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                base.OnPropertyChanged("Phone");
            }
        }

        public async void GetPhone(string guid)
        {
            base.IsLoading = true;
            Phone=await _apiFacade.GetPhone(guid);
            base.IsLoading = false;
        }
    }
}
