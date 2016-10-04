using System;
using DLToolkit.PageFactory;

namespace Examples
{
	public class MainPageModel : BasePageModel
	{
		public MainPageModel()
		{
			WelcomeText = "Welcome to PageFactory!";
		}

		public string WelcomeText
		{
			get { return GetField<string>(); }
			set { SetField(value); }
		}
	}
}
