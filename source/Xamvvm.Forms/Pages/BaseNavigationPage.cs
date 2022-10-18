using Xamarin.Forms;

namespace Xamvvm
{
	public class BaseNavigationPage<TPageModel> : NavigationPage, IBasePage<TPageModel> where TPageModel : class, IBasePageModel
	{
		public BaseNavigationPage() : base()
		{
		}

		public BaseNavigationPage(Page page) : base(page)
		{
		}
	}
}
