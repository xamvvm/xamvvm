using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
	public class SimpleSecondPage : PFContentPage<SimpleSecondViewModel>
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

		public override void PageFactoryMessageReceived(string message, object sender, object arg)
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage received {0} message from {1} with arg: {2}",  
				message, sender.GetType(), arg);
		}

		public override bool PageFactoryPushing()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPushing");

			return base.PageFactoryPushing();
		}

		public override void PageFactoryPushed()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPushed");
		}

		public override bool PageFactoryPopping()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPopping");

			return base.PageFactoryPopping();
		}

		public override void PageFactoryPopped()
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondPage PageFactoryPopped");
		}

		public override void PageFactoryRemovingFromCache()
		{
			// FREE some resources here

			base.PageFactoryRemovingFromCache();
		}
	}
}


