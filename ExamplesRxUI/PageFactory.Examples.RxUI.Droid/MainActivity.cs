using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Logging;


namespace PageFactory.Examples.RxUI.Droid
{

    class con : ILogSink
    {
        public void Log(string message)
        {
            Android.Util.Log.Info("PageFactory", message);
        }
    }


    [Activity(Label = "PageFactory.Examples.RxUI", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Log.LogSink = new con();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

