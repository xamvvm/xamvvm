using Xamvvm;

namespace ExampleRxUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
			var factory = new XamvvmFormsRxUIFactory(this);
			XamvvmCore.SetCurrentFactory(factory);

			MainPage = this.GetPageFromCache<DemoListViewPageModel>() as Page;
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
