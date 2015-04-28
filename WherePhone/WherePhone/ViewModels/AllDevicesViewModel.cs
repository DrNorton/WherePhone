using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;

namespace WherePhone.ViewModels
{
    public class AllDevicesViewModel : BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
        private List<Phone> _phones;

        public List<Phone> Phones
        {
            get { return _phones; }
            set
            {
                _phones = value;
                OnPropertyChanged("Phones");
            }
        }

        public AllDevicesViewModel(IApiFacade apiFacade)
        {
            _apiFacade = apiFacade;
            GetDevices();
        }

        private async void GetDevices()
        {
           Phones=await _apiFacade.GetPhoneList();
            foreach (var phone in Phones)
            {
                if (String.IsNullOrEmpty(phone.Image))
                {
                    phone.Image = @"http://sakha.today/wp-content/uploads/2014/10/3qcnphhuwfc_41.jpg";
                }
            }
        }
    }
}
