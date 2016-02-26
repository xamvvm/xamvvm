using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    public static class MessagingExtensions
    {
        /// <summary>
        /// Sends the message to Page.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name = "sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="arg">Argument.</param>
        public static IBaseMessagablePage<TPageModel> SendMessageToPage<TPageModel>(this IBaseMessagablePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.SendMessageToPage(page, message, sender, arg);
            return page;
        }

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
        /// Sends the message to PageModel.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name = "sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="arg">Argument.</param>
        public static IBaseMessagablePage<TPageModel> SendMessageToPageModel<TPageModel>(this IBaseMessagablePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged, IMessagable
        {
            PF.Factory.SendMessageToPageModel(page, message, sender, arg);
            return page;
        }

        /// <summary>
        /// Sends the message to Page and PageModel.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name = "sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="arg">Argument.</param>
        public static IBaseMessagablePage<TPageModel> SendMessageToPageAndPageModel<TPageModel>(this IBaseMessagablePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged, IMessagable
        {
            PF.Factory.SendMessageToPage(page, message, sender, arg);
            PF.Factory.SendMessageToPageModel(page, message, sender, arg);
            return page;
        }
    }
}

