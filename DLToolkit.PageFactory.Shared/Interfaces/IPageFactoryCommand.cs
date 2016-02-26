using System;
using System.Windows.Input;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IPageFactoryCommand.
	/// </summary>
	public interface IPageFactoryCommand : ICommand
	{
		/// <summary>
		/// Raises the can execute changed.
		/// </summary>
		void RaiseCanExecuteChanged();
	}
}

