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
		/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		IBasePage<IBaseMessagable> GetMessagablePageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged, new();

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="viewModelType">View model type.</param>
		/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
		IBasePage<IBaseMessagable> GetMessagablePageFromCache(Type viewModelType, bool resetViewModel = false);

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		IBasePage<IBaseMessagable> GetMessagablePageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged, new();

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="viewModelType">View model type.</param>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
		IBasePage<IBaseMessagable> GetMessagablePageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false);

		/// <summary>
		/// Gets the page by view model.
		/// </summary>
		/// <returns>The page by view model.</returns>
		/// <param name="viewModelInstance">View model instance.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		IBasePage<IBaseMessagable> GetMessagablePageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged, new();

		/// <summary>
		/// Sends the message to page only.
		/// </summary>
		/// <returns><c>true</c>, if message to view model was received, <c>false</c> otherwise.</returns>
		/// <param name = "sender">Sender.</param>
		/// <param name="page">Page instance.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		/// <typeparam name="TPage">Page type.</typeparam>
		bool SendMessageToPage<TPage>(TPage page, string message, object sender = null, object arg = null) where TPage : class, IBasePage<INotifyPropertyChanged>;

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <returns><c>true</c>, if message to view model was received, <c>false</c> otherwise.</returns>
		/// <param name = "consumer">Message consumer.</param>
		/// <param name = "sender">Sender.</param>
		/// <param name="page">Page instance.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		/// <typeparam name="TPage">Page type.</typeparam>
		bool SendMessageByPage<TPage>(MessageConsumer consumer, TPage page, string message, object sender = null, object arg = null) where TPage : class, IBasePage<IBaseMessagable>;

		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <returns><c>true</c>, if message to view model was received, <c>false</c> otherwise.</returns>
		/// <param name = "consumer">Message consumer.</param>
		/// <param name = "sender">Sender.</param>
		/// <param name="viewModelInstance">View model instance.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		bool SendMessageByViewModel<TViewModel>(MessageConsumer consumer, TViewModel viewModelInstance, string message, object sender = null, object arg = null) where TViewModel : class, INotifyPropertyChanged, IBaseMessagable, new();

		/// <summary>
		/// Sends the message to cached page.
		/// </summary>
		/// <returns><c>true</c>, if message to cached view model was received, <c>false</c> otherwise.</returns>
		/// <param name = "consumer">Message consumer.</param>
		/// <param name = "sender">Sender.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		/// <param name="createPageIfNotExists">If set to <c>true</c> creates page instance if not exists in cache.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		bool SendMessageToCached<TViewModel>(MessageConsumer consumer, string message, object sender = null, object arg = null, bool createPageIfNotExists = true) where TViewModel : class, INotifyPropertyChanged, IBaseMessagable, new();
	}
}

