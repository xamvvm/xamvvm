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
		/// Gets the page by view model.
		/// </summary>
		/// <returns>The page by view model.</returns>
		/// <param name="viewModelInstance">View model instance.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		IBasePage<INotifyPropertyChanged> GetPageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, INotifyPropertyChanged, new();

		/// <summary>
		/// Replaces the page view model.
		/// </summary>
		/// <param name = "page">Page.</param>
		/// <param name="newViewModel">New view model.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		void ReplacePageViewModel<TViewModel>(IBasePage<INotifyPropertyChanged> page, TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Resets the page view model.
		/// </summary>
		/// <param name = "page">Page.</param>
		void ResetPageViewModel(IBasePage<INotifyPropertyChanged> page);
	}
}

