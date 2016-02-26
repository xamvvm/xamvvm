using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// DLToolkit.PageFactory
	/// </summary>
	public interface IPageFactory : IPageFactoryCaching, IPageFactoryNavigation, IPageFactoryMessaging
	{
		/// <summary>
		/// Gets the Page by PageModel.
		/// </summary>
		/// <returns>The page by page model.</returns>
		/// <param name="pageModelInstance">Page model instance.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModelInstance) where TPageModel : class, INotifyPropertyChanged;

        /// <summary>
        /// Gets the Page PageModel.
        /// </summary>
        /// <returns>The page model.</returns>
        /// <param name="page">Page.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Replaces the Page PageModel.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="newPageModel">New page model.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        void ReplacePageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Resets the Page PageModel.
		/// </summary>
		/// <param name = "page">Page.</param>
        void ResetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged;
	}
}

