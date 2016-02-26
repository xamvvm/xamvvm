using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
    public partial class SimpleXamlFirstPage : ContentPage, IBasePage<SimpleXamlFirstViewModel>
	{
		public SimpleXamlFirstPage()
		{
			InitializeComponent();
		}
	}
}

