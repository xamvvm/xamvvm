namespace Xamvvm
{
	public partial class XamvvmMockFactory : IBaseFactory
	{

		public virtual IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModel) where TPageModel : class, IBasePageModel
		{
			object page = null;
			_weakPageCache.TryGetValue(pageModel, out page);
		    if (page == null)
		    {
		        return  new MockPage<TPageModel>();
		    }
			return page as IBasePage<TPageModel>;
		}

		public virtual TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel
		{
			var xfPage = page as MockPage<TPageModel>;
			return xfPage?.BindingContext;
		}

		public virtual void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel
		{
			((MockPage<TPageModel>)page).BindingContext = newPageModel;
			AddToWeakCacheIfNotExists(page, newPageModel);
            LastAction = XammvvmAction.PageModelChanged;
		    TargetPageModel = newPageModel;
		    LastActionSuccess = true;
		}
	}
}
