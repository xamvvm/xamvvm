using ReactiveUI;
using Xamvvm;

namespace Xamvvm
{

    public interface IBasePageRxUI<TPageModel> : IBasePage<TPageModel>, IViewFor<TPageModel> where TPageModel : BasePageModelRxUI
    {
    }


}