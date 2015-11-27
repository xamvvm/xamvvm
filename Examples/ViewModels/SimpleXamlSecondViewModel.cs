using System;
using DLToolkit.PageFactory;

namespace PageFactory.Examples.ViewModels
{
	public class SimpleXamlSecondViewModel : BaseViewModel
	{
		public SimpleXamlSecondViewModel()
		{
		}

		public override void PageFactoryMessageReceived(string message, object sender, object arg)
		{
			ReceivedMessage = string.Format(
				"Received message: {0} with arg: {1} from: {2}",
				message, arg, sender.GetType());
		}

		public string ReceivedMessage {
			get { return GetField<string>(); }
			set { SetField(value); }
		}
	}
}

