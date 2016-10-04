using System;
using System.Windows.Input;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// PageFactory ICommand implementation.
	/// </summary>
	public class BaseCommand<T> : IBaseCommand
	{
		readonly Action<T> execute;
		readonly Func<T, bool> canExecute;

		/// <summary>
		/// Initializes a new instance of the <see cref="DLToolkit.PageFactory.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		public BaseCommand(Action<T> execute) : this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DLToolkit.PageFactory.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		/// <param name="canExecute">Can execute.</param>
		public BaseCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute Action");
			}

			this.execute = execute;
			this.canExecute = canExecute;
		}

		/// <summary>
		/// Occurs when can execute changed.
		/// </summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Raises the can execute changed.
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			var handler = CanExecuteChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Determines whether this instance can execute the specified parameter.
		/// </summary>
		/// <returns><c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.</returns>
		/// <param name="parameter">Parameter.</param>
		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return canExecute == null || canExecute((T)parameter);
		}

		/// <summary>
		/// Execute the specified action.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		public void Execute(object parameter)
		{
			execute((T)parameter);
		}		
	}

	/// <summary>
	/// PageFactory ICommand implementation.
	/// </summary>
	public class BaseCommand : IBaseCommand
	{
		readonly Action execute;
		readonly Func<bool> canExecute;

		/// <summary>
		/// Initializes a new instance of the <see cref="DLToolkit.PageFactory.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		public BaseCommand(Action execute) : this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DLToolkit.PageFactory.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		/// <param name="canExecute">Can execute.</param>
		public BaseCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute Action");
			}

			this.execute = execute;
			this.canExecute = canExecute;
		}

		/// <summary>
		/// Occurs when can execute changed.
		/// </summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Raises the can execute changed.
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			var handler = CanExecuteChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Determines whether this instance can execute the specified parameter.
		/// </summary>
		/// <returns><c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.</returns>
		/// <param name="parameter">Parameter.</param>
		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return canExecute == null || canExecute();
		}
			
		/// <summary>
		/// Execute the specified action.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		public void Execute(object parameter)
		{
			execute();
		}
	}
}

