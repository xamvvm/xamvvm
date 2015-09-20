using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// I base page.
	/// </summary>
	public interface IBasePage<out TViewModel> : IBasePage, IBaseMessagable, IBaseNavigationEvents where TViewModel: class, INotifyPropertyChanged
	{
		/// <summary>
		/// Gets the view model.
		/// </summary>
		/// <value>The view model.</value>
		TViewModel ViewModel { get; }
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

