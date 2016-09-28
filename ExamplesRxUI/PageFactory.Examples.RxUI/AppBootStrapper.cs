using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLToolkit.PageFactory;
using Logging;
using PageFactory.Examples.RxUI.ViewModels;
using PageFactory.Examples.RxUI.Views;
using ReactiveUI;
using Splat;
using Xamarin.Forms;

namespace PageFactory.Examples.RxUI
{
    public class AppBootstrapper : ReactiveObject
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.
        public AppBootstrapper()
        {

            //Locator.CurrentMutable.Register(() => new MainPageViewModel(), typeof(IViewFor<MainPageViewModel>));
            //Locator.CurrentMutable.Register(() => new DemoListViewView(), typeof(IViewFor<DemoListViewViewModel>));
            //Locator.CurrentMutable.Register(() => new ListViewItemView(), typeof(IViewFor<DogsItemViewModel>));

        }

        public Page CreateMainPage()
        {
            using (Log.Perf("InitFactory"))
            {
                return new RxUIPageFactory().Init<MainPageViewModel,PFNavigationPage>();
                //return new RxUIPageFactory().InitWithPageTypes<MainPageViewModel, PFNavigationPage>(
                //    typeof(MainPage),
                //    typeof(DemoListViewView));
            }

        }
    }
}