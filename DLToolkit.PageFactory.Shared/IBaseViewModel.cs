using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// Base view model interface.
	/// </summary>
	public interface IBaseViewModel : IBaseMessagable, INotifyPropertyChanged
	{
		/// <summary>
		/// Gets the PageFactory.Factory.
		/// </summary>
		/// <value>The page factory.</value>
		IPageFactory PageFactory { get; }
	}
}
