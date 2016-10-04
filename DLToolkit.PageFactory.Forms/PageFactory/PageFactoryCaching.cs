using System;
using Xamarin.Forms;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactoryCaching
    {
		public virtual IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel, new()
		{
			var pageModelType = typeof(TPageModel);
			var key = Tuple.Create(pageModelType, cacheKey);

			if (_pageCache.ContainsKey(key))
			{
				var cachedPage = _pageCache[key] as IBasePage<TPageModel>;

				if (pageModel != null)
					SetPageModel(cachedPage, pageModel);

				return cachedPage;
			}

			var page = GetPageAsNewInstance(pageModel);
			_pageCache.Add(key, page as IBasePage<IBasePageModel>);
			return page;
		}

		public virtual IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(TPageModel pageModel = null) where TPageModel : class, IBasePageModel, new()
		{
			var pageModelType = typeof(TPageModel);
			var pageType = GetPageType(pageModelType);
			IBasePage<TPageModel> page;
			Func<object> pageCreationFunc;
			if (_pageCreation.TryGetValue(pageModelType, out pageCreationFunc))
			{
				page = pageCreationFunc() as IBasePage<TPageModel>;
			}
			else
				page = Activator.CreateInstance(pageType) as IBasePage<TPageModel>;

			if (pageModel != null)
			{
				SetPageModel(page, pageModel);
			}
			else
			{
				Func<object> pageModelCreationFunc;
				if (_pageModelCreation.TryGetValue(pageModelType, out pageModelCreationFunc))
					SetPageModel(page, pageModelCreationFunc() as TPageModel);
				else
					SetPageModel(page, new TPageModel());
			}

			return page;
		}

		public virtual bool RemovePageTypeFromCache<TPageModel>(string cacheKey = null) where TPageModel : class, IBasePageModel, new()
		{
			var pageModelType = typeof(TPageModel);
			var key = Tuple.Create(pageModelType, cacheKey);

			IBasePage<IBasePageModel> page;
			if (_pageCache.TryGetValue(key, out page))
			{
				var navEventsPage = page as INavigationRemovingFromCache;
				if (navEventsPage != null)
					navEventsPage.PageFactoryRemovingFromCache();
				
				_pageCache.Remove(key);
				return true;
			}

			return false;
		}

		public void ClearCache()
		{
			foreach (var page in _pageCache.Values)
			{
				var navEventsPage = page as INavigationRemovingFromCache;
				if (navEventsPage != null)
					navEventsPage.PageFactoryRemovingFromCache();
			}

			_pageCache.Clear();
		}
	}
}

