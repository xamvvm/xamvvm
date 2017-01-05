using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Xamvvm
{
    public partial class XamvvmFormsFactory : IBaseFactoryCaching
    {
		public virtual IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			var pageModelType = typeof(TPageModel);
			var pageType = GetPageType(pageModelType);

			// check for DisableCacheAttribute
			var noCachePageModelAttr = pageModelType?.GetTypeInfo()?.GetCustomAttribute<DisableCacheAttribute>();
			var noCachePageAttr = pageType?.GetTypeInfo()?.GetCustomAttribute<DisableCacheAttribute>();
			if (noCachePageModelAttr != null || noCachePageAttr != null)
			{
				return GetPageAsNewInstance(pageModel);
			}

			var key = Tuple.Create(pageModelType, cacheKey);
			if (_pageCache.ContainsKey(key))
			{
				IncreaseCacheHits(key);
				var cachedPage = _pageCache[key] as IBasePage<TPageModel>;

				if (pageModel != null)
					SetPageModel(cachedPage, pageModel);

				return cachedPage;
			}

			var page = GetPageAsNewInstance(pageModel);

            // no longer needed because we add it to the cache above
			//if (_maxPageCacheItems > 0)
			//{
			//	RemoveUnusedPagesFromCache();
			//	_pageCache.Add(key, page);
			//	IncreaseCacheHits(key);
			//}

			return page;
		}

		public virtual IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(TPageModel pageModel = null) where TPageModel : class, IBasePageModel
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

            // cache key is always null because we specifically requested that it be a new instance
            string cacheKey = null;
            var key = Tuple.Create(pageModelType, cacheKey);
            // if it already exists, remove it since we are creating /registering a new instance
            if(_pageCache.ContainsKey(key))
            {
                _pageCache.Remove(key);
            }
            // also add it to the cache just for kicks
            if (_maxPageCacheItems > 0)
            {
                RemoveUnusedPagesFromCache();
                _pageCache.Add(key, page);
                IncreaseCacheHits(key);
            }

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
			// check for DisableCacheAttribute
			var pageType = GetPageType(pageModelType);
			var noCachePageModelAttr = pageModelType.GetTypeInfo().GetCustomAttribute<DisableCacheAttribute>();
			var noCachePageAttr = pageType.GetTypeInfo().GetCustomAttribute<DisableCacheAttribute>();
			if (noCachePageModelAttr != null || noCachePageAttr != null)
			{
				return GetPageAsNewInstance(pageModelType);
			}

			var key = Tuple.Create(pageModelType, cacheKey);

			if (_pageCache.ContainsKey(key))
			{
				IncreaseCacheHits(key);
				return _pageCache[key] as IBasePage<IBasePageModel>;
			}

			var page = GetPageAsNewInstance(pageModelType);

			if (_maxPageCacheItems > 0)
			{
				RemoveUnusedPagesFromCache();
				_pageCache.Add(key, page);
				IncreaseCacheHits(key);
			}

			return page;
		}

		public IBasePage<IBasePageModel> GetPageAsNewInstance(Type pageModelType)
		{
			var pageType = GetPageType(pageModelType);
			IBasePage<IBasePageModel> page;
			Func<object> pageCreationFunc;
			if (_pageCreation.TryGetValue(pageModelType, out pageCreationFunc))
			{
				page = pageCreationFunc() as IBasePage<IBasePageModel>;
			}
			else
				page = Activator.CreateInstance(pageType) as IBasePage<IBasePageModel>;

			Func<object> pageModelCreationFunc;
			if (_pageModelCreation.TryGetValue(pageModelType, out pageModelCreationFunc))
				SetPageModel(page, pageModelCreationFunc() as IBasePageModel);
			else
				SetPageModel(page, Activator.CreateInstance(pageModelType) as IBasePageModel);

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
				_pageCacheHits.Remove(key);
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
			_pageCacheHits.Clear();
		}

		internal void RemoveUnusedPagesFromCache()
		{
			if (_maxPageCacheItems <= 0)
				return;

			var ordered = _pageCacheHits
				.OrderByDescending(v => v.Value)
				.Skip(_maxPageCacheItems);

			foreach (var item in ordered)
			{
				_pageCache.Remove(item.Key);
				_pageCacheHits.Remove(item.Key);
			}
		}

		internal void IncreaseCacheHits(Tuple<Type, string> key)
		{
			int count;
			if (_pageCacheHits.TryGetValue(key, out count))
			{
				_pageCacheHits[key] = count + 1;
			}
			else
			{
				var minValue = _pageCacheHits.Values
					.OrderByDescending(v => v)
					.LastOrDefault();

				_pageCacheHits.Add(key, minValue == 0 ? 1 : minValue);
			}
		}
	}
}

