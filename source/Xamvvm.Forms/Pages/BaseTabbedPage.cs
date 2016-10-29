using System;
using Xamarin.Forms;

namespace Xamvvm
{
	public class BaseTabbedPage<TPageModel> : TabbedPage, IBasePage<TPageModel> where TPageModel : class, IBasePageModel
	{
	}
}
