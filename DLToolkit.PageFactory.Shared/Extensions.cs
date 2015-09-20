using System;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// PageFactory Extensions helpers
	/// </summary>
	public static class Extensions
	{
		#region Factory

		/// <summary>
		/// Gets the page by view model.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="viewModel">View model.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		public static IBasePage<INotifyPropertyChanged> GetPage<TViewModel>(this TViewModel viewModel) where TViewModel : class, INotifyPropertyChanged, new()
		{
			return PageFactory.Factory.GetPageByViewModel(viewModel);
		}

		/// <summary>
		/// Gets the messagable page by view model.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="viewModel">View model.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		public static IBasePage<IBaseMessagable> GetMessagablePage<TViewModel>(this TViewModel viewModel) where TViewModel : class, IBaseMessagable, new()
		{
			return PageFactory.Factory.GetMessagablePageByViewModel(viewModel);
		}

		/// <summary>
		/// Resets page view model.
		/// </summary>
		/// <param name = "page">Page.</param>
		public static IBasePage<INotifyPropertyChanged> ResetViewModel(this IBasePage<INotifyPropertyChanged> page)
		{
			PageFactory.Factory.ResetPageViewModel(page);
			return page;
		}

		/// <summary>
		/// Resets page view model.
		/// </summary>
		/// <param name = "page">Page.</param>
		public static IBasePage<IBaseMessagable> ResetViewModel(this IBasePage<IBaseMessagable> page)
		{
			PageFactory.Factory.ResetPageViewModel(page);
			return page;
		}

		/// <summary>
		/// Replaces page view model.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="newViewModel">New view model.</param>
		public static IBasePage<INotifyPropertyChanged> ReplaceViewModel<TViewModel>(this IBasePage<INotifyPropertyChanged> page, TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged, new()
		{
			PageFactory.Factory.ReplacePageViewModel(page, newViewModel);
			return page;
		}

		/// <summary>
		/// Replaces page view model.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="newViewModel">New view model.</param>
		public static IBasePage<IBaseMessagable> ReplaceViewModel<TViewModel>(this IBasePage<IBaseMessagable> page, TViewModel newViewModel) where TViewModel : class, IBaseMessagable, new()
		{
			PageFactory.Factory.ReplacePageViewModel(page, newViewModel);
			return page;
		}

		#endregion

		#region Navigation

		/// <summary>
		/// Pushes the page into navigation stack.
		/// </summary>
		/// <param name="page">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<INotifyPropertyChanged> PushPage(this IBasePage<INotifyPropertyChanged> page, bool animated = true)
		{
			PageFactory.Factory.PushPageAsync(page, animated);
			return page;
		}

		/// <summary>
		/// Pushes the messagable page into navigation stack.
		/// </summary>
		/// <param name="page">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<IBaseMessagable> PushPage(this IBasePage<IBaseMessagable> page, bool animated = true)
		{
			PageFactory.Factory.PushPageAsync(page, animated);
			return page;
		}

		/// <summary>
		/// Pushes the page as modal.
		/// </summary>
		/// <param name="page">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<INotifyPropertyChanged> PushModalPage(this  IBasePage<INotifyPropertyChanged> page, bool animated = true)
		{
			PageFactory.Factory.PushModalPageAsync(page, animated);
			return page;
		}

		/// <summary>
		/// Pushes the messagable page as modal.
		/// </summary>
		/// <param name="page">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<IBaseMessagable> PushModalPage(this  IBasePage<IBaseMessagable> page, bool animated = true)
		{
			PageFactory.Factory.PushModalPageAsync(page, animated);
			return page;
		}

		/// <summary>
		/// Pops the page from navigation stack.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<INotifyPropertyChanged> PopPage(this  IBasePage<INotifyPropertyChanged> page, bool animated = true)
		{
			PageFactory.Factory.PopPageAsync(false, animated);
			return page;
		}

		/// <summary>
		/// Pops the messagable page from navigation stack.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<IBaseMessagable> PopPage(this  IBasePage<IBaseMessagable> page, bool animated = true)
		{
			PageFactory.Factory.PopPageAsync(false, animated);
			return page;
		}

		/// <summary>
		/// Pops the modal page.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<INotifyPropertyChanged> PopModalPage(this  IBasePage<INotifyPropertyChanged> page, bool animated = true)
		{
			PageFactory.Factory.PopModalPageAsync(false, animated);
			return page;
		}

		/// <summary>
		/// Pops the messagable modal page.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<IBaseMessagable> PopModalPage(this  IBasePage<IBaseMessagable> page, bool animated = true)
		{
			PageFactory.Factory.PopModalPageAsync(false, animated);
			return page;
		}

		/// <summary>
		/// Removes page from Navigation.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="page">Page.</param>
		public static IBasePage<INotifyPropertyChanged> RemovePage(this  IBasePage<INotifyPropertyChanged> page)
		{
			PageFactory.Factory.RemovePage(page);
			return page;
		}

		/// <summary>
		/// Removes page from Navigation.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="page">Page.</param>
		public static IBasePage<IBaseMessagable> RemovePage(this  IBasePage<IBaseMessagable> page)
		{
			PageFactory.Factory.RemovePage(page);
			return page;
		}

		/// <summary>
		/// Inserts the page before another page in Navigation stack.
		/// </summary>
		/// <returns>The page before.</returns>
		/// <param name="page">Page.</param>
		/// <param name="before">Before.</param>
		public static IBasePage<INotifyPropertyChanged> InsertPageBefore(this  IBasePage<INotifyPropertyChanged> page, IBasePage<INotifyPropertyChanged> before)
		{
			PageFactory.Factory.InsertPageBefore(page, before);
			return page;
		}

		/// <summary>
		/// Inserts the page before another page in Navigation stack.
		/// </summary>
		/// <returns>The page before.</returns>
		/// <param name="page">Page.</param>
		/// <param name="before">Before.</param>
		public static IBasePage<IBaseMessagable> InsertPageBefore(this  IBasePage<IBaseMessagable> page, IBasePage<INotifyPropertyChanged> before)
		{
			PageFactory.Factory.InsertPageBefore(page, before);
			return page;
		}

		/// <summary>
		/// Pops the pages to root.
		/// </summary>
		/// <returns>Page.</returns>
		/// <param name="page">Page.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<INotifyPropertyChanged> PopPagesToRoot(this  IBasePage<INotifyPropertyChanged> page, bool animated = true)
		{
			PageFactory.Factory.PopPagesToRootAsync(false, animated);
			return page;
		}

		/// <summary>
		/// Pops the pages to root.
		/// </summary>
		/// <returns>Page.</returns>
		/// <param name="page">Page.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		public static IBasePage<IBaseMessagable> PopPagesToRoot(this  IBasePage<IBaseMessagable> page, bool animated = true)
		{
			PageFactory.Factory.PopPagesToRootAsync(false, animated);
			return page;
		}

		#endregion

		#region Caching

		/// <summary>
		/// Removes the page instance from cache if instance exists in cache.
		/// </summary>
		/// <returns>Page.</returns>
		/// <param name="page">Page.</param>
		public static IBasePage<INotifyPropertyChanged> RemovePageInstanceFromCache(this IBasePage<INotifyPropertyChanged> page)
		{
			PageFactory.Factory.RemovePageInstanceFromCache(page);
			return page;
		}

		/// <summary>
		/// Removes the messagable page instance from cache if instance exists in cache.
		/// </summary>
		/// <returns>Page.</returns>
		/// <param name="page">Page.</param>
		public static IBasePage<IBaseMessagable> RemovePageInstanceFromCache(this IBasePage<IBaseMessagable> page)
		{
			PageFactory.Factory.RemovePageInstanceFromCache(page);
			return page;
		}
			
		/// <summary>
		/// Removes the page type from cache if type exists in cache.
		/// </summary>
		/// <returns>Page.</returns>
		/// <param name="page">Page.</param>
		public static IBasePage<INotifyPropertyChanged> RemovePageTypeFromCache(this IBasePage<INotifyPropertyChanged> page)
		{
			PageFactory.Factory.RemovePageTypeFromCache(page.GetType());
			return page;
		}

		/// <summary>
		/// Removes the messagable page type from cache if type exists in cache.
		/// </summary>
		/// <returns>Page.</returns>
		/// <param name="page">Page.</param>
		public static IBasePage<IBaseMessagable> RemovePageTypeFromCache(this IBasePage<IBaseMessagable> page)
		{
			PageFactory.Factory.RemovePageTypeFromCache(page.GetType());
			return page;
		}

		#endregion

		#region Messaging

		/// <summary>
		/// Sends the message to page and view model.
		/// </summary>
		/// <returns>The message.</returns>
		/// <param name="page">Page.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		/// <param name="sender">Sender.</param>
		public static IBasePage<IBaseMessagable> SendMessageToPageAndViewModel(this IBasePage<IBaseMessagable> page, string message, object sender = null, object arg = null)
		{
			PageFactory.Factory.SendMessageByPage(MessageConsumer.PageAndViewModel, page, message, sender, arg);
			return page;
		}

		/// <summary>
		/// Sends the message to view model.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name = "sender">Sender.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		public static IBasePage<IBaseMessagable> SendMessageToViewModel(this IBasePage<IBaseMessagable> page, string message, object sender = null, object arg = null)
		{
			PageFactory.Factory.SendMessageByPage(MessageConsumer.ViewModel, page, message, sender, arg);
			return page;
		}

		/// <summary>
		/// Sends the message to page.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name = "sender">Sender.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		public static IBasePage<IBasePage> SendMessageToPage(this IBasePage<IBasePage> page, string message, object sender = null, object arg = null)
		{
			PageFactory.Factory.SendMessageToPage(page, message, sender, arg);
			return page;
		}

		/// <summary>
		/// Sends the message to messagable page.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name = "sender">Sender.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		public static IBasePage<IBaseMessagable> SendMessageToPage(this IBasePage<IBaseMessagable> page, string message, object sender = null, object arg = null)
		{
			PageFactory.Factory.SendMessageToPage(page, message, sender, arg);
			return page;
		}

		#endregion
	}
}

