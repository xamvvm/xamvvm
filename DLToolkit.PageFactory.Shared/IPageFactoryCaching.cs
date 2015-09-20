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
		/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		IBasePage<INotifyPropertyChanged> GetPageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged, new();

		/// <summary>
		/// Gets the page from cache. Creates a new page instances if not exists.
		/// </summary>
		/// <returns>The page from cache.</returns>
		/// <param name="viewModelType">View model type.</param>
		/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
		IBasePage<INotifyPropertyChanged> GetPageFromCache(Type viewModelType, bool resetViewModel = false);

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		IBasePage<INotifyPropertyChanged> GetPageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged, new();

		/// <summary>
		/// Gets the page as new instance.
		/// </summary>
		/// <returns>The page as new instance.</returns>
		/// <param name="viewModelType">View model type.</param>
		/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
		IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false);

		/// <summary>
		/// Replaces the cached page view model.
		/// </summary>
		/// <returns><c>true</c>, if cached page view model was replaced, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <param name="newViewModel">New view model.</param>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		bool ReplaceCachedPageViewModel<TViewModel>(TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged, new();

		/// <summary>
		/// Resets the cached page view model.
		/// </summary>
		/// <returns><c>true</c>, if cached page view model was reset, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		bool ResetCachedPageViewModel<TViewModel>() where TViewModel : class, INotifyPropertyChanged, new();

		/// <summary>
		/// Removes the page from cache.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <param name="viewModelType">View model type.</param>
		bool RemovePageTypeFromCache(Type viewModelType);

		/// <summary>
		/// Removes the page from cache.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
		/// <typeparam name="TViewModel">View model type.</typeparam>
		bool RemovePageTypeFromCache<TViewModel>() where TViewModel : class, INotifyPropertyChanged, new();

		/// <summary>
		/// Removes the page instance from cache if exists.
		/// </summary>
		/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page instance doesn't exist in cache).</returns>
		/// <param name="page">Page instance.</param>
		bool RemovePageInstanceFromCache(IBasePage<INotifyPropertyChanged> page);

		/// <summary>
		/// Clears the cache.
		/// </summary>
		void ClearCache();
	}
}

