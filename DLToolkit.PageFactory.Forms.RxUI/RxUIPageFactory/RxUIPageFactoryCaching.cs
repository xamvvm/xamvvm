using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Logging;


namespace DLToolkit.PageFactory
{
    public partial class RxUIPageFactory : XamarinFormsPageFactory
    {
        #region IPageFactoryCaching implementation


        public override  IBasePage<INotifyPropertyChanged> GetPageFromCache(Type pageModelType, bool resetPageModel = false)
        {
            using (Log.Perf("GetPageFromCache"))
            {




                if (!pageCache.ContainsKey(pageModelType))
                {
                    using (Log.Perf("GetPageFromCache.CreateInstance"))
                    {
                        IBasePage<INotifyPropertyChanged> page;

                        if (staticInitialization)
                        {
                            page = viewModelToViewCreationMap[pageModelType]();
                        }
                        else
                        {
                            var pageType = GetPageType(pageModelType);
                            page = Activator.CreateInstance(pageType) as IBasePage<INotifyPropertyChanged>;
                        }


                        using (Log.Perf("GetPageFromCache.ResetPageModel"))
                        {

                            ResetPageModel(page);
                        }

                        pageCache.Add(pageModelType, page);
                    }
                }
                else if (resetPageModel)
                {
                    using (Log.Perf("GetPageFromCache.ResetPageModel"))
                    {
                        ResetPageModel(pageCache[pageModelType]);
                    }
                }

                return pageCache[pageModelType];
            }
        }


        public override  IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type pageModelType, bool saveOrReplaceInCache = false)
        {
            IBasePage<INotifyPropertyChanged> page;
            if (staticInitialization)
            {
                page = viewModelToViewCreationMap[pageModelType]();
            }
            else
            {
                var pageType = GetPageType(pageModelType);

                page = (IBasePage<INotifyPropertyChanged>)Activator.CreateInstance(pageType);
            }
            ResetPageModel(page);

            if (saveOrReplaceInCache && pageCache.ContainsKey(pageModelType))
            {
                RemovePageTypeFromCache(pageModelType);
                pageCache.Add(pageModelType, page);
            }

            return page;
        }



        public new bool RemovePageInstanceFromCache<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            IBasePage<INotifyPropertyChanged> pageExists;

            var viewModelType =  staticInitialization ? viewToViewModelCreationMap[page.GetType()].GetType() : GetPageModelType(page);

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


        #endregion
    }
}

