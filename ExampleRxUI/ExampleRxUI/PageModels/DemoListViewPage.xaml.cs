using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DLToolkit.PageFactory;
using ReactiveUI;

namespace ExampleRxUI
{
	public partial class DemoListViewPage : ContentPage, IBasePage<DemoListViewPageModel>, IViewFor<DemoListViewPageModel>
	{
		public DemoListViewPage()
		{
			InitializeComponent();
		}

		public DemoListViewPageModel ViewModel
		{
			get { return (DemoListViewPageModel)BindingContext; }
			set { BindingContext = value; }
		}

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (DemoListViewPageModel)value; }
		}
	}
}
