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
        /// Sends the message to PageModel.
        /// </summary>
        /// <returns><c>true</c>, if message to page model was sent, <c>false</c> otherwise.</returns>
        /// <param name="page">Page.</param>
        /// <param name="message">Message.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="arg">Argument.</param>
        /// <typeparam name="TPageModel">Page model.</typeparam>
        bool SendMessageToPageModel<TPageModel>(IBasePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged, IMessagable;

        /// <summary>
        /// Sends the action to page model.
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        void SendActionToPageModel<TPageModel>(IBasePage<TPageModel> page, Action<TPageModel> action) where TPageModel : class, INotifyPropertyChanged, IMessagable;
	}
}

