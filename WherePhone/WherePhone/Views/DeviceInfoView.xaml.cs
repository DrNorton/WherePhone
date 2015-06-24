using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.ViewModels;
using Xamarin.Forms;

namespace WherePhone.Views
{
    public partial class DeviceInfoView : ContentPage
    {
        public DeviceInfoView(DeviceInfoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
