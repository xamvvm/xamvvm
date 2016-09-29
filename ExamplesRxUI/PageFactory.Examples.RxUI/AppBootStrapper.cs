#define automatic_registration


using DLToolkit.PageFactory;
using Logging;
using PageFactory.Examples.RxUI.ViewModels;
using PageFactory.Examples.RxUI.Views;
using ReactiveUI;
using Xamarin.Forms;

namespace PageFactory.Examples.RxUI
{
    public class AppBootstrapper : ReactiveObject
    {
        public AppBootstrapper()
        {
        }

        public Page CreateMainPage()
        {
            using (Log.Perf("InitFactory"))
            {


#if automatic_registration
                using (Log.Perf("InitFactory"))
                {
                    return new RxUIPageFactory().Init<MainPageViewModel, PFNavigationPage>();
                }
#elif manualViews_registration
                using (Log.Perf("InitFactory"))
                {
                             
                return new RxUIPageFactory().InitWithPageTypes<MainPageViewModel, PFNavigationPage>(
                    typeof(MainPage),
                    typeof(DemoListViewView));
                }
#elif manualTypes_registration             
                using (Log.Perf("Register"))
                {
                    RxUIPageFactory.RegisterViews<MainPageViewModel, MainPage>();
                    RxUIPageFactory.RegisterViews<DemoListViewViewModel, DemoListViewView>();
                }

                using (Log.Perf("InitFactory"))
                {
                    return new RxUIPageFactory().InitStatic<MainPageViewModel, PFNavigationPage>();

                }
#endif
            }

        }
    }
}