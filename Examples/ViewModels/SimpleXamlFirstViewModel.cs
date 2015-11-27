using System;
using DLToolkit.PageFactory;

namespace PageFactory.Examples.ViewModels
{
	public class SimpleXamlFirstViewModel : BaseViewModel
	{
		public SimpleXamlFirstViewModel()
		{
			OpenSecondPageCommand = new PageFactoryCommand(() => 
				PageFactory.GetMessagablePageFromCache<SimpleXamlSecondViewModel>()
				.SendMessageToViewModel("Hello", this, Guid.NewGuid())
				.PushPage());
		}

		public IPageFactoryCommand OpenSecondPageCommand { get; private set; }
	}
}

