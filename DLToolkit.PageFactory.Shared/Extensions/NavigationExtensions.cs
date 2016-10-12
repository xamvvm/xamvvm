using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
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
			var currentPage = PageFactory.Instance.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);

				return PageFactory.Instance.PushPageAsync(currentPage, pageToPush, animated);
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
			var currentPage = PageFactory.Instance.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				var pageToPush = PageFactory.Instance.GetPageFromCache<TPageModel>(cacheKey: cacheKey);

				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);

				return PageFactory.Instance.PushPageAsync(currentPage, pageToPush, animated);
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
			var currentPage = PageFactory.Instance.GetPageByModel(currentPageModel);

			if (currentPage != null)
			{
				if (executeOnPageModel != null)
					pageToPush.ExecuteOnPageModel(executeOnPageModel);
				
				return PageFactory.Instance.PushModalPageAsync(currentPage, pageToPush, animated);
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
			var page = PageFactory.Instance.GetPageByModel(pageModel);

			if (page != null)
				return PageFactory.Instance.PopPageAsync(page, animated);
					              
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
			var page = PageFactory.Instance.GetPageByModel(pageModel);

			if (page != null)
				return PageFactory.Instance.PopModalPageAsync(page, animated);

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
			var currentPage = PageFactory.Instance.GetPageByModel(currentPageModel);

			if (currentPage != null)
				return PageFactory.Instance.RemovePageAsync(currentPage, pageToRemove);

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
			var currentPage = PageFactory.Instance.GetPageByModel(currentPageModel);

			if (currentPage != null)
				return PageFactory.Instance.PopPagesToRootAsync(currentPage, clearCache, animated);

			return Task.FromResult(false);
        }
    }
}

