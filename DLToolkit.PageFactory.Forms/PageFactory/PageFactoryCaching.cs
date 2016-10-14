using System;
using System.Linq;
using Xamarin.Forms;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IBaseFactoryCaching
    {
		public virtual IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			var pageModelType = typeof(TPageModel);
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
			RemoveUnusedPagesFromCache();
			_pageCache.Add(key, page);
			IncreaseCacheHits(key);
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
					navEventsPage.PageFactoryRemovingFromCache();
				
				_pageCache.Remove(key);
				_pageCacheHits.Remove(key);
				return true;
			}

			return false;
		}

		public void ClearPageCache()
		{
			foreach (var page in _pageCache.Values)
			{
				var navEventsPage = page as INavigationRemovingFromCache;
				if (navEventsPage != null)
					navEventsPage.PageFactoryRemovingFromCache();
			}

			_pageCache.Clear();
			_pageCacheHits.Clear();
		}

		internal void RemoveUnusedPagesFromCache()
		{
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

