using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactoryMessaging
    {
        #region IPageFactoryMessaging implementation

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

        public void SendActionToPageModel<TPageModel>(IBasePage<TPageModel> page, Action<TPageModel> action) where TPageModel : class, INotifyPropertyChanged, IMessagable
        {
            #pragma warning disable 4014
            Task.Run(() => { 
                TPageModel pageModel = GetPageModel(page);
                action(pageModel); 
            });
            #pragma warning restore 4014
        }
    }
}

