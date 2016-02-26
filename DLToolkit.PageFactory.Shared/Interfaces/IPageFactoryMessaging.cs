using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IPageFactory messaging.
	/// </summary>
	public interface IPageFactoryMessaging
	{
		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets page model.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBaseMessagablePage<TPageModel> GetMessagablePageFromCache<TPageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="resetPageModel">If set to <c>true</c> resets page model.</param>
        IBaseMessagablePage<INotifyPropertyChanged> GetMessagablePageFromCache(Type pageModelType, bool resetPageModel = false);

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBaseMessagablePage<TPageModel> GetMessagablePageAsNewInstance<TPageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
        IBaseMessagablePage<INotifyPropertyChanged> GetMessagablePageAsNewInstance(Type pageModelType, bool saveOrReplaceInCache = false);

		/// <summary>
		/// Gets the page by page model.
		/// </summary>
		/// <returns>The page by page model.</returns>
		/// <param name="pageModelInstance">Page model instance.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBaseMessagablePage<TPageModel> GetMessagablePageByModel<TPageModel>(TPageModel pageModelInstance) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Sends the message to Page.
		/// </summary>
		/// <returns><c>true</c>, if message to page model was received, <c>false</c> otherwise.</returns>
		/// <param name = "sender">Sender.</param>
		/// <param name="page">Page instance.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		/// <typeparam name="TPage">Page type.</typeparam>
        bool SendMessageToPage<TPageModel>(IBaseMessagablePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged;

        /// <summary>
        /// Sends the message to PageModel.
        /// </summary>
        /// <returns><c>true</c>, if message to page model was sent, <c>false</c> otherwise.</returns>
        /// <param name="page">Page.</param>
        /// <param name="message">Message.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="arg">Argument.</param>
        /// <typeparam name="TPage">The 1st type parameter.</typeparam>
        bool SendMessageToPageModel<TPageModel>(IBasePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged, IMessagable;
	}
}

