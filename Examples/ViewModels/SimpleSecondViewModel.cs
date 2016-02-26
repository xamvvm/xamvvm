using System;
using DLToolkit.PageFactory;

namespace PageFactory.Examples.ViewModels
{
    public class SimpleSecondViewModel : BasePageModel
	{
		public SimpleSecondViewModel()
		{
			PageTitle = "SecondPage";

			PopPageCommand = new PageFactoryCommand(() => {
				this.GetPage()
					.PopPage();
			}); 
		}

		public override void PageFactoryMessageReceived(string message, object sender, object arg)
		{
			System.Diagnostics.Debug.WriteLine("SimpleSecondViewModel received {0} message from {1} with arg: {2}",  
				message, sender.GetType(), arg);
		}   

		public IPageFactoryCommand PopPageCommand { get; private set; }

		public string PageTitle
		{
			get { return GetField<string>(); }
			set { SetField(value); }
		}
	}
}

