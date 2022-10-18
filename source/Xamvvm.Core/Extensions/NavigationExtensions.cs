using System;
using System.Threading.Tasks;

namespace Xamvvm
{
    /// <summary>
    /// Navigation extensions.
    /// </summary>
    public static class NavigationExtensions
    {
		/// <summary>
		/// Pushes the page into current navigation stack.
		/// </summary>
		/// <returns>The page async.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="pageToPush">Page to push.</param>
		/// <param name="executeOnPageModel">Execute on page model.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TCurrentPageModel">The 1st type parameter.</typeparam>
		/// <typeparam name="TPageModel">The 2nd type parameter.</typeparam>		
		public static Task<bool> PushPageAsync<TCurrentPageModel, TPageModel>(this TCurrentPageModel currentPageModel, IBasePage<TPageModel> pageToPush, Action<TPageModel> executeOnPageModel = null,  bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
        {
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);

				return XamvvmCore.CurrentFactory.PushPageAsync(currentPage, pageToPush, animated);
			}

			return Task.FromResult(false);
        }

		/// <summary>
		/// Pushes the cached page into current navigation stack.
		/// </summary>
		/// <returns>The page from cache async.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="executeOnPageModel">Execute on page model.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static Task<bool> PushPageFromCacheAsync<TPageModel>(this IBasePageModel currentPageModel, Action<TPageModel> executeOnPageModel = null, string cacheKey = null, bool animated = true) where TPageModel : class, IBasePageModel
		{
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				var pageToPush = XamvvmCore.CurrentFactory.GetPageFromCache<TPageModel>(cacheKey: cacheKey);

				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);

