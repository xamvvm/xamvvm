using System;
namespace Xamvvm
{
    /// <summary>
    /// Base view.
    /// </summary>
    public interface IBaseView<out TViewModel> where TViewModel : class, IBaseViewModel
    {
    }
}
