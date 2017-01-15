using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Xamvvm
{
    public class BaseContentPageRxUI<TPageModel> : ReactiveContentPage<TPageModel>,  IBasePage<TPageModel> where TPageModel : BasePageModelRxUI
    {
    }
}
