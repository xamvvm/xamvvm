using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamvvm;

namespace Examples
{
	public partial class DetailPage : ContentPage, IBasePage<DetailPageModel>
	{
		public DetailPage()
		{
			InitializeComponent();
		}
	}
}
