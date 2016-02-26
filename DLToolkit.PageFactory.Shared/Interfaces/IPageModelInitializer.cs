using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    /// <summary>
    /// IPageModelInitializer.
    /// </summary>
    public interface IPageModelInitializer<out TPageModel> where TPageModel: class, INotifyPropertyChanged
    {
        /// <summary>
        /// PageModel initializer.
        /// </summary>
        /// <returns>ViewModel.</returns>
        TPageModel PageModelInitializer();
    }
}

