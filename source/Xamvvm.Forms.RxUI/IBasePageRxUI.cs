using System.ComponentModel;
using ReactiveUI;

namespace Xamvvm
{

    public interface IBasePageRxUI<TPageModel> : IBasePage<TPageModel>, IViewFor<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }


}