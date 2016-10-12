using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DLToolkit.PageFactory;
using Examples.PageModels;

namespace Examples
{
	public partial class MainPage : ContentPage, IBasePage<MainPageModel>
	{
		public MainPage()
		{
			InitializeComponent();
		}
	}
}
