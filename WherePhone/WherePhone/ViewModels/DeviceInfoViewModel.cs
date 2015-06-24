using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceInfo.Plugin.Abstractions;

namespace WherePhone.ViewModels
{
    public class DeviceInfoViewModel:BaseViewModel
    {
        private string _model;
        private Platform _platform;
        private string _version;
        private string _deviceId;

        public DeviceInfoViewModel(IAppIdGenerator generator)
        {
            _deviceId = generator.Id();
            _model = generator.DeviceModel();
            _platform = generator.Platform();
            _version = generator.PlatformVersion();
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

    }
}
