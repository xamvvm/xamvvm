using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using PageFactory.Examples.RxUI.Views;
using Xamarin.Forms;

namespace PageFactory.Examples.RxUI
{
    public class App : Application
    {
        public App()
        {
            var bootstrapper = new AppBootstrapper();
            // The root page of your application

            using (Log.Perf("Create MainPage"))
            {
                var t = new MainPage();
            }
            MainPage = bootstrapper.CreateMainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
