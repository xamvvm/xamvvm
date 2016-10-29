using System;
using Xamarin.Forms;

namespace Xamvvm
{
	public class BaseMasterDetailPage<TPageModel> : MasterDetailPage, IBasePage<TPageModel> where TPageModel : class, IBasePageModel
	{
	}
}
