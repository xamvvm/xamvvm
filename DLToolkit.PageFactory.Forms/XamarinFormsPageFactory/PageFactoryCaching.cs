using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactoryCaching
    {
        #region IPageFactoryCaching implementation

        public IBasePage<TPageModel> GetPageFromCache<TPageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged
        {
            return GetPageFromCache(typeof(TPageModel), resetPageModel) as IBasePage<TPageModel>;
        }

        public virtual IBasePage<INotifyPropertyChanged> GetPageFromCache(Type pageModelType, bool resetPageModel = false)
        {
            var pageType = GetPageType(pageModelType);

            if (!pageCache.ContainsKey(pageModelType))
            {
                IBasePage<INotifyPropertyChanged> page = Activator.CreateInstance(pageType) as IBasePage<INotifyPropertyChanged>;
                ResetPageModel(page);
                pageCache.Add(pageModelType, page);
            }
            else if (resetPageModel)
            {
                ResetPageModel(pageCache[pageModelType]);
            }

            return pageCache[pageModelType];
        }

        public IBasePage<TPageModel> GetPageAsNewInstance<TPageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged
        {
            return GetPageAsNewInstance(typeof(TPageModel), saveOrReplaceInCache) as IBasePage<TPageModel>;
        }

        public virtual IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type pageModelType, bool saveOrReplaceInCache = false)
        {
            var pageType = GetPageType(pageModelType);

            IBasePage<INotifyPropertyChanged> page = (IBasePage<INotifyPropertyChanged>)Activator.CreateInstance(pageType);
            ResetPageModel(page);

            if (saveOrReplaceInCache && pageCache.ContainsKey(pageModelType))
            {
                RemovePageTypeFromCache(pageModelType);
                pageCache.Add(pageModelType, page);
            }

            return page;
        }

        public IBaseNavigationPage<TPageModel> GetNavigationFromCache<TPageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged
        {
            return GetPageFromCache(typeof(TPageModel), resetPageModel) as IBaseNavigationPage<TPageModel>;
        }

        public IBaseNavigationPage<INotifyPropertyChanged> GetNavigationFromCache(Type pageModelType, bool resetPageModel = false)
        {
            return GetPageFromCache(pageModelType, resetPageModel) as IBaseNavigationPage<INotifyPropertyChanged>;
        }

        public IBaseNavigationPage<TPageModel> GetNavigationAsNewInstance<TPageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged
        {
            return GetPageAsNewInstance(typeof(TPageModel), saveOrReplaceInCache) as  IBaseNavigationPage<TPageModel>;
        }

        public IBaseNavigationPage<INotifyPropertyChanged> GetNavigationAsNewInstance(Type pageModelType, bool saveOrReplaceInCache = false)
        {
            return GetPageAsNewInstance(pageModelType, saveOrReplaceInCache) as IBaseNavigationPage<INotifyPropertyChanged>;
        }

        public bool ReplaceCachedPageModel<TPageModel>(TPageModel newPageModel) where TPageModel : class, INotifyPropertyChanged
        {
            if (pageCache.ContainsKey(typeof(TPageModel)))
            {
                var page = GetPageFromCache<TPageModel>();
                ReplacePageModel(page, newPageModel);
                return true;
            }

            return false;
        }

        public bool ResetCachedPageModel<TPageModel>() where TPageModel : class, INotifyPropertyChanged
        {
            if (pageCache.ContainsKey(typeof(TPageModel)))
            {
                var page = GetPageFromCache<TPageModel>();
                ResetPageModel(page);
                return true;
            }

            return false;
        }

        public bool RemovePageTypeFromCache(Type pageModelType)
        {
            IBasePage<INotifyPropertyChanged> page;

            if (pageCache.TryGetValue(pageModelType, out page))
            {
                var navEventsPage = page as INavigationRemovingFromCache;
                if (navEventsPage != null)
                    #pragma warning disable 4014
                    Task.Run(() => navEventsPage.PageFactoryRemovingFromCache()).ConfigureAwait(false);
                    #pragma warning restore 4014
                
                pageCache.Remove(pageModelType);
                return true;
            }

            return false;
        }

        public bool RemovePageTypeFromCache<TPageModel>() where TPageModel : class, INotifyPropertyChanged
        {
            return RemovePageTypeFromCache(typeof(TPageModel));
        }

        public bool RemovePageInstanceFromCache<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            IBasePage<INotifyPropertyChanged> pageExists;

            var viewModelType = GetPageModelType(page);

            if (pageCache.TryGetValue(viewModelType, out pageExists))
            {
                if (pageExists == page)
                {
                    var navEventsPage = page as INavigationRemovingFromCache;
                    if (navEventsPage != null)
                        #pragma warning disable 4014
                        Task.Run(() => navEventsPage.PageFactoryRemovingFromCache()).ConfigureAwait(false);
                        #pragma warning restore 4014

                    pageCache.Remove(viewModelType);
                }

                return true;
            }

            return false;
        }

        public void ClearCache()
        {
            foreach (var page in pageCache.Values)
            {
                var navEventsPage = page as INavigationRemovingFromCache;
                if (navEventsPage != null)
                    #pragma warning disable 4014
                    Task.Run(() => navEventsPage.PageFactoryRemovingFromCache()).ConfigureAwait(false);
                    #pragma warning restore 4014
            }

            pageCache.Clear();
        }

        #endregion
    }
}

