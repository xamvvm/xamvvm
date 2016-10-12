using System;
using DLToolkit.PageFactory;
using Xamarin.Forms;

namespace Examples
{
	public class MainNavigationPage : NavigationPage, IBasePage<MainNavigationPageModel>
	{
		public MainNavigationPage(Page root) : base(root)
		{
		}
	}

	public class MainNavigationPageModel : BasePageModel
	{
	}
}
