using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    /// <summary>
    /// Page model extensions.
    /// </summary>
    public static class PageModelExtensions
    {
        /// <summary>
        /// Gets the page by page model.
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="pageModel">Page model.</param>
        /// <typeparam name="TPageModel">Page model type.</typeparam>
        public static IBasePage<TPageModel> GetPage<TPageModel>(this TPageModel pageModel) where TPageModel : class, IBasePageModel, new()
        {
            return PageFactory.Instance.GetPageByModel(pageModel);
        }

        /// <summary>
        /// Gets the page page model.
        /// </summary>
        /// <returns>The page model.</returns>
        /// <param name="page">Page.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        public static TPageModel GetPageModel<TPageModel>(this IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel, new()
        {
            return PageFactory.Instance.GetPageModel(page);
        }

		/// <summary>
		/// Sets the page page model.
		/// </summary>
		/// <returns>The page model.</returns>
		/// <param name="page">Page.</param>
		/// <param name="newPageModel">New page model.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> SetPageModel<TPageModel>(this IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel, new()
		{
			PageFactory.Instance.SetPageModel(page, newPageModel);

			return page;
		}

		/// <summary>
		/// Executes the action on the page model.
		/// </summary>
		/// <returns>The on page model.</returns>
		/// <param name="page">Page.</param>
		/// <param name="action">Action.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		public static IBasePage<TPageModel> ExecuteOnPageModel<TPageModel>(this IBasePage<TPageModel> page, Action<TPageModel> action) where TPageModel : class, IBasePageModel, new()
		{
			var model = page.GetPageModel();
			action?.Invoke(model);

			return page;
		}
    }
}

