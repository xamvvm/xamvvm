using ReactiveUI.XamForms;
using System.ComponentModel;

namespace Xamvvm
{
	public class BaseContentPageRxUI<TPageModel> : ReactiveContentPage<TPageModel>,  IBasePage<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }
}
