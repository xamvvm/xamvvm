using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// I base page.
	/// </summary>
	public interface IBasePage<out TViewModel> : IBasePage, IBaseMessagable, IBaseNavigationEvents where TViewModel: INotifyPropertyChanged
	{
		/// <summary>
		/// Gets the view model.
		/// </summary>
		/// <value>The view model.</value>
		TViewModel ViewModel { get; }

		/// <summary>
		/// Resets the view model.
		/// </summary>
		void PageFactoryResetViewModel();

		/// <summary>
		/// Replaces the view model.
		/// </summary>
		/// <param name="newViewModel">New view model.</param>
		void PageFactoryReplaceViewModel(object newViewModel);
	}	

	/// <summary>
	/// I base page.
	/// </summary>
	public interface IBasePage : IBaseMessagable, IBaseNavigationEvents
	{
		/// <summary>
		/// Gets the PageFactory.Factory.
		/// </summary>
		/// <value>The page factory.</value>
		IPageFactory PageFactory { get; }
	}
}

