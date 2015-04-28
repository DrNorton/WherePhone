using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.BarCodes;


using Xamarin.Forms;

namespace WherePhone.ViewModels
{
    public class TakeMeViewModel :BaseViewModel
    {
      
      
        private string _youName;

        public TakeMeViewModel()
        {
         
          //  GetPeoples();
        }

        private async void GetPeoples()
        {
          
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
