using System;
using Xamarin.Forms;

namespace Xamvvm
{
    public partial class XamvvmFormsFactory : IBaseFactory
    {
		IBaseLogger logger;
		public IBaseLogger Logger
		{
			get
			{
				if (logger == null)
					logger = new BaseLogger();

				return logger;
			}

			set
			{
				logger = value;
			}
		}

		public virtual IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModel) where TPageModel : class, IBasePageModel
		{
			object page = null;
			_weakPageCache.TryGetValue(pageModel, out page);
			return page as IBasePage<TPageModel>;
		}

		public virtual TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel
		{
			var xfPage = page as Page;
			return xfPage?.BindingContext as TPageModel;
		}

		public virtual void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel
		{
			((Page)page).BindingContext = newPageModel;
			AddToWeakCacheIfNotExists(page, newPageModel);
		}

		//TODO
		public IBasePage<IBasePageModel> CurrentPage { get; protected set; }

		//TODO
		public IBasePageModel CurrentPageModel { get; protected set; }
    }
}

