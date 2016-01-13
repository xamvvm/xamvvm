using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IBaseNavigationPage.
	/// </summary>
	public interface IBaseNavigationPage<TNavigationViewModel> : IBasePage<TNavigationViewModel> where TNavigationViewModel: class, INotifyPropertyChanged
	{
	}

	/// <summary>
	/// IBaseNavigationPage.
	/// </summary>
	public interface IBaseNavigationPage<TNavigationViewModel, TPageViewModel> : IBaseNavigationPage<TNavigationViewModel> 
		where TNavigationViewModel: class, INotifyPropertyChanged
		where TPageViewModel: class, INotifyPropertyChanged
	{
		/// <summary>
		/// Gets the current navigated page.
		/// </summary>
		/// <returns>The current navigated page.</returns>
		IBasePage<TPageViewModel> GetCurrentPage();

		/// <summary>
		/// Gets the root navigation page.
		/// </summary>
		/// <returns>The navigation root page.</returns>
		IBasePage<TPageViewModel> GetRootPage();
	}
}

