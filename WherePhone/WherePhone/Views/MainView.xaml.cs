using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.ViewModels;
using Xamarin.Forms;

namespace WherePhone.Views
{
    public partial class MainView : MasterDetailPage
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
           // menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);
            viewModel.Navigation = this.Navigation;
            Detail = IoC.Get<PhoneOwnerView>();

        }


    }
}