				return XamvvmCore.CurrentFactory.PushPageAsync(currentPage, pageToPush, animated);
			}

			return Task.FromResult(false);
		}

		/// <summary>
		/// Pushes the modal cached page as current navigation stack.
		/// </summary>
		/// <returns>The page from cache async.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="executeOnPageModel">Execute on page model.</param>
		/// <param name="cacheKey">Cache key.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static Task<bool> PushModalPageFromCacheAsync<TPageModel>(this IBasePageModel currentPageModel, Action<TPageModel> executeOnPageModel = null, string cacheKey = null, bool animated = true) where TPageModel : class, IBasePageModel
		{
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				var pageToPush = XamvvmCore.CurrentFactory.GetPageFromCache<TPageModel>(cacheKey: cacheKey);

				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);

				return XamvvmCore.CurrentFactory.PushModalPageAsync(currentPage, pageToPush, animated);
			}

			return Task.FromResult(false);
		}

		/// <summary>
		/// Pushs the page as new instance into current navigation stack.
		/// </summary>
		/// <returns>The page as new instance async.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="executeOnPageModel">Execute on page model.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static Task<bool> PushPageAsNewInstanceAsync<TPageModel>(this IBasePageModel currentPageModel, Action<TPageModel> executeOnPageModel = null, bool animated = true) where TPageModel : class, IBasePageModel
		{
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				var pageToPush = XamvvmCore.CurrentFactory.GetPageAsNewInstance<TPageModel>();

				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);

				return XamvvmCore.CurrentFactory.PushPageAsync(currentPage, pageToPush, animated);
			}

			return Task.FromResult(false);
		}

		/// <summary>
		/// Pushs the page as new instance into current navigation stack.
		/// </summary>
		/// <returns>The page as new instance async.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="executeOnPageModel">Execute on page model.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static Task<bool> PushModalPageAsNewInstanceAsync<TPageModel>(this IBasePageModel currentPageModel, Action<TPageModel> executeOnPageModel = null, bool animated = true) where TPageModel : class, IBasePageModel
		{
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				var pageToPush = XamvvmCore.CurrentFactory.GetPageAsNewInstance<TPageModel>();

				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);

				return XamvvmCore.CurrentFactory.PushModalPageAsync(currentPage, pageToPush, animated);
			}

			return Task.FromResult(false);
		}
 
		/// <summary>
		/// Pushes the modal page into current navigation stack.
		/// </summary>
		/// <returns>The modal page async.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="pageToPush">Page to push.</param>
		/// <param name="executeOnPageModel">Execute on page model.</param> 
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TCurrentPageModel">The 1st type parameter.</typeparam>
		/// <typeparam name="TPageModel">The 2nd type parameter.</typeparam>
        public static Task<bool> PushModalPageAsync<TCurrentPageModel, TPageModel>(this TCurrentPageModel currentPageModel, IBasePage<TPageModel> pageToPush, Action<TPageModel> executeOnPageModel = null, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);
				
				return XamvvmCore.CurrentFactory.PushModalPageAsync(currentPage, pageToPush, animated);
			}

			return Task.FromResult(false);
		}

		/// <summary>
		/// Pops the page from current navigation stack.
		/// </summary>
		/// <returns>The page async.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static Task<bool> PopPageAsync<TPageModel>(this TPageModel pageModel, bool animated = true)  where TPageModel : class, IBasePageModel
        {
			var page = XamvvmCore.CurrentFactory.GetPageByModel(pageModel);

			if (page != null)
				return XamvvmCore.CurrentFactory.PopPageAsync(page, animated);
					              
            return Task.FromResult(false);
        }

		/// <summary>
		/// Pops the modal page from current navigation stack.
		/// </summary>
		/// <returns>The modal page async.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static Task<bool> PopModalPageAsync<TPageModel>(this TPageModel pageModel, bool animated = true) where TPageModel : class, IBasePageModel
		{
			var page = XamvvmCore.CurrentFactory.GetPageByModel(pageModel);

			if (page != null)
				return XamvvmCore.CurrentFactory.PopModalPageAsync(page, animated);

			return Task.FromResult(false);
		}

		/// <summary>
		/// Removes the page from current navigation stack.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="pageToRemove">Page to remove.</param>
		/// <typeparam name="TCurrentPageModel">The 1st type parameter.</typeparam>
		/// <typeparam name="TPageModel">The 2nd type parameter.</typeparam>
		public static Task<bool> RemovePageAsync<TCurrentPageModel, TPageModel>(this TCurrentPageModel currentPageModel, IBasePage<TPageModel> pageToRemove) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
        {
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
				return XamvvmCore.CurrentFactory.RemovePageAsync(currentPage, pageToRemove);

			return Task.FromResult(false);
        }

		/// <summary>
		/// Pops all pages to root in current navigation stack.
		/// </summary>
		/// <returns>The pages to root async.</returns>
		/// <param name="currentPageModel">Current page model.</param>
		/// <param name="clearCache">If set to <c>true</c> clear cache.</param>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		/// <typeparam name="TCurrentPageModel">The 1st type parameter.</typeparam>
		public static Task<bool> PopPagesToRootAsync<TCurrentPageModel>(this TCurrentPageModel currentPageModel, bool clearCache = false, bool animated = true)  where TCurrentPageModel : class, IBasePageModel
        {
			var currentPage = XamvvmCore.CurrentFactory.GetPageByModel(currentPageModel);

			if (currentPage != null)
				return XamvvmCore.CurrentFactory.PopPagesToRootAsync(currentPage, clearCache, animated);

			return Task.FromResult(false);
        }


        /// <summary>
        /// Sets the new root and resets based on PageModel
        /// </summary>
        /// <param name="currentPageModel">Current page model.</param>
		/// <param name="clearCache">Clear cache.</param>
        public static Task<bool> SetNewRootAndResetAsync<TNewRootPageModel>(this IBasePageModel currentPageModel, bool clearCache = true) where TNewRootPageModel : class, IBasePageModel
        {
            return XamvvmCore.CurrentFactory.SetNewRootAndResetAsync<TNewRootPageModel>(clearCache);
        }

    }
}

