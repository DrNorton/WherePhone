using System;
using Acr.BarCodes;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using WherePhone.Core;
using Xamarin.Forms.Platform.Android;

namespace WherePhone.Droid
{
    //https://github.com/aritchie/barcodes/tree/master/src/Samples/Samples.iOS
    [Activity(Label = "Silkweb.Mobile.MountainForecast.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Icon="@android:color/transparent")]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            BarCodes.Init(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            LoadApplication(new WherePhoneApp());
        }
    }
}

