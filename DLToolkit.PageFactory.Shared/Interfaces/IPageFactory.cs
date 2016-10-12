using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// DLToolkit.PageFactory
	/// </summary>
	public interface IPageFactory : IPageFactoryCaching, IPageFactoryNavigation
	{
		/// <summary>
		/// Gets or sets the logger.
		/// </summary>
		/// <value>The logger.</value>
		IBaseLogger Logger { get; set; }

		/// <summary>
		/// Gets the page by model.
		/// </summary>
		/// <returns>The page by model.</returns>
		/// <param name="pageModel">Page model.</param>
		/// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
		IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModel) where TPageModel : class, IBasePageModel, new();

        /// <summary>
        /// Gets the Page PageModel.
        /// </summary>
        /// <returns>The page model.</returns>
        /// <param name="page">Page.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel, new();

		/// <summary>
		/// Replaces the Page PageModel.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="newPageModel">New page model.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel, new();
	}
}

