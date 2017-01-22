using System.ComponentModel;
using ReactiveUI;
using Xamvvm;

namespace Xamvvm
{

    public interface IBasePageRxUI<TPageModel> : IBasePage<TPageModel>, IViewFor<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }


}