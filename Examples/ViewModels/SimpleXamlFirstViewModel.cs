using System;
using DLToolkit.PageFactory;

namespace PageFactory.Examples.ViewModels
{
    public class SimpleXamlFirstViewModel : BasePageModel
	{
		public SimpleXamlFirstViewModel()
		{
			OpenSecondPageCommand = new PageFactoryCommand(() => 
				PageFactory.GetMessagablePageFromCache<SimpleXamlSecondViewModel>()
				.SendMessageToPageModel("Hello", this, Guid.NewGuid())
				.PushPage());
		}

		public IPageFactoryCommand OpenSecondPageCommand { get; private set; }
	}
}

