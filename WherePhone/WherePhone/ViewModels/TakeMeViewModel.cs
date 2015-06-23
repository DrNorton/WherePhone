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
      
        private List<User> _usersBeforeFilter;
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private string _pattern;
        public string DeviceId { get; set; }

        public TakeMeViewModel(IApiFacade apiFacade)
        {
            _apiFacade = apiFacade;
            _users = new ObservableCollection<User>();
            _usersBeforeFilter=new List<User>();
            GetPeoples();
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

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                if (value != null)
                {
                    BorrowPhone(value);
                }
            
                base.OnPropertyChanged("SelectedUser");
            }
        }

        private async void BorrowPhone(User user)
        {
          var ds= await _apiFacade.AddBorrow(new AddBorrow(){Description = "",UserId = user.Id,DeviceId = DeviceId});
            Debug.WriteLine(ds);
        }

        private void FilterCollection(string value)
        {
            IsLoading = true;
            Users =new ObservableCollection<User>(_usersBeforeFilter.Where(x => x.Fio.ToLower().Contains(value.ToLower())));
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
                Pattern = result.Code;
            }
        }
    }
}
