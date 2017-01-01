using System;
using System.Reflection;

namespace Xamvvm
{
	public partial class XamvvmMockFactory : IBaseFactory
	{
		public virtual IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel setPageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			var pageModelType = typeof(TPageModel);
			var pageType = typeof(MockPage<>);

			// check for DisableCacheAttribute
			var noCachePageModelAttr = pageModelType.GetTypeInfo().GetCustomAttribute<DisableCacheAttribute>();
			var noCachePageAttr = pageType.GetTypeInfo().GetCustomAttribute<DisableCacheAttribute>();
			if (noCachePageModelAttr != null || noCachePageAttr != null)
			{
				return GetPageAsNewInstance(setPageModel);
			}

			var key = Tuple.Create(pageModelType, cacheKey);
			if (_pageCache.ContainsKey(key))
			{
				var cachedPage = _pageCache[key] as IBasePage<TPageModel>;

				if (setPageModel != null)
					SetPageModel(cachedPage, setPageModel);

				return cachedPage;
			}

			var page = GetPageAsNewInstance(setPageModel);
			_pageCache.Add(key, page);
			return page;
		}

		public virtual IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(TPageModel setPageModel = null) where TPageModel : class, IBasePageModel
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

			if (setPageModel != null)
			{
				SetPageModel(page, setPageModel);
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
			// check for DisableCacheAttribute
			var pageType = typeof(MockPage<>);
			var noCachePageModelAttr = pageModelType.GetTypeInfo().GetCustomAttribute<DisableCacheAttribute>();
			var noCachePageAttr = pageType.GetTypeInfo().GetCustomAttribute<DisableCacheAttribute>();
			if (noCachePageModelAttr != null || noCachePageAttr != null)
			{
				return GetPageAsNewInstance(pageModelType);
			}

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
