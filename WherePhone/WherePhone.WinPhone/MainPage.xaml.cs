using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Acr.BarCodes;
using ImageCircle.Forms.Plugin.WindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WherePhone.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            BarCodes.Init();
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();
            LoadApplication(new WherePhoneApp());
        }
    }
}
