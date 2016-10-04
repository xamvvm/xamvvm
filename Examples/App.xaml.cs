using Xamarin.Forms;
using DLToolkit.PageFactory;

namespace Examples
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var factory = new XamarinFormsPageFactory();

			factory.Init(this);
			factory.RegisterView<MainNavigationPageModel, MainNavigationPage>(
				createPage: () =>
				{
					var mainPage = PageFactory.Instance.GetPageFromCache<MainPageModel>();
					var navPage = new MainNavigationPage(mainPage as Page);
					return navPage ;
				});

			MainPage = PageFactory.Instance.GetPageFromCache<MainNavigationPageModel>() as NavigationPage;
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
