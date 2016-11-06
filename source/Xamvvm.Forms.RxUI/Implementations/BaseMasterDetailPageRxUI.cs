using System;
using ReactiveUI;
using Xamarin.Forms;


namespace Xamvvm
{
	public class BaseMasterDetailPageRxUI<TPageModel> : MasterDetailPage,  IBasePageRxUI<TPageModel> where TPageModel : BasePageModelRxUI
    {

        /// The ViewModel to display
        /// </summary>
        public TPageModel ViewModel
        {
            get { return (TPageModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<BaseMasterDetailPageRxUI<TPageModel>, TPageModel>(x => x.ViewModel, default(TPageModel), BindingMode.OneWay);

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (TPageModel)value; }
        }


    }
}
