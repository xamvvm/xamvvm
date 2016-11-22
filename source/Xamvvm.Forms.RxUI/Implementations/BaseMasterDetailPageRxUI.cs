using System;
using ReactiveUI;
using Xamarin.Forms;
using ReactiveUI.XamForms;

namespace Xamvvm
{
	public class BaseMasterDetailPageRxUI<TPageModel> : ReactiveMasterDetailPage<TPageModel>,  IBasePageRxUI<TPageModel> where TPageModel : BasePageModelRxUI
    {
    }
}
