using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Logging;

namespace DLToolkit.PageFactory
{
    public partial class RxUIPageFactory : XamarinFormsPageFactory
    {
        #region IPageFactory implementation



        public override  void ReplacePageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) 
        {
            RemoveFromWeakCacheIfExists(page);
            ((Page)page).BindingContext = newPageModel;

            PropertyInfo prop = page.GetType().GetRuntimeProperty("ViewModel");
            if (prop != null)
            {
                prop.SetValue(page, newPageModel);
            }

            AddToWeakCacheIfNotExists(page);
        }

        public override void ResetPageModel<TPageModel>(IBasePage<TPageModel> page) 
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

