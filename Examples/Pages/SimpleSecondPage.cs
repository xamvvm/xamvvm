using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
    public class SimpleSecondPage : ContentPage, IBasePage<SimpleSecondViewModel>, INavigationPushing, INavigationPushed, INavigationPopping, INavigationPopped, INavigationRemovingFromCache
	{
		public SimpleSecondPage()
		{
			this.SetBinding<SimpleSecondViewModel>(Page.TitleProperty, v => v.PageTitle);

			var button = new Button() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "Pop page"
			};
			button.SetBinding<SimpleSecondViewModel>(Button.CommandProperty, v => v.PopPageCommand);

			Content = button;
		}

        public bool PageFactoryPushing()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPushing");
            // if false is returned Pushing will be cancelled

			return true;
		}

        public void PageFactoryPushed()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPushed");
		}

        public bool PageFactoryPopping()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPopping");

            return true;
		}

        public void PageFactoryPopped()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPopped");
		}

        public void PageFactoryRemovingFromCache()
		{
			// FREE some resources here
		}
	}
}


