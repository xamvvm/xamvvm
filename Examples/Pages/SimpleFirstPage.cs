using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
    public class SimpleFirstPage : ContentPage, IBasePage<SimpleFirstViewModel>
	{
		public SimpleFirstPage()
		{
			this.SetBinding<SimpleFirstViewModel>(Page.TitleProperty, v => v.PageTitle);

			var button = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Text = "Open Second Page"
			};
			button.SetBinding<SimpleFirstViewModel>(Button.CommandProperty, v => v.OpenPageCommand);

			Content = button;
		}
	}
}


