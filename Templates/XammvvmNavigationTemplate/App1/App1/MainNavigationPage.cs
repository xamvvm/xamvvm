using Xamarin.Forms;
using Xamvvm;

namespace App1
{
    public class MainNavigationPage : NavigationPage, IBasePage<MainNavigationPageModel>
    {
        public MainNavigationPage(Page child) : base(child)
        {

        }
    }
}