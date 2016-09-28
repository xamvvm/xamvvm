using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace DLToolkit.PageFactory
{
    public partial class RxUIPageFactory : IPageFactoryNavigation
    {
        #region IPageFactoryNavigation implementation

        public async Task<bool> PushPageAsync<TPageModel>(IBasePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            var navEventsPage = page as INavigationPushing;
            if (navEventsPage != null && !navEventsPage.PageFactoryPushing())
                return false;

            await NavigationPage.Navigation.PushAsync((Page)page, animated);

            var navEventsPage2 = page as INavigationPushed;
            if (navEventsPage2 != null)
                #pragma warning disable 4014
                Task.Run(() => navEventsPage2.PageFactoryPushed()).ConfigureAwait(false);
                #pragma warning restore 4014

            return true;
        }

        public async Task<bool> PushModalPageAsync<TPageModel>(IBasePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            var navEventsPage = page as INavigationPushing;
            if (navEventsPage != null && !navEventsPage.PageFactoryPushing())
                return false;

            await NavigationPage.Navigation.PushModalAsync((Page)page, animated);

            var navEventsPage2 = page as INavigationPushed;
            if (navEventsPage2 != null)
                #pragma warning disable 4014
                Task.Run(() => navEventsPage2.PageFactoryPushed()).ConfigureAwait(false);
            #pragma warning restore 4014

            return true;
        }

        public async Task<bool> PushPageFromCacheAsync<TPageModel>(bool resetPageModel = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            var page = GetPageFromCache<TPageModel>(resetPageModel);
            return await PushPageAsync(page, animated);
        }

        public async Task<bool> PushModalPageFromCacheAsync<TPageModel>(bool resetPageModel = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            var page = GetPageFromCache<TPageModel>(resetPageModel);
            return await PushModalPageAsync(page, animated);
        }

        public async Task<bool> PushPageAsNewAsync<TPageModel>(bool saveOrReplaceInCache = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            var page = GetPageAsNewInstance<TPageModel>(saveOrReplaceInCache);
            return await PushPageAsync(page, animated);
        }

        public async Task<bool> PushModalPageAsNewAsync<TPageModel>(bool saveOrReplaceInCache = false, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            var page = GetPageAsNewInstance<TPageModel>(saveOrReplaceInCache);
            return await PushModalPageAsync(page, animated);
        }

        public bool InsertPageBefore<TPageModel, TBeforePageModel>(IBasePage<TPageModel> page, IBasePage<TBeforePageModel> before) where TPageModel : class, INotifyPropertyChanged where TBeforePageModel : class, INotifyPropertyChanged
        {
            var navEventsPage = page as INavigationInserting;
            if (navEventsPage != null && !navEventsPage.PageFactoryInserting())
                return false;

            NavigationPage.Navigation.InsertPageBefore((Page)page, (Page)before);

            var navEventsPage2 = page as INavigationInserted;
            if (navEventsPage2 != null)
                Task.Run(() => navEventsPage2.PageFactoryInserted()).ConfigureAwait(false);

            return true;
        }

        public bool InsertPageBeforeFromCache<TPageModel, TBeforePageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged where TBeforePageModel : class, INotifyPropertyChanged
        {
            var page = GetPageFromCache<TPageModel>(resetPageModel);
            var before = GetPageFromCache<TBeforePageModel>(false);

            return InsertPageBefore(page, before);
        }

        public bool InsertPageBeforeAsNew<TPageModel, TBeforePageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged where TBeforePageModel : class, INotifyPropertyChanged
        {
            var page = GetPageAsNewInstance<TPageModel>(saveOrReplaceInCache);
            var before = GetPageFromCache<TBeforePageModel>(false);

            return InsertPageBefore(page, before);
        }

        public async Task<bool> PopPageAsync(bool resetPageModel = false, bool animated = true)
        {
            var page = NavigationPage.Navigation.NavigationStack.LastOrDefault() as IBasePage<INotifyPropertyChanged>;

            if (page != null)
            {
                var navEventsPage = page as INavigationPopping;

                if (navEventsPage != null && !navEventsPage.PageFactoryPopping())
                    return false;

                if (resetPageModel)
                    ResetPageModel(page);

                await NavigationPage.Navigation.PopAsync(animated); 

                var navEventsPage2 = page as INavigationPopped;
                if (navEventsPage2 != null)
                    #pragma warning disable 4014
                    Task.Run(() => navEventsPage2.PageFactoryPopped()).ConfigureAwait(false);
                    #pragma warning restore 4014

                return true;
            }

            return false;
        }

        public async Task<bool> PopModalPageAsync(bool resetPageModel = false, bool animated = true)
        {
            var page = NavigationPage.Navigation.ModalStack.LastOrDefault() as IBasePage<INotifyPropertyChanged>;

            if (page != null)
            {
                var navEventsPage = page as INavigationPopping;

                if (navEventsPage != null && !navEventsPage.PageFactoryPopping())
                    return false;

                if (resetPageModel)
                    ResetPageModel(page);

                await NavigationPage.Navigation.PopModalAsync(animated);    

                var navEventsPage2 = page as INavigationPopped;
                if (navEventsPage2 != null)
                    #pragma warning disable 4014
                    Task.Run(() => navEventsPage2.PageFactoryPopped()).ConfigureAwait(false);
                    #pragma warning restore 4014

                return true;
            }

            return false;
        }

        public bool RemovePage<TPageModel>(IBasePage<TPageModel> pageToRemove) where TPageModel : class, INotifyPropertyChanged
        {
            var exists = NavigationPage.Navigation.NavigationStack.Contains((Page)pageToRemove);

            if (exists)
            {
                var navEventsPage = pageToRemove as INavigationRemoving;
                if (navEventsPage != null && !navEventsPage.PageFactoryRemoving())
                    return false;

                NavigationPage.Navigation.RemovePage((Page)pageToRemove);

                var navEventsPage2 = pageToRemove as INavigationRemoved;
                if (navEventsPage2 != null)
                    Task.Run(() => navEventsPage2.PageFactoryRemoved()).ConfigureAwait(false);
            }

            return false;
        }

        public bool RemoveCachedPage<TPageModel>(bool removeFromCache = false, bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged
        {
            if (pageCache.ContainsKey(typeof(TPageModel)))
            {
                var page = GetPageFromCache<TPageModel>(resetPageModel);

                if (removeFromCache)
                    RemovePageTypeFromCache<TPageModel>();

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

        public void SetNewRootAndReset<TPageModel>() where TPageModel : class, INotifyPropertyChanged
        {
            ClearCache();
            var page = GetPageAsNewInstance<TPageModel>(true);
            var navPageType = NavigationPage.GetType();
            navigationPage = (NavigationPage)Activator.CreateInstance(navPageType, page);

            Application.Current.MainPage = NavigationPage;
        }

        public async Task<bool> PushPageIntoNavigationAsync<TPageModel>(IBaseNavigationPage<TPageModel> navigation, IBasePage<INotifyPropertyChanged> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            var navEventsPage = page as INavigationPushing;
            if (navEventsPage != null && !navEventsPage.PageFactoryPushing())
                return false;

            var xfNavigation = navigation as NavigationPage;

            await xfNavigation.Navigation.PushAsync((Page)page, animated);

            var navEventsPage2 = page as INavigationPushed;
            if (navEventsPage2 != null)
                #pragma warning disable 4014
                Task.Run(() => navEventsPage2.PageFactoryPushed()).ConfigureAwait(false);
                #pragma warning restore 4014

            return true;
        }

        #endregion
    }
}

