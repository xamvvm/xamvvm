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

    /// <summary>
    /// IBaseMessagablePage.
    /// </summary>
    public interface IBaseMessagablePage<out TViewModel> : IBasePage<TViewModel>, IMessagable where TViewModel: class, INotifyPropertyChanged
    {
    }
}

