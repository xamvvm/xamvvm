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
    public class BaseContentPageRxUI<TPageModel> : ContentPage,  IBasePageRxUI<TPageModel> where TPageModel : BasePageModelRxUI
    {
        /// The ViewModel to display
        /// </summary>
        public TPageModel ViewModel
        {
            get { return (TPageModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<BaseContentPageRxUI<TPageModel>, TPageModel>(x => x.ViewModel, default(TPageModel), BindingMode.OneWay);

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (TPageModel)value; }
        }

    }
}
