using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WherePhone.Core
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            Current = this;

        }

        public ResourceDictionary Resources { get; set; }

        public static Xamarin.Forms.Application Current { get; set; }

        public Page MainPage { get; set; }
    }
}
