using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using WherePhone.Api.Facade;
using WherePhone.Core;
using WherePhone.Ldap;
using WherePhone.Views;
using Xamarin.Forms;

namespace WherePhone.ViewModels
{
  
    public class MainViewModel :BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
        private readonly AllDevicesView _allDevicesView;
        private readonly TakeMeView _takeMeView;
        private readonly IAppIdGenerator _generator;
        private string _deviceId;

        public  INavigation Navigation { get; set; }


        public MainViewModel(IApiFacade apiFacade, AllDevicesView allDevicesView,TakeMeView takeMeView, IAppIdGenerator generator)
        {
            _apiFacade = apiFacade;
            _allDevicesView = allDevicesView;
            _takeMeView = takeMeView;
            _generator = generator; 
            new LdapDirectoryService().Get();
          
           
         
        }

        public ICommand GoToAllDevicesCommand
        {
            get { return new Command(() => Navigation.PushAsync(_allDevicesView)); }
        }

        public ICommand GoToTakeMeCommand
        {
            get { return new Command(() => Navigation.PushAsync(_takeMeView)); }
        }

        public string DeviceId
        {
            get { return _deviceId; }
            set
            {
                _deviceId = value;
                base.OnPropertyChanged("DeviceId");
            }
        }
    }
}
