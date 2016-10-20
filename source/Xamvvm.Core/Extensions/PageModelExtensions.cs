using System;
using System.ComponentModel;

namespace Xamvvm
{
    /// <summary>
    /// Page model extensions.
    /// </summary>
    public static class PageModelExtensions
    {
        /// <summary>
        /// Gets the current page by page model.
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="pageModel">Page model.</param>
        /// <typeparam name="TPageModel">Page model type.</typeparam>
        public static IBasePage<TPageModel> GetCurrentPage<TPageModel>(this TPageModel pageModel) where TPageModel : class, IBasePageModel
        {
            return XamvvmCore.CurrentFactory.GetPageByModel(pageModel);
        }

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> GetPageFromCache<TPageModel>(this IBasePageModel currentPageModel, TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			return XamvvmCore.CurrentFactory.GetPageFromCache(pageModel, cacheKey);
		}

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="cacheKey">Cache key.</param>
		public static IBasePage<IBasePageModel> GetPageFromCache(this IBasePageModel currentPageModel, Type pageModelType, string cacheKey = null)
		{
			return XamvvmCore.CurrentFactory.GetPageFromCache(pageModelType, cacheKey);
		}

		/// <summary>
		/// Gets the page as new instance.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="currentPageModel">Current page model.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(this IBasePageModel currentPageModel, TPageModel pageModel = null) where TPageModel : class, IBasePageModel
		{
			return XamvvmCore.CurrentFactory.GetPageAsNewInstance(pageModel);
		}

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="pageModelType">Page model type.</param>
		public static IBasePage<IBasePageModel> GetPageAsNewInstance(this IBasePageModel currentPageModel, Type pageModelType)
		{
			return XamvvmCore.CurrentFactory.GetPageAsNewInstance(pageModelType);
		}

		/// <summary>
		/// Removes the page type from cache.
		/// </summary>
		/// <returns><c>true</c>, if page type from cache was removed, <c>false</c> otherwise.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static bool RemovePageTypeFromCache<TPageModel>(this IBasePageModel currentPageModel, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			return XamvvmCore.CurrentFactory.RemovePageTypeFromCache<TPageModel>(cacheKey);
		}

		/// <summary>
		/// Clears the page cache.
		/// </summary>
		/// <param name="currentPageModel">Current page model.</param>
		public static void ClearPageCache(this IBasePageModel currentPageModel)
		{
			XamvvmCore.CurrentFactory.ClearPageCache();
		}
    }
}

