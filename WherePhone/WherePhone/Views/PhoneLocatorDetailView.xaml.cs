using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.ViewModels;
using Xamarin.Forms;

namespace WherePhone.Views
{
    public partial class PhoneLocatorDetailView : ContentPage
    {
        private PhoneLocatorDetailViewModel _viewModel;

        public string Guid
        {
            
            set
            {
                _viewModel.GetPhone(value);
            }
        }

        public PhoneLocatorDetailView(PhoneLocatorDetailViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.BindingContext = viewModel;
        }
    }
}
