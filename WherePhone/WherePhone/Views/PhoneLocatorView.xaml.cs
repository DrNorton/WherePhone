using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.ViewModels;
using Xamarin.Forms;

namespace WherePhone.Views
{
    public partial class PhoneLocatorView : ContentPage
    {
    
 
        public PhoneLocatorView(PhoneLocatorViewModel locatorViewModel)
        {
            InitializeComponent();
            this.BindingContext = locatorViewModel;
        }

        
    }
}
