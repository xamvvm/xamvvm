using System;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// Page extensions.
	/// </summary>
	public static class PageExtensions
	{
		/// <summary>
		/// Gets the page page model.
		/// </summary>
		/// <returns>The page model.</returns>
		/// <param name="page">Page.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static TPageModel GetPageModel<TPageModel>(this IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel
		{
			return PageFactory.Current.GetPageModel(page);
		}

		/// <summary>
		/// Sets the page page model.
		/// </summary>
		/// <returns>The page model.</returns>
		/// <param name="page">Page.</param>
		/// <param name="newPageModel">New page model.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> SetPageModel<TPageModel>(this IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel
		{
			PageFactory.Current.SetPageModel(page, newPageModel);

			return page;
		}

		/// <summary>
		/// Executes the action on the page model.
		/// </summary>
		/// <returns>The on page model.</returns>
		/// <param name="page">Page.</param>
		/// <param name="action">Action.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> ExecuteOnPageModel<TPageModel>(this IBasePage<TPageModel> page, Action<TPageModel> action) where TPageModel : class, IBasePageModel
		{
			var model = page.GetPageModel();
			action?.Invoke(model);

			return page;
		}

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="currentPage">Current page.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> GetPageFromCache<TPageModel>(this IBasePage<TPageModel> currentPage, TPageModel pageModel = null, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			return PageFactory.Current.GetPageFromCache(pageModel, cacheKey);
		}

		/// <summary>
		/// Gets the page as new instance.
		/// Optionally provide a page model (else will be set automatically)
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="currentPage">Current page.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(this IBasePage<IBasePageModel> currentPage, TPageModel pageModel = null) where TPageModel : class, IBasePageModel
		{
			return PageFactory.Current.GetPageAsNewInstance(pageModel);
		}

		/// <summary>
		/// Removes the page type from cache.
		/// </summary>
		/// <returns><c>true</c>, if page type from cache was removed, <c>false</c> otherwise.</returns>
		/// <param name="currentPage">Current page.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static bool RemovePageTypeFromCache<TPageModel>(this IBasePage<IBasePageModel> currentPage, string cacheKey = null) where TPageModel : class, IBasePageModel
		{
			return PageFactory.Current.RemovePageTypeFromCache<TPageModel>(cacheKey);
		}

		/// <summary>
		/// Clears the page cache.
		/// </summary>
		/// <param name="currentPage">Current page.</param>
		public static void ClearPageCache(this IBasePage<IBasePageModel> currentPage)
		{
			PageFactory.Current.ClearPageCache();
		}
	}
}
