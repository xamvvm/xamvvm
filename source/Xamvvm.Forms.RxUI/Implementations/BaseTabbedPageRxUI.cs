using System;
using System.ComponentModel;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamvvm;

namespace Xamvvm
{
    public class BaseTabbedPageRxUI<TPageModel> : ReactiveTabbedPage<TPageModel>, IBasePageRxUI<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }
}
