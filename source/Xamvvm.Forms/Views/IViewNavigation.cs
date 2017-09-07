using System;
using System.Threading.Tasks;

namespace Xamvvm
{
    public interface IViewNavigation<TNavigationViewModel> where TNavigationViewModel : class, IBaseViewModel
    {
        Task PopViewAsync();

        Task PushViewAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IBaseViewModel;

        Task SetMainViewAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IBaseViewModel;

        Task PopToRootAsync();

        void ClearCache();
    }
}
