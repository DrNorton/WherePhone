using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.Api.Facade;
using WherePhone.Views;
using Xamarin.Forms;
using Device = WherePhone.Api.Models.Device;

namespace WherePhone.ViewModels
{
    public class PhoneLocatorViewModel:BaseViewModel
    {
        private readonly IApiFacade _apiFacade;
        private readonly INavigation _navigation;
        private ObservableCollection<Device> _phones;
        private ObservableCollection<Grouping<string, Device>> _phonesGrouped;
        private Device _selectedPhone; 

        public PhoneLocatorViewModel(IApiFacade apiFacade,INavigation navigation)
        {
            _apiFacade = apiFacade;
            _navigation = navigation;
            GetDevices();
        }

        public ObservableCollection<Device> Phones
        {
            get { return _phones; }
            set
            {
                _phones = value;
                SetGroup();
                base.OnPropertyChanged("Phones");
            }
        }

        private void SetGroup()
        {
            var sorted =
          from phone in _phones
          group phone by phone.Platform
          into contactGroup
          select new Grouping<string, Device>(contactGroup.Key, contactGroup);
            PhonesGrouped = new ObservableCollection<Grouping<string, Device>>(sorted);
        }

        public ObservableCollection<Grouping<string, Device>> PhonesGrouped
        {
            get { return _phonesGrouped; }
            set
            {
                _phonesGrouped = value;
                base.OnPropertyChanged("PhonesGrouped");
            }
        }

        public  Device SelectedPhone
        {
            get { return _selectedPhone; }
            set
            {
                _selectedPhone = value;
                if (value != null)
                {
                   GoToPhoneDetails(value);
                }
                base.OnPropertyChanged("SelectedPhone");
            }
        }

        private void GoToPhoneDetails(Device device)
        {
            var phoneLocatorDetailView=IoC.Get<PhoneLocatorDetailView>();
            phoneLocatorDetailView.Guid = device.Guid;
            _navigation.PushAsync(phoneLocatorDetailView);
        }

        private async void GetDevices()
        {
            base.IsLoading = true;
            Phones = new ObservableCollection<Device>(await _apiFacade.GetPhoneList());
            foreach (var phone in Phones)
            {
                if (String.IsNullOrEmpty(phone.ImageUrl))
                {
                    phone.ImageUrl = @"http://sakha.today/wp-content/uploads/2014/10/3qcnphhuwfc_41.jpg";
                }
            }
            base.IsLoading = false;
        }
    }

    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }

}
