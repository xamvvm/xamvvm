using System;
using System.Windows.Input;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IBaseCommand.
	/// </summary>
	public interface IBaseCommand : ICommand
	{
		/// <summary>
		/// Raises the can execute changed.
		/// </summary>
		void RaiseCanExecuteChanged();
	}
}

