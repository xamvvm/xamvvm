using System;
using Xamarin.Forms;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactory
    {
		public virtual IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModel) where TPageModel : class, IBasePageModel, new()
		{
			IBasePage<IBasePageModel> page = null;
			_weakPageCache.TryGetValue(pageModel, out page);
			return page as IBasePage<TPageModel>;
		}

		public virtual TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel, new()
		{
			var xfPage = page as Page;
			return xfPage?.BindingContext as TPageModel;
		}

		public virtual void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel, new()
		{
			((Page)page).BindingContext = newPageModel;
			AddToWeakCacheIfNotExists(page, newPageModel);
		}
    }
}

