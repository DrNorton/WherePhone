using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.BarCodes;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;
using Xamarin.Forms;

namespace WherePhone.ViewModels
{
    public class TakeMeViewModel :BaseViewModel
    {
        private readonly IApiFacade _apiFacade;

        private bool _isLoading;
        private string _youName;
        private List<User> _usersBeforeFilter;
        private ObservableCollection<User> _users;
        private string _pattern;

        public TakeMeViewModel(IApiFacade apiFacade)
        {
            _apiFacade = apiFacade;
            _users = new ObservableCollection<User>();
            _usersBeforeFilter=new List<User>();
            // GetPeoples();
        }

        

        private async void GetPeoples()
        {
            IsLoading = true;
            _usersBeforeFilter = await _apiFacade.GetUsers();
            Users = new ObservableCollection<User>(_usersBeforeFilter);
            IsLoading = false;
        }

        public  ICommand GetQrCodeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await GetQrCode();
                });
            }
        }

     

        public string YouName
        {
            get { return _youName; }
            set
            {
                _youName = value;
                base.OnPropertyChanged("YouName");
            }
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                base.OnPropertyChanged("Users");
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                base.OnPropertyChanged("IsLoading");
            }
        }

        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                FilterCollection(value);
                base.OnPropertyChanged("Pattern");
            }
        }

        private void FilterCollection(string value)
        {
            IsLoading = true;
            Users =new ObservableCollection<User>(_usersBeforeFilter.Where(x => x.CommonName.ToLower().Contains(value.ToLower())));
            IsLoading = false;
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
                YouName = result.Code;
            }
        }
    }
}
