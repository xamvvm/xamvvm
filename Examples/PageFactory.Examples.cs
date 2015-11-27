using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			MainPage = new XamarinFormsPageFactory().Init<HomeViewModel, PFNavigationPage>();
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

