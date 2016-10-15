using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ReactiveUI;
using Xamarin.Forms;

namespace Xamvvm
{

	public class XFRxUIFactory : XamarinFormsPageFactory
	{
		public XFRxUIFactory(Application appInstance, int maxPageCacheItems = 6, bool automaticAssembliesDiscovery = true, params Assembly[] additionalPagesAssemblies) : base(appInstance, maxPageCacheItems, automaticAssembliesDiscovery, additionalPagesAssemblies)
		{
		}

		public override void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel)
		{
			base.SetPageModel(page, newPageModel);

			var rxPage = page as IViewFor<TPageModel>;
			if (rxPage != null)
			{
			   rxPage.ViewModel = newPageModel;
			}
		}
	}
}
