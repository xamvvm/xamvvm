using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DLToolkit.PageFactory
{
	public class XamarinFormsPageFactory : IPageFactory
	{
		readonly Dictionary<Type, IBasePage<INotifyPropertyChanged>> pageCache = new Dictionary<Type, IBasePage<INotifyPropertyChanged>>();

		readonly Dictionary<Type, Type> viewModelsTypes = new Dictionary<Type, Type>();

		readonly ConditionalWeakTable<INotifyPropertyChanged, IBasePage<INotifyPropertyChanged>> weakPageCache = new ConditionalWeakTable<INotifyPropertyChanged, IBasePage<INotifyPropertyChanged>>();

		void AddToWeakCacheIfNotExists(IBasePage<INotifyPropertyChanged> page)
		{
			IBasePage<INotifyPropertyChanged> weakExists;
			if (!weakPageCache.TryGetValue(page.ViewModel, out weakExists))
			{
				weakPageCache.Add(page.ViewModel, page);
			}
		}

		public NavigationPage Init<TMainPageViewModel, TNavigationPage>(params Assembly[] additionalPagesAssemblies) where TMainPageViewModel : class, INotifyPropertyChanged where TNavigationPage : PFNavigationPage
		{
			PageFactory.SetPageFactory(this);

			viewModelsTypes.Clear();

			var pagesAssemblies = additionalPagesAssemblies.ToList();
			pagesAssemblies.Add(typeof(TMainPageViewModel).GetTypeInfo().Assembly);
			pagesAssemblies.Add(typeof(TNavigationPage).GetTypeInfo().Assembly);

			foreach (var assembly in pagesAssemblies.Distinct())
			{
				foreach(var typeInfo in assembly.DefinedTypes.Where(t => t.IsClass && !t.IsAbstract && t.ImplementedInterfaces != null))
				{
					var found = typeInfo.ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType && 
						t.GetGenericTypeDefinition() == typeof(IBasePage<>));

					if (found != default(Type))
					{
						var viewModelType = found.GenericTypeArguments.First();
						var pageType = typeInfo.AsType();

						if(!viewModelsTypes.ContainsKey(viewModelType))
						{
							viewModelsTypes.Add(viewModelType, pageType);
						}
						else
						{
							throw new ArgumentOutOfRangeException(
								string.Format("ViewModel {0} has multiple Page definitions", viewModelType.ToString()));
						}
					}
				}	
			}

			var page = GetPageFromCache(typeof(TMainPageViewModel));
			navigationPage = (PFNavigationPage)Activator.CreateInstance(typeof(TNavigationPage), page);
			weakPageCache.Add(navigationPage.ViewModel, navigationPage);

			return NavigationPage;
		}

		PFNavigationPage navigationPage = null;
		public PFNavigationPage NavigationPage
		{
			get
			{
				if (navigationPage == null)
					throw new NullReferenceException("NavigationPage is null. Please set NavigationPage with Init method");

				return navigationPage;	
			}
		}

		Type GetPageType(Type viewModelType)
		{
			Type pageType;

			if (viewModelsTypes.TryGetValue(viewModelType, out pageType))
			{
				return pageType;
			}

			throw new KeyNotFoundException(
				string.Format("Page definition for {0} ViewModel could not be found", viewModelType.ToString()));
		}
			
		#region IPageFactory implementation

		public IBasePage<INotifyPropertyChanged> GetPageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged
		{
			return GetPageFromCache(typeof(TViewModel), resetViewModel);
		}

		public IBasePage<INotifyPropertyChanged> GetPageFromCache(Type viewModelType, bool resetViewModel = false)
		{
			var pageType = GetPageType(viewModelType);

			if (!pageCache.ContainsKey(viewModelType))
			{
				IBasePage<INotifyPropertyChanged> page = Activator.CreateInstance(pageType) as IBasePage<INotifyPropertyChanged>;
				pageCache.Add(viewModelType, page);
				weakPageCache.Add(page.ViewModel, page);
			}

			if (resetViewModel)
			{
				pageCache[viewModelType].PageFactoryResetViewModel();
			}

			return pageCache[viewModelType];
		}

		public IBasePage<INotifyPropertyChanged> GetPageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged
		{
			return GetPageAsNewInstance(typeof(TViewModel), saveOrReplaceInCache);
		}

		public IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false)
		{
			var pageType = GetPageType(viewModelType);

			IBasePage<INotifyPropertyChanged> page = (IBasePage<INotifyPropertyChanged>)Activator.CreateInstance(pageType);
			weakPageCache.Add(page.ViewModel, page);

			if (saveOrReplaceInCache && pageCache.ContainsKey(viewModelType))
			{
				pageCache.Remove(viewModelType);
				pageCache.Add(viewModelType, page);
			}

			return page;
		}

		public IBasePage<INotifyPropertyChanged> GetPageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, INotifyPropertyChanged
		{
			IBasePage<INotifyPropertyChanged> page;

			if (weakPageCache.TryGetValue(viewModelInstance, out page))
			{
				return page;
			}

			return null;
		}

		public IBasePage<IBaseMessagable> GetMessagablePageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged
		{
			return (IBasePage<IBaseMessagable>)GetPageFromCache<TViewModel>(resetViewModel);
		}

		public IBasePage<IBaseMessagable> GetMessagablePageFromCache(Type viewModelType, bool resetViewModel = false)
		{
			return (IBasePage<IBaseMessagable>)GetPageFromCache(viewModelType, resetViewModel);
		}

		public IBasePage<IBaseMessagable> GetMessagablePageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged
		{
			return (IBasePage<IBaseMessagable>)GetPageAsNewInstance<TViewModel>(saveOrReplaceInCache);
		}

		public IBasePage<IBaseMessagable> GetMessagablePageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false)
		{
			return (IBasePage<IBaseMessagable>)GetPageAsNewInstance(viewModelType, saveOrReplaceInCache);
		}

		public IBasePage<IBaseMessagable> GetMessagablePageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged
		{
			return (IBasePage<IBaseMessagable>)GetPageByViewModel<TViewModel>(viewModelInstance);
		}

		public bool SendMessageByPage<TPage>(TPage page, string message, object sender = null, object arg = null) where TPage : class, IBasePage<INotifyPropertyChanged>
		{
			if (page != null)
			{
				page.PageFactoryMessageReceived(message, sender, arg);

				return true;
			}

			return false;
		}

		public bool SendMessageByPage<TPage>(MessageConsumer consumer, TPage page, string message, object sender = null, object arg = null) where TPage : class, IBasePage<IBaseMessagable>
		{
			if (page != null)
			{
				if (consumer == MessageConsumer.Page || consumer == MessageConsumer.PageAndViewModel)
				{
					page.PageFactoryMessageReceived(message, sender, arg);
				}
				if (consumer == MessageConsumer.ViewModel || consumer == MessageConsumer.PageAndViewModel)
				{
					page.ViewModel.PageFactoryMessageReceived(message, sender, arg);
				}

				return true;
			}

			return false;
		}

		public bool SendMessageByViewModel<TViewModel>(MessageConsumer consumer, TViewModel viewModelInstance, string message, object sender = null, object arg = null) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged
		{
			var page = GetPageByViewModel(viewModelInstance);

			if (page != null)
			{
				if (consumer == MessageConsumer.Page || consumer == MessageConsumer.PageAndViewModel)
				{
					page.PageFactoryMessageReceived(message, sender, arg);
				}
				if (consumer == MessageConsumer.ViewModel || consumer == MessageConsumer.PageAndViewModel)
				{
					((IBaseMessagable)page.ViewModel).PageFactoryMessageReceived(message, sender, arg);
				}

				return true;
			}

			return false;
		}

		public bool SendMessageToCached<TViewModel>(MessageConsumer consumer, string message, object sender = null, object arg = null,  bool createPageIfNotExists = true) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged
		{
			if (pageCache.ContainsKey(typeof(TViewModel)) || createPageIfNotExists)
			{
				var page = GetPageFromCache<TViewModel>(false);

				if (consumer == MessageConsumer.Page || consumer == MessageConsumer.PageAndViewModel)
				{
					page.PageFactoryMessageReceived(message, sender, arg);
				}
				if (consumer == MessageConsumer.ViewModel || consumer == MessageConsumer.PageAndViewModel)
				{
					((IBaseMessagable)page.ViewModel).PageFactoryMessageReceived(message, sender, arg);
				}

				return true;
			}

			return false;
		}

		public bool ReplaceCachedPageViewModel<TViewModel>(TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged
		{
			if (pageCache.ContainsKey(typeof(TViewModel)))
			{
				var page = GetPageFromCache<TViewModel>();
				page.PageFactoryReplaceViewModel(newViewModel);
				return true;
			}

			return false;
		}

		public bool ResetCachedPageViewModel<TViewModel>() where TViewModel : class, INotifyPropertyChanged
		{
			if (pageCache.ContainsKey(typeof(TViewModel)))
			{
				var page = GetPageFromCache<TViewModel>();
				page.PageFactoryResetViewModel();
				return true;
			}

			return false;
		}

		public bool RemovePageFromCache(Type viewModelType)
		{
			if (pageCache.ContainsKey(viewModelType))
			{
				pageCache.Remove(viewModelType);
				return true;
			}

			return false;
		}

		public bool RemovePageFromCache<TViewModel>() where TViewModel : class, INotifyPropertyChanged
		{
			return RemovePageFromCache(typeof(TViewModel));
		}

		public void ClearCache()
		{
			pageCache.Clear();
		}

		public async Task<bool> PushPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true)
		{
			AddToWeakCacheIfNotExists(page);

			if (!page.PageFactoryPushing())
				return false;

			await NavigationPage.Navigation.PushAsync((Page)page, animated);

			page.PageFactoryPushed();

			return true;
		}

		public async Task<bool> PushModalPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true)
		{
			AddToWeakCacheIfNotExists(page);

			if (!page.PageFactoryPushing())
				return false;

			await NavigationPage.Navigation.PushModalAsync((Page)page, animated);

			page.PageFactoryPushed();

			return true;
		}

		public async Task<bool> PushPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageFromCache<TViewModel>(resetViewModel);
			return await PushPageAsync(page, animated);
		}

		public async Task<bool> PushPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageFromCache<TViewModel>(false);
			page.PageFactoryReplaceViewModel(replaceViewModel);
			return await PushPageAsync(page, animated);
		}

		public async Task<bool> PushModalPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageFromCache<TViewModel>(resetViewModel);
			return await PushModalPageAsync(page, animated);
		}

		public async Task<bool> PushModalPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageFromCache<TViewModel>(false);
			page.PageFactoryReplaceViewModel(replaceViewModel);
			return await PushModalPageAsync(page, animated);
		}

		public async Task<bool> PushPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageAsNewInstance<TViewModel>(saveOrReplaceInCache);
			page.PageFactoryReplaceViewModel(replaceViewModel);
			return await PushPageAsync(page, animated);
		}

		public async Task<bool> PushPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageAsNewInstance<TViewModel>(saveOrReplaceInCache);
			return await PushPageAsync(page, animated);
		}

		public async Task<bool> PushModalPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageAsNewInstance<TViewModel>(saveOrReplaceInCache);
			page.PageFactoryReplaceViewModel(replaceViewModel);
			return await PushModalPageAsync(page, animated);
		}

		public async Task<bool> PushModalPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageAsNewInstance<TViewModel>(saveOrReplaceInCache);
			return await PushModalPageAsync(page, animated);
		}

		public bool InsertPageBefore(IBasePage<INotifyPropertyChanged> page, IBasePage<INotifyPropertyChanged> before)
		{
			AddToWeakCacheIfNotExists(page);

			if (!page.PageFactoryInserting())
				return false;
			
			NavigationPage.Navigation.InsertPageBefore((Page)page, (Page)before);
			page.PageFactoryInserted();

			return true;
		}

		public bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageFromCache<TViewModel>(resetViewModel);
			var before = GetPageFromCache<TBeforeViewModel>(false);

			return InsertPageBefore(page, before);
		}

		public bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageFromCache<TViewModel>(false);
			var before = GetPageFromCache<TBeforeViewModel>(false);
			page.PageFactoryReplaceViewModel(replaceViewModel);

			return InsertPageBefore(page, before);
		}

		public bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageAsNewInstance<TViewModel>(saveOrReplaceInCache);
			var before = GetPageFromCache<TBeforeViewModel>(false);

			return InsertPageBefore(page, before);
		}

		public bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged
		{
			var page = GetPageAsNewInstance<TViewModel>(saveOrReplaceInCache);
			var before = GetPageFromCache<TBeforeViewModel>(false);
			page.PageFactoryReplaceViewModel(replaceViewModel);

			return InsertPageBefore(page, before);
		}

		public async Task<bool> PopPageAsync(bool resetViewModel = false, bool animated = true)
		{
			var page = NavigationPage.Navigation.NavigationStack.LastOrDefault() as IBasePage<INotifyPropertyChanged>;

			if (page != null && page.PageFactoryPopping())
			{
				if (resetViewModel)
					page.PageFactoryResetViewModel();

				await NavigationPage.Navigation.PopAsync(animated);	
				page.PageFactoryPopped();

				return true;
			}

			return false;
		}

		public async Task<bool> PopModalPageAsync(bool resetViewModel = false, bool animated = true)
		{
			var page = NavigationPage.Navigation.ModalStack.LastOrDefault() as IBasePage<INotifyPropertyChanged>;

			if (page != null && page.PageFactoryPopping())
			{
				if (resetViewModel)
					page.PageFactoryResetViewModel();

				await NavigationPage.Navigation.PopModalAsync(animated);	
				page.PageFactoryPopped();

				return true;
			}

			return false;
		}

		public bool RemovePage(IBasePage<INotifyPropertyChanged> pageToRemove)
		{
			AddToWeakCacheIfNotExists(pageToRemove);

			var exists = NavigationPage.Navigation.NavigationStack.Contains((Page)pageToRemove);

			if (exists && pageToRemove.PageFactoryRemoving())
			{
				NavigationPage.Navigation.RemovePage((Page)pageToRemove);
				pageToRemove.PageFactoryRemoved();
			}

			return false;
		}

		public bool RemoveCachedPage<TViewModel>(bool removeFromCache = false, bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged
		{
			if (pageCache.ContainsKey(typeof(TViewModel)))
			{
				var page = GetPageFromCache<TViewModel>(resetViewModel);

				if (removeFromCache)
					RemovePageFromCache<TViewModel>();
					
				return RemovePage(page);
			}

			return false;
		}


		public async Task PopPagesToRootAsync(bool clearCache = false, bool animated = true)
		{
			await NavigationPage.Navigation.PopToRootAsync(animated);

			if (clearCache)
			{
				ClearCache();
			}
		}
			
		public void SetNewRootAndReset<TViewModel>() where TViewModel : class, INotifyPropertyChanged
		{
			ClearCache();
			var page = GetPageAsNewInstance<TViewModel>(true);
			var navPageType = NavigationPage.GetType();
			navigationPage = (PFNavigationPage)Activator.CreateInstance(navPageType, page);

			weakPageCache.Add(navigationPage.ViewModel, navigationPage);

			Application.Current.MainPage = NavigationPage;	
		}

		#endregion
	}
}

