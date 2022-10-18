using System.ComponentModel;
using ReactiveUI.XamForms;

namespace Xamvvm
{
    public class BaseTabbedPageRxUI<TPageModel> : ReactiveTabbedPage<TPageModel>, IBasePageRxUI<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }
}
