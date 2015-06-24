using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeviceInfo.Plugin;
using DeviceInfo.Plugin.Abstractions;
using WherePhone.Api;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;
using WherePhone.Views;
using Xamarin.Forms;
using Device = WherePhone.Api.Models.Device;

namespace WherePhone.ViewModels
{
  
    public class MainViewModel :BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
        private string _deviceId;
        private Device _device;
        private User _user;
        private string _model;
        private Platform _platform;
        private string _version;
        private DateTime _when;
        private List<MenuItem> _menuItems;
        private MenuItem _selectedMenu;
     
        public  INavigation Navigation { get; set; }


        public MainViewModel(IApiFacade apiFacade, IAppIdGenerator generator)
        {
            _apiFacade = apiFacade;
           
            _deviceId = generator.Id();
            _model = generator.DeviceModel();
            _platform = generator.Platform();
            _version = generator.PlatformVersion();
            MenuItems = new List<MenuItem>() { new MenuItem() {Id = 0,Description = "узнать у кого телефон", Name = "У кого телефон" },new MenuItem(){Id=1,Description = "О телефоне",Name = "информация о телефоне"} };
            GetDeviceInfo();
        }

        

        public async void GetDeviceInfo()
        {
            IsLoading = true;
            var phone = await _apiFacade.GetPhone(_deviceId);
            if (phone == null)
            {
                //Значит телефон либо ничей, либо не зарегистрирован
                //проверяем регистрацию
                _device = await _apiFacade.GetPhone(_deviceId);
                if (_device == null)
                {
                    //Если девайс не найден, то регистрируем его
                    var device = new Device() { Name = _model, Platform = _platform.ToString(), Guid = _deviceId };
                    await _apiFacade.RegisterPhone(device);
                    Device = device;

                }
                User = new User() { FirstName = "Телефон ничей" };
              
            }
            else
            {
                Device = phone;
                User = phone.CurrentUser;
                When = phone.BorrowTime;
            }
            IsLoading = false;
        }

        public ICommand GoToAllDevicesCommand
        {
            get { return new Command(() => Navigation.PushAsync(IoC.Get<AllDevicesView>())); }
        }

        public ICommand GoToTakeMeCommand
        {
            get
            {
               
                return new Command(() =>
                {
                    var view = IoC.Get<TakeMeView>();
                    view.DeviceId = _deviceId;
                    Navigation.PushAsync(view);
                });
            }
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

        public Device Device
        {
            get { return _device; }
            set
            {
                _device = value;
                base.OnPropertyChanged("Device");
            }
        }

        public User User
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

        public List<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                base.OnPropertyChanged("MenuItems");
            }
        }

        public MenuItem SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                _selectedMenu = value;
                if (value != null)
                {
                    GoTo(value.Id);
                }
            
                
                base.OnPropertyChanged("SelectedMenu");
            }
        }

        private void GoTo(int id)
        {
            switch (id)
            {
                case 0:
                    Navigation.PushAsync(IoC.Get<GetPhoneView>());
                    break;

                case 1:
                    Navigation.PushAsync(IoC.Get<TakeMeView>());
                    break;
            }
           
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Id { get; set; }
    }
}
