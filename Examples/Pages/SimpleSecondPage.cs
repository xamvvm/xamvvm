using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
    public class SimpleSecondPage : ContentPage, IBaseMessagablePage<SimpleSecondViewModel>, INavigationEvents
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

		public void PageFactoryMessageReceived(string message, object sender, object arg)
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage received {0} message from {1} with arg: {2}",  
				message, sender.GetType(), arg);
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

        public bool PageFactoryRemoving()
        {
            System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryRemoving");

            return true;
        }

        public bool PageFactoryInserting()
        {
            System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryInserting");

            return true;
        }

        public void PageFactoryRemoved()
        {
            System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryRemoved");
        }

        public void PageFactoryInserted()
        {
            System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryInserted");
        }
	}
}


