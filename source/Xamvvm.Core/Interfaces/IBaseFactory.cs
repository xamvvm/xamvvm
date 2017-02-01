using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Xamvvm
{
	/// <summary>
	/// Xamvvm
	/// </summary>
	public interface IBaseFactory : IBaseFactoryCaching, IBaseFactoryNavigation
	{

		/// <summary>
		/// Gets the page by model.
		/// </summary>
		/// <returns>The page by model.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModel) where TPageModel : class, IBasePageModel;

        /// <summary>
        /// Gets the Page PageModel.
        /// </summary>
        /// <returns>The page model.</returns>
        /// <param name="page">Page.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Replaces the Page PageModel.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="newPageModel">New page model.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Gets the current page.
		/// </summary>
		/// <value>The current page.</value>
		IBasePage<IBasePageModel> CurrentPage { get; }

		/// <summary>
		/// Gets the current page model.
		/// </summary>
		/// <value>The current page model.</value>
		IBasePageModel CurrentPageModel { get; }
	}
}

