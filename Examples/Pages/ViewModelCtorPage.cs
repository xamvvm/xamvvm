using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
    public class ViewModelCtorPage : ContentPage, IBasePage<ViewModelCtorViewModel>, IPageModelInitializer<ViewModelCtorViewModel>
	{
		public ViewModelCtorPage()
		{
			Title = "ViewModelCtorPage";

			var label = new Label { 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center,
			};

			label.SetBinding<ViewModelCtorViewModel>(Label.TextProperty, v => v.LabelText);

			Content = label;
		}

		public ViewModelCtorViewModel PageModelInitializer()
		{
			return new ViewModelCtorViewModel(Guid.NewGuid());
		}
	}
}


