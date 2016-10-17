using System;
namespace Xamvvm
{
	public partial class XamvvmMockFactory : IBaseFactory
	{
		public virtual IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
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
			_pageCache.Add(key, page);
			return page;
		}

		public virtual IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(TPageModel pageModel = null) where TPageModel : class, IBasePageModel
		{
			var pageModelType = typeof(TPageModel);

			IBasePage<TPageModel> page;
			Func<object> pageCreationFunc;
			if (_pageCreation.TryGetValue(pageModelType, out pageCreationFunc))
			{
				page = pageCreationFunc() as IBasePage<TPageModel>;
			}
			else
				page = new MockPage<TPageModel>();

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
					SetPageModel(page, Activator.CreateInstance<TPageModel>());
			}

			return page;
		}

		public virtual bool RemovePageTypeFromCache<TPageModel>(string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			var pageModelType = typeof(TPageModel);
			var key = Tuple.Create(pageModelType, cacheKey);

			object page;
			if (_pageCache.TryGetValue(key, out page))
			{
				var navEventsPage = page as INavigationRemovingFromCache;
				if (navEventsPage != null)
					navEventsPage.NavigationRemovingFromCache();

				_pageCache.Remove(key);

				return true;
			}

			return false;
		}

		public virtual void ClearPageCache()
		{
			foreach (var page in _pageCache.Values)
			{
				var navEventsPage = page as INavigationRemovingFromCache;
				if (navEventsPage != null)
					navEventsPage.NavigationRemovingFromCache();
			}

			_pageCache.Clear();
		}
	}
}
