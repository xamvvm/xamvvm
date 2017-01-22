using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Xamvvm
{
    public class BaseNavigationPageRxUI<TPageModel> : ReactiveNavigationPage<TPageModel>, IBasePageRxUI<TPageModel> where TPageModel : class, IBasePageModel, INotifyPropertyChanged
    {
    }
}
