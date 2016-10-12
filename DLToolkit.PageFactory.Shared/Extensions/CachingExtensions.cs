using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    /// <summary>
    /// Caching extensions.
    /// </summary>
    public static class CachingExtensions
    {
		/// <summary>
		/// Removes the page type from cache if type exists in cache.
		/// </summary>
		/// <returns><c>true</c>, if type from cache was removed, <c>false</c> otherwise.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static bool RemoveTypeFromCache<TPageModel>(this TPageModel pageModel, string cacheKey = null) where TPageModel : class, IBasePageModel
        {
			return PageFactory.Current.RemovePageTypeFromCache<TPageModel>(cacheKey);
        }
    }
}

