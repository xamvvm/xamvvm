using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IPageFactory caching.
	/// </summary>
	public interface IPageFactoryCaching
	{
		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets page model.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBasePage<TPageModel> GetPageFromCache<TPageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="resetPageModel">If set to <c>true</c> resets page model.</param>
		IBasePage<INotifyPropertyChanged> GetPageFromCache(Type pageModelType, bool resetPageModel = false);

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
		IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type pageModelType, bool saveOrReplaceInCache = false);

		/// <summary>
		/// Gets the navigation from cache. Creates a new navigation instances if not exists.
		/// </summary>
		/// <returns>The navigation from cache.</returns>
		/// <param name="resetPageModel">If set to <c>true</c> resets page model.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBaseNavigationPage<TPageModel> GetNavigationFromCache<TPageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Gets the navigation from cache. Creates a new navigation instances if not exists.
		/// </summary>
		/// <returns>The navigation from cache.</returns>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="resetPageModel">If set to <c>true</c> resets page model.</param>
		IBaseNavigationPage<INotifyPropertyChanged> GetNavigationFromCache(Type pageModelType, bool resetPageModel = false);

		/// <summary>
		/// Gets the navigation as new instance.
		/// </summary>
		/// <returns>The navigation as new instance.</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces navigation in cache.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
        IBaseNavigationPage<TPageModel> GetNavigationAsNewInstance<TPageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Gets the navigation as new instance.
		/// </summary>
		/// <returns>The navigation as new instance.</returns>
		/// <param name="pageModelType">Page model type.</param>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces navigation in cache.</param>
		IBaseNavigationPage<INotifyPropertyChanged> GetNavigationAsNewInstance(Type pageModelType, bool saveOrReplaceInCache = false);

		/// <summary>
		/// Replaces the cached Page PageModel.
		/// </summary>
		/// <returns><c>true</c>, if cached page page model was replaced, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <param name="newPageModel">New page model.</param>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		bool ReplaceCachedPageModel<TPageModel>(TPageModel newPageModel) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
        /// Resets the cached Page PageModel.
		/// </summary>
		/// <returns><c>true</c>, if cached page page model was reset, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		bool ResetCachedPageModel<TPageModel>() where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Removes the page from cache.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <param name="pageModelType">Page model type.</param>
		bool RemovePageTypeFromCache(Type pageModelType);

		/// <summary>
		/// Removes the page from cache.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <typeparam name="TPageModel">Page model type.</typeparam>
		bool RemovePageTypeFromCache<TPageModel>() where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Removes the page instance from cache if exists.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page instance doesn't exist in cache).</returns>
		/// <param name="page">Page instance.</param>
        bool RemovePageInstanceFromCache<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged;

		/// <summary>
		/// Clears the cache.
		/// </summary>
		void ClearCache();
	}
}

