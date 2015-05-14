using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeviceInfo.Plugin;
using DeviceInfo.Plugin.Abstractions;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;
using WherePhone.Views;
using Xamarin.Forms;

namespace WherePhone.ViewModels
{
  
    public class MainViewModel :BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
        private string _deviceId;
        private Phone _device;
        private Borrower _user;
        private string _model;
        private Platform _platform;
        private string _version;
        private DateTime _when;

        public  INavigation Navigation { get; set; }


        public MainViewModel(IApiFacade apiFacade, IAppIdGenerator generator)
        {
            _apiFacade = apiFacade;
           
            _deviceId = generator.Id();
            _model = generator.DeviceModel();
            _platform = generator.Platform();
            _version = generator.PlatformVersion();
            GetDeviceInfo();
        }

        public async void GetDeviceInfo()
        {

           var borrow=await _apiFacade.GetBorrow(_deviceId);
            if (borrow == null)
            {
                //Значит телефон либо ничей, либо не зарегистрирован
                //проверяем регистрацию
                _device=await _apiFacade.GetPhone(_deviceId);
                if (_device == null)
                {
                    //Если девайс не найден, то регистрируем его
                    Device = await _apiFacade.AddPhone(new Phone() {Name = _model,Platform = _platform.ToString(),Udid = _deviceId});
                }
            }
            else
            {
                Device = borrow.Phone;
                User = borrow.Borrower;
                When=borrow.When;
            }
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

        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                base.OnPropertyChanged("Model");
            }
        }

        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                base.OnPropertyChanged("Version");
            }
        }

        public Platform Platform
        {
            get { return _platform; }
            set
            {
                _platform = value;
                base.OnPropertyChanged("Platform");
            }
        }

        public Phone Device
        {
            get { return _device; }
            set
            {
                _device = value;
                base.OnPropertyChanged("Device");
            }
        }

        public Borrower User
        {
            get { return _user; }
            set
            {
                _user = value;
                base.OnPropertyChanged("User");
            }
        }

        public DateTime When
        {
            get { return _when; }
            set
            {
                _when = value;
                base.OnPropertyChanged("When");
            }
        }
    }
}
