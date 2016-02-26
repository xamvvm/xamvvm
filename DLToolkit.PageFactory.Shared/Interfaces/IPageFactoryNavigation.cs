using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IPageFactory navigation.
	/// </summary>
	public interface IPageFactoryNavigation
	{
		/// <summary>
		/// Pushes the page into navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="page">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
        Task<bool> PushPageAsync<TPageModel>(IBasePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Pushes the page as modal.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="page">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
        Task<bool> PushModalPageAsync<TPageModel>(IBasePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Pushes the cached page into navigation stack. Creates a new page instances if not exists.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets page page model.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		Task<bool> PushPageFromCacheAsync<TPageModel>(bool resetPageModel = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Pushes the cached page as modal. Creates a new page instances if not exists.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets page model.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		Task<bool> PushModalPageFromCacheAsync<TPageModel>(bool resetPageModel = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Pushes a new page instance into navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		Task<bool> PushPageAsNewAsync<TPageModel>(bool saveOrReplaceInCache = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Pushes a new page instance as modal.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		Task<bool> PushModalPageAsNewAsync<TPageModel>(bool saveOrReplaceInCache = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Inserts the page before another page into navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page was insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
		/// <param name="page">Page to insert</param>
		/// <param name="before">Before page</param>
        bool InsertPageBefore<TPageModel, TBeforePageModel>(IBasePage<TPageModel> page, IBasePage<TBeforePageModel> before) where TPageModel : class, INotifyPropertyChanged where TBeforePageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Inserts the cached page before another page into navigation stack. Creates a new page instances if not exists.
		/// </summary>
		/// <returns><c>true</c>, if page insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets the page model.</param>
		/// <typeparam name="TPageModel">Type of page to insert.</typeparam>
		/// <typeparam name="TBeforePageModel">Type of before page.</typeparam>
		bool InsertPageBeforeFromCache<TPageModel, TBeforePageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged where TBeforePageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Inserts the page before another page into navigation stack as new instance.
		/// </summary>
		/// <returns><c>true</c>, if page insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
		/// <typeparam name="TPageModel">Type of page to insert.</typeparam>
		/// <typeparam name="TBeforePageModel">Type of before page.</typeparam>
		bool InsertPageBeforeAsNew<TPageModel, TBeforePageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged  where TBeforePageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Pops the page from navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page was pop was not interrupted, <c>false</c> otherwise (eg. PageFactoryPopping returned <c>false</c>)</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets the page model.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		Task<bool> PopPageAsync(bool resetPageModel = false, bool animated = true);

		/// <summary>
		/// Pops the modal page.
		/// </summary>
		/// <returns><c>true</c>, if page pop was not interrupted, <c>false</c> otherwise (eg. PageFactoryPopping returned <c>false</c>)</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets the page model.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		Task<bool> PopModalPageAsync(bool resetPageModel = false, bool animated = true);

		/// <summary>
		/// Removes the page from navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page remove was not interrupted, <c>false</c> otherwise (eg. PageFactoryRemoving returned <c>false</c>)</returns>
		/// <param name="pageToRemove">Page to remove.</param>
        bool RemovePage<TPageModel>(IBasePage<TPageModel> pageToRemove) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Removes the cached page from navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page remove was not interrupted, <c>false</c> otherwise (eg. PageFactoryRemoving returned <c>false</c>)</returns>
		/// <param name="removeFromCache">If set to <c>true</c> removes the page from cache.</param>
		/// <param name="resetPageModel">If set to <c>true</c> resets the page model.</param>
		/// <typeparam name="TPageModel">Cached page model type.</typeparam>
		bool RemoveCachedPage<TPageModel>(bool removeFromCache = false, bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Pops the pages to root.
		/// </summary>
		/// <param name="clearCache">If set to <c>true</c> clears cache.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
		Task PopPagesToRootAsync(bool clearCache = false, bool animated = true);

		/// <summary>
		/// Sets the new root and resets.
		/// </summary>
		/// <typeparam name="TPageModel">New root page model type.</typeparam>
		void SetNewRootAndReset<TPageModel>() where TPageModel : class, INotifyPropertyChanged;

		/* NAVIGATION PAGES */

		/// <summary>
		/// Pushes another page into current navigation page navigation stack.
		/// </summary>
		/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
		/// <param name = "navigation">Navigation page</param>
		/// <param name="page">Page to push.</param>
		/// <param name="animated">If set to <c>true</c> animation enabled.</param>
        Task<bool> PushPageIntoNavigationAsync<TPageModel>(IBaseNavigationPage<TPageModel> navigation, IBasePage<INotifyPropertyChanged> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged;
	}
}

