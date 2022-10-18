using System;

namespace Xamvvm
{
	/// <summary>
	/// IBaseFactory caching.
	/// </summary>
	public interface IBaseFactoryCaching
	{
		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="setPageModel">Page model.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel setPageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="cacheKey">Cache key.</param>
		IBasePage<IBasePageModel> GetPageFromCache(Type pageModelType, string cacheKey = null);

		/// <summary>
		/// Gets the page as new instance.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="setPageModel">Page model.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(TPageModel setPageModel = null) where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Gets the page as new instance.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="pageModelType">Page model type.</param>
		IBasePage<IBasePageModel> GetPageAsNewInstance(Type pageModelType);

		/// <summary>
		/// Removes the page from cache.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		bool RemovePageTypeFromCache<TPageModel>(string cacheKey = null) where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Clears pages cache.
		/// </summary>
		void ClearPageCache();
	}
}

