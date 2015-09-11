using System;

namespace DLToolkit.PageFactory
{
	public class PageFactoryCommand : Xamarin.Forms.Command
	{
		public PageFactoryCommand(Action execute) : base(execute)
		{
		}

		public PageFactoryCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute)
		{
		}
	}
}

