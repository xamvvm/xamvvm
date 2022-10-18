using System.ComponentModel;
using ReactiveUI.XamForms;

namespace Xamvvm
{
    public class BaseNavigationPageRxUI<TPageModel> : ReactiveNavigationPage<TPageModel>, IBasePageRxUI<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }
}
