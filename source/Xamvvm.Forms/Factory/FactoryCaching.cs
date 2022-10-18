﻿using System;
using System.Linq;
using System.Reflection;

namespace Xamvvm
{
    public partial class XamvvmFormsFactory : IBaseFactoryCaching
    {
		public virtual IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel setPageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			var pageModelType = typeof(TPageModel);
			var pageType = GetPageType(pageModelType);

			// check for DisableCacheAttribute
			var noCachePageModelAttr = pageModelType?.GetTypeInfo()?.GetCustomAttribute<DisableCacheAttribute>();
			var noCachePageAttr = pageType?.GetTypeInfo()?.GetCustomAttribute<DisableCacheAttribute>();
			if (noCachePageModelAttr != null || noCachePageAttr != null)
			{
				return GetPageAsNewInstance(setPageModel);
			}

			var key = Tuple.Create(pageModelType, cacheKey);
			if (_pageCache.ContainsKey(key))
			{
				IncreaseCacheHits(key);
				var cachedPage = _pageCache[key] as IBasePage<TPageModel>;

				if (setPageModel != null)
					SetPageModel(cachedPage, setPageModel);

				return cachedPage;
			}

			var page = GetPageAsNewInstance(setPageModel);

			if (_maxPageCacheItems > 0)
			{
				RemoveUnusedPagesFromCache();
				_pageCache.Add(key, page);
				IncreaseCacheHits(key);
			}

			return page;
		}

		public virtual IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(TPageModel setPageModel = null) where TPageModel : class, IBasePageModel
		{
			var pageModelType = typeof(TPageModel);
			var pageType = GetPageType(pageModelType);

			IBasePage<TPageModel> page;
			Func<object> pageCreationFunc;
		    _pageCreation.TryGetValue(pageModelType, out pageCreationFunc);

            // first check if we have a registered PageCreation method for this pageModelType
            if (pageCreationFunc != null)
		    {                
		        page = pageCreationFunc() as IBasePage<TPageModel>;
		    }
		    else
		    {
                // if not check if it failed because there was no registered PageType
                // we cannot check this earlier because for NavgationPages we allow PageModels of the NavigationPage not having a custom NavigationPage type
                // in this case we register a default creation method, but we won't register a pageType
                // !!!!!!!!! Not sure if this is the correct way to do it
		        if (pageType == null)
		        {
		            throw new NoPageForPageModelRegisteredException("No PageType Registered for PageModel type: " + pageModelType);
		        }
		        page = XamvvmIoC.Resolve(pageType) as IBasePage<TPageModel>;

		        if (page == null)
		        {
		            throw new NoPageForPageModelRegisteredException("PageType not registered in IOC: " + pageType);
		        }
            }

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
					SetPageModel(page, XamvvmIoC.Resolve<TPageModel>());
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
				page = XamvvmIoC.Resolve(pageType) as IBasePage<IBasePageModel>;

			Func<object> pageModelCreationFunc;
			if (_pageModelCreation.TryGetValue(pageModelType, out pageModelCreationFunc))
				SetPageModel(page, pageModelCreationFunc() as IBasePageModel);
			else
				SetPageModel(page, XamvvmIoC.Resolve(pageModelType) as IBasePageModel);

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

