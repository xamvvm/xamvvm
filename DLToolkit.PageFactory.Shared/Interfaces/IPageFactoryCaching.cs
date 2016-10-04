using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IPageFactory caching.
	/// </summary>
	public interface IPageFactoryCaching
	{
		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		IBasePage<TPageModel> GetPageFromCache<TPageModel>(TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel, new();

		/// <summary>
		/// Gets the page as new instance.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(TPageModel pageModel = null) where TPageModel : class, IBasePageModel, new();

		/// <summary>
		/// Removes the page from cache.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		bool RemovePageTypeFromCache<TPageModel>(string cacheKey = null) where TPageModel : class, IBasePageModel, new();

		/// <summary>
		/// Clears pages cache.
		/// </summary>
		void ClearCache();
	}
}

