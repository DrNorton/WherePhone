using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.BarCodes;
using DeviceInfo.Plugin.Abstractions;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;
using WherePhone.Views;
using Xamarin.Forms;
using Device = WherePhone.Api.Models.Device;

namespace WherePhone.ViewModels
{
    public class PhoneOwnerViewModel:BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
        private string _deviceId;
        private Device _device;
        private User _user;
        private string _model;
        private DateTime _when;
        private Platform _platform;
        private string _version;

        public INavigation Navigation { get; set; }

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

        public ICommand GoToFastTakeByCodeCommand
        {
            get
            {

                return new Command(async() =>
                {
                     await GetQrCode();
                });
            }
        }

        private async Task GetQrCode()
        {
            var result = await BarCodes.Instance.Read();
            if (!result.Success)
            {

            }
            else
            {
                var msg = String.Format("Barcode: {0} - {1}", result.Format, result.Code);
                int id;
                if (int.TryParse(result.Code, out id))
                {
                    BorrowPhone(new User() { Id = id });   
                }
               
            }
        }

        private async void BorrowPhone(User user)
        {
            var ds = await _apiFacade.AddBorrow(new AddBorrow() { Description = "", UserId = user.Id, DeviceId = DeviceId });
            GetDeviceInfo();
        }

        public PhoneOwnerViewModel(IApiFacade apiFacade, IAppIdGenerator generator)
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
    }
}
