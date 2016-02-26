using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
    /// IBasePage.
	/// </summary>
	public interface IBasePage<out TViewModel> : IBasePage where TViewModel: class, INotifyPropertyChanged
	{
	}	

    /// <summary>
    /// IBaseMessagablePage.
    /// </summary>
    public interface IBaseMessagablePage<out TViewModel> : IBasePage<TViewModel>, IMessagable where TViewModel: class, INotifyPropertyChanged
    {
    }

	/// <summary>
    /// IBasePage.
	/// </summary>
	public interface IBasePage
	{
        
	}

    /// <summary>
    /// IBasePage.
    /// </summary>
    public interface IBasePageAll<out TViewModel> : IBaseMessagablePage<TViewModel>, IPageModelInitializer<TViewModel>, INavigationEvents where TViewModel: class, INotifyPropertyChanged
    {

    }
}

