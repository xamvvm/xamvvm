using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Xamvvm
{
	/// <summary>
	/// IBaseFactory navigation.
	/// </summary>
	public interface IBaseFactoryNavigation
	{
		/// <summary>
		/// Pushes the page into navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="currentPage">Current page.</param>
		/// <param name="pageToPush">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
        Task<bool> PushPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Pushes the page as modal.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="currentPage">Current page.</param>
		/// <param name="pageToPush">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
        Task<bool> PushModalPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Inserts the page before another page into navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page was insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
		/// <param name="pageToInsert">Page to insert</param>
		/// <param name="beforePage">Before page</param>
        Task<bool> InsertPageBeforeAsync<TPageModel, TBeforePageModel>(IBasePage<TPageModel> pageToInsert, IBasePage<TBeforePageModel> beforePage) where TPageModel : class, IBasePageModel where TBeforePageModel : class, IBasePageModel;

		/// <summary>
		/// Pops the page from navigation stack.
		/// </summary>
		/// <param name="currentPage">Current page.</param>
		/// <returns><c>true</c>, if page was pop was not interrupted, <c>false</c> otherwise (eg. PageFactoryPopping returned <c>false</c>)</returns>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		Task<bool> PopPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel;

		/// <summary>
		/// Pops the modal page.
		/// </summary>
		/// <param name="currentPage">Current page.</param>
		/// <returns><c>true</c>, if page pop was not interrupted, <c>false</c> otherwise (eg. PageFactoryPopping returned <c>false</c>)</returns>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		Task<bool> PopModalPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel;

		/// <summary>
		/// Removes the page from navigation stack.
		/// </summary>
		/// <param name="currentPage">Current page.</param>
		/// <returns><c>true</c>, if page remove was not interrupted, <c>false</c> otherwise (eg. PageFactoryRemoving returned <c>false</c>)</returns>
		/// <param name="pageToRemove">Page to remove.</param>
        Task<bool> RemovePageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToRemove) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel;

		/// <summary>
		/// Pops the pages to root.
		/// </summary>
		/// <param name="currentPage">Current page.</param>
		/// <param name="clearCache">If set to <c>true</c> clears cache.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		Task<bool> PopPagesToRootAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool clearCache = false, bool animated = true) where TCurrentPageModel : class, IBasePageModel;

		/// <summary>
		/// Sets the new root and resets.
		/// </summary>
		/// <returns>The new root and reset async.</returns>
		/// <param name="currentPage">Current page.</param>
		/// <param name="newRootPage">New root page.</param>
		/// <param name="clearCache">If set to <c>true</c> clear cache.</param>
		/// <typeparam name="TCurrentPageModel">The 1st type parameter.</typeparam>
		/// <typeparam name="TPageModel">The 2nd type parameter.</typeparam>
		Task<bool> SetNewRootAndResetAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> newRootPage, bool clearCache = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel;
	}
}

