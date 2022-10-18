using ReactiveUI.XamForms;
using System.ComponentModel;

namespace Xamvvm
{
	public class BaseMasterDetailPageRxUI<TPageModel> : ReactiveMasterDetailPage<TPageModel>,  IBasePageRxUI<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }
}
