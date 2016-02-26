using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
    /// IBasePage.
	/// </summary>
	public interface IBasePage<out TViewModel> where TViewModel: class, INotifyPropertyChanged
	{
	}	
}

