using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace DLToolkit.PageFactory.Form.RxUI
{

        public class RxUIXamarinFormsPageFactory : XamarinFormsPageFactory
        {
	        //public override void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel, new()
         //   {
         //       base.SetPageModel(page, newPageModel);

         //       var rxPage = page as IViewFor<TPageModel>;

         //       if (rxPage != null)
         //       {
         //           rxPage.ViewModel = newPageModel;
         //       }
         //   }
            public RxUIXamarinFormsPageFactory(Application appInstance, int maxPageCacheItems = 6, bool automaticAssembliesDiscovery = true, params Assembly[] additionalPagesAssemblies) : base(appInstance, maxPageCacheItems, automaticAssembliesDiscovery, additionalPagesAssemblies)
            {
            }
        }

}
