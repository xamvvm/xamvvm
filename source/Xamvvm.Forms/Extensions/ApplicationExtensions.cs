using Xamarin.Forms;

namespace Xamvvm
{
	public static class ApplicationExtensions
	{
		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="app">Current Application.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> GetPageFromCache<TPageModel>(this Application app, TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			return XamvvmCore.CurrentFactory.GetPageFromCache(pageModel, cacheKey);
		}

		/// <summary>
		/// Gets the page as new instance.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="app">Current Application.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(this Application app, TPageModel pageModel = null) where TPageModel : class, IBasePageModel
		{
			return XamvvmCore.CurrentFactory.GetPageAsNewInstance(pageModel);
		}

		/// <summary>
		/// Removes the page type from cache.
		/// </summary>
		/// <returns><c>true</c>, if page type from cache was removed, <c>false</c> otherwise.</returns>
		/// <param name="app">Current Application.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static bool RemovePageTypeFromCache<TPageModel>(this Application app, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			return XamvvmCore.CurrentFactory.RemovePageTypeFromCache<TPageModel>(cacheKey);
		}

		/// <summary>
		/// Clears the page cache.
		/// </summary>
		/// <param name="currentPage">Current page.</param>
		public static void ClearPageCache(this IBasePage<IBasePageModel> currentPage)
		{
			XamvvmCore.CurrentFactory.ClearPageCache();
		}
	}
}
