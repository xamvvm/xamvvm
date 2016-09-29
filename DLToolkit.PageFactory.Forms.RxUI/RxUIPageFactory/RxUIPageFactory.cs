using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Logging;

namespace DLToolkit.PageFactory
{
    public partial class RxUIPageFactory : IPageFactory
    {
        #region IPageFactory implementation

        public IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModelInstance) where TPageModel : class, INotifyPropertyChanged
        {
            IBasePage<INotifyPropertyChanged> page;

            if (weakPageCache.TryGetValue(pageModelInstance, out page))
            {
                return page as IBasePage<TPageModel>;
            }

            return null;
        }

        public TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            var xfPage = page as Page;

            if (xfPage != null)
                return xfPage.BindingContext as TPageModel;

            return null;
        }

        public void ReplacePageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, INotifyPropertyChanged
        {
            RemoveFromWeakCacheIfExists(page);
            ((Page)page).BindingContext = newPageModel;

            ((Page)page).BindingContext = newPageModel;


            using (Log.Perf("SetViewModel"))
            {
                PropertyInfo prop = page.GetType().GetRuntimeProperty("ViewModel");
                prop.SetValue(page, newPageModel);
            }

            AddToWeakCacheIfNotExists(page);
        }

        public void ResetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            var pageModelInitializerPage = page as IPageModelInitializer<INotifyPropertyChanged>;

            if (pageModelInitializerPage != null)
            {
                ReplacePageModel(page, pageModelInitializerPage.PageModelInitializer());
            }
            else
            {
                var pageModel = CreatePageModelInstance(page);
                ReplacePageModel(page, pageModel);
            }
        }

        private INotifyPropertyChanged CreatePageModelInstance<TPageModel>(IBasePage<TPageModel> page)
            where TPageModel : class, INotifyPropertyChanged
        {
            if (staticInitialization)
            {
                return viewToViewModelCreationMap[page.GetType()]();
                }
            else
            {
                return Activator.CreateInstance(GetPageModelType(page)) as INotifyPropertyChanged;

            }
        }

        #endregion
    }
}

