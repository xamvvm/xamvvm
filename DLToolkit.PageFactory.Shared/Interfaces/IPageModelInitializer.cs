using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    public interface IPageModelInitializer<out TPageModel> where TPageModel: class, INotifyPropertyChanged
    {
        /// <summary>
        /// PageModel initializer.
        /// </summary>
        /// <returns>ViewModel.</returns>
        TPageModel PageModelInitializer();
    }
}

