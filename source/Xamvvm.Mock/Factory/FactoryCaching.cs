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

		public IBasePage<IBasePageModel> GetPageFromCache(Type pageModelType, string cacheKey = null)
		{
			var key = Tuple.Create(pageModelType, cacheKey);

			if (_pageCache.ContainsKey(key))
			{
				return _pageCache[key] as IBasePage<IBasePageModel>;
			}

			var page = GetPageAsNewInstance(pageModelType);
			_pageCache.Add(key, page);
			return page;
		}

		public IBasePage<IBasePageModel> GetPageAsNewInstance(Type pageModelType)
		{
			IBasePage<IBasePageModel> page;
			Func<object> pageCreationFunc;
			if (_pageCreation.TryGetValue(pageModelType, out pageCreationFunc))
			{
				page = pageCreationFunc() as IBasePage<IBasePageModel>;
			}
			else
			{
				var pageType = typeof(MockPage<>);
				Type[] typeArgs = { pageModelType };
				var pageGenericType = pageType.MakeGenericType(typeArgs);
				page = Activator.CreateInstance(pageGenericType) as IBasePage<IBasePageModel>;
			}

			Func<object> pageModelCreationFunc;
			if (_pageModelCreation.TryGetValue(pageModelType, out pageModelCreationFunc))
				SetPageModel(page, pageModelCreationFunc() as IBasePageModel);
			else
				SetPageModel(page, Activator.CreateInstance(pageModelType) as IBasePageModel);

			return page;
		}

		public virtual bool RemovePageTypeFromCache<TPageModel>(string cacheKey = null) where TPageModel : class, IBasePageModel
		{
            LastAction = XammvvmAction.PageRemovedFromCache;

			var pageModelType = typeof(TPageModel);
			var key = Tuple.Create(pageModelType, cacheKey);

			object page;
			if (_pageCache.TryGetValue(key, out page))
			{
				var navEventsPage = page as INavigationRemovingFromCache;
				if (navEventsPage != null)
					navEventsPage.NavigationRemovingFromCache();

				_pageCache.Remove(key);

			    TargetPageModel = (page as IBasePage<TPageModel>).GetPageModel();
			    LastActionSuccess = true;

				return true;
			}

		    LastActionSuccess = false;
			return false;
		}

		public virtual void ClearPageCache()
		{
		    TargetPageModel = null;
            LastAction = XammvvmAction.CacheCleared;

            foreach (var page in _pageCache.Values)
			{
				var navEventsPage = page as INavigationRemovingFromCache;
				if (navEventsPage != null)
					navEventsPage.NavigationRemovingFromCache();
			}

			_pageCache.Clear();

		    LastActionSuccess = true;
		}
	}
}
