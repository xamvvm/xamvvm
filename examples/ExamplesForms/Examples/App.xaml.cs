using System.Collections.Generic;
using Xamarin.Forms;
using Xamvvm;

namespace Examples
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var factory = new XamvvmFormsFactory(this);
			factory.RegisterNavigationPage<MainNavigationPageModel>(() => this.GetPageFromCache<MainPageModel>());
			XamvvmCore.SetCurrentFactory(factory);
			MainPage = this.GetPageFromCache<MainNavigationPageModel>() as NavigationPage;
            // grab the start page
            var startPage = this.GetPageFromCache<MainPageModel>();
            // grab it's page model
            var startPageModel = startPage.GetPageModel<MainPageModel>();
            // invoke some initialization logic
            startPageModel.OnNavigatedTo();
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
