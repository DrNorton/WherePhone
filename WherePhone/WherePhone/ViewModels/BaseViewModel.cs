using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WherePhone.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isLoading;
        private bool _isLoadingReverse;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                IsLoadingReverse = !value;
                OnPropertyChanged("IsLoading");
            }
        }

        public bool IsLoadingReverse
        {
            get { return _isLoadingReverse; }
            set
            {
                _isLoadingReverse = value;
                OnPropertyChanged("IsLoadingReverse");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
