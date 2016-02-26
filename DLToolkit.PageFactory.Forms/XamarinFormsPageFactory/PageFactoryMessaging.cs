using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactoryMessaging
    {
        #region IPageFactoryMessaging implementation

        public IBaseMessagablePage<TPageModel> GetMessagablePageFromCache<TPageModel>(bool resetPageModel = false) where TPageModel : class, INotifyPropertyChanged
        {
            var page = GetPageFromCache<TPageModel>(resetPageModel) as IBaseMessagablePage<TPageModel>;

            if (page == null)
                throw new Exception(string.Format("Page of {0} does not implement IMessagable", typeof(TPageModel)));

            return page;
        }

        public IBaseMessagablePage<INotifyPropertyChanged> GetMessagablePageFromCache(Type pageModelType, bool resetPageModel = false)
        {
            var page =  GetPageFromCache(pageModelType, resetPageModel) as IBaseMessagablePage<INotifyPropertyChanged>;

            if (page == null)
                throw new Exception(string.Format("Page of {0} does not implement IMessagable", pageModelType));

            return page;
        }

        public IBaseMessagablePage<TPageModel> GetMessagablePageAsNewInstance<TPageModel>(bool saveOrReplaceInCache = false) where TPageModel : class, INotifyPropertyChanged
        {
            var page =  GetPageAsNewInstance<TPageModel>(saveOrReplaceInCache) as IBaseMessagablePage<TPageModel>;

            if (page == null)
                throw new Exception(string.Format("Page of {0} does not implement IMessagable", typeof(TPageModel)));

            return page;
        }

        public IBaseMessagablePage<INotifyPropertyChanged> GetMessagablePageAsNewInstance(Type pageModelType, bool saveOrReplaceInCache = false)
        {
            var page =  GetPageAsNewInstance(pageModelType, saveOrReplaceInCache) as IBaseMessagablePage<INotifyPropertyChanged>;

            if (page == null)
                throw new Exception(string.Format("Page of {0} does not implement IMessagable", pageModelType));

            return page;
        }

        public IBaseMessagablePage<TPageModel> GetMessagablePageByModel<TPageModel>(TPageModel pageModelInstance) where TPageModel : class, INotifyPropertyChanged
        {
            var page =  GetPageByModel<TPageModel>(pageModelInstance) as IBaseMessagablePage<TPageModel>;

            if (page == null)
                throw new Exception(string.Format("Page of {0} does not implement IMessagable", typeof(TPageModel)));

            return page;
        }

        public bool SendMessageToPage<TPageModel>(IBaseMessagablePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged
        {
            if (page != null)
            {
                #pragma warning disable 4014
                Task.Run(() => page.PageFactoryMessageReceived(message, sender, arg)).ConfigureAwait(false);
                #pragma warning restore 4014
                return true;
            }

            return false;
        }

        public bool SendMessageToPageModel<TPageModel>(IBasePage<TPageModel> page, string message, object sender = null, object arg = null) where TPageModel : class, INotifyPropertyChanged, IMessagable
        {
            if (page != null)
            {
                var xfPage = (Page)page;
                var viewModel = xfPage.BindingContext as IMessagable;

                if (viewModel != null)
                {
                    #pragma warning disable 4014
                    Task.Run(() => viewModel.PageFactoryMessageReceived(message, sender, arg)).ConfigureAwait(false);
                    #pragma warning restore 4014
                    return true;
                }
            }

            return false;
        }
        #endregion


    }
}

