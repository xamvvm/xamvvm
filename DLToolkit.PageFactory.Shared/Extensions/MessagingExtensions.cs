using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
    /// <summary>
    /// Messaging extensions.
    /// </summary>
    public static class MessagingExtensions
    {
        /// <summary>
        /// Sends the message to PageModel.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name = "sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="arg">Argument.</param>
        public static IBasePage<TPageModel> SendMessageToPageModel<TPageModel>(this IBasePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged, IMessagable
        {
            PF.Factory.SendMessageToPageModel(page, message, sender, arg);
            return page;
        }

        /// <summary>
        /// Sends the action to PageModel.
        /// </summary>
        /// <returns>The action to page model.</returns>
        /// <param name="page">Page.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        public static IBasePage<TPageModel> SendActionToPageModel<TPageModel>(this IBasePage<TPageModel> page, Action<TPageModel> action) where TPageModel : class, INotifyPropertyChanged, IMessagable
        {
            PF.Factory.SendActionToPageModel(page, action);
            return page;
        }
    }
}

