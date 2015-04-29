using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using WherePhone.Api.Facade;

using WherePhone.Views;
using Xamarin.Forms;

namespace WherePhone.ViewModels
{
  
    public class MainViewModel :BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
      
        private readonly IAppIdGenerator _generator;
        private string _deviceId;

        public  INavigation Navigation { get; set; }


        public MainViewModel(IApiFacade apiFacade, IAppIdGenerator generator)
        {
            _apiFacade = apiFacade;
  
            _generator = generator; 
        
            



        }

        public ICommand GoToAllDevicesCommand
        {
            get { return new Command(() => Navigation.PushAsync(IoC.Get<AllDevicesView>())); }
        }

        public ICommand GoToTakeMeCommand
        {
            get { return new Command(() => Navigation.PushAsync(IoC.Get<TakeMeView>())); }
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
