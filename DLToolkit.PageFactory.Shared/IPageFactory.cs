using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
	public interface IPageFactory
	{
		IBasePage<INotifyPropertyChanged> GetPageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged;

		IBasePage<INotifyPropertyChanged> GetPageFromCache(Type viewModelType, bool resetViewModel = false);

		IBasePage<INotifyPropertyChanged> GetPageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged;

		IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false);
	
		IBasePage<INotifyPropertyChanged> GetPageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, INotifyPropertyChanged;


		bool ReplaceCachedPageViewModel<TViewModel>(TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged;

		bool ResetCachedPageViewModel<TViewModel>() where TViewModel : class, INotifyPropertyChanged;

		bool RemovePageFromCache(Type viewModelType);

		bool RemovePageFromCache<TViewModel>() where TViewModel : class, INotifyPropertyChanged;

		void ClearCache();


		bool SendMessageToPage<TViewModel>(TViewModel viewModelInstance, string message, object arg = null) where TViewModel : class, INotifyPropertyChanged;

		bool SendMessageToCachedPage<TViewModel>(string message, object arg = null, bool createPageIfNotExists = true) where TViewModel : class, INotifyPropertyChanged;


		Task<bool> PushPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true);

		Task<bool> PushModalPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true);


		Task<bool> PushPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

		Task<bool> PushPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

		Task<bool> PushModalPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

		Task<bool> PushModalPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged;


		Task<bool> PushPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

		Task<bool> PushPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

		Task<bool> PushModalPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

		Task<bool> PushModalPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;


		bool InsertPageBefore(IBasePage<INotifyPropertyChanged> page, IBasePage<INotifyPropertyChanged> before);


		bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

		bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

		bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

		bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;


		Task<bool> PopPageAsync(bool removeFromCache = false, bool resetViewModel = false, bool animated = true);

		Task<bool> PopModalPageAsync(bool removeFromCache = false, bool resetViewModel = false, bool animated = true);


		bool RemovePage(IBasePage<INotifyPropertyChanged> pageToRemove);

		bool RemoveCachedPage<TViewModel>(bool removeFromCache = false, bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged;


		Task PopPagesToRootAsync(bool clearCache = false, bool animated = true);

		void SetNewRootAndReset<TViewModel>() where TViewModel : class, INotifyPropertyChanged;
	}
}

