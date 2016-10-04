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
		readonly Action<T> _execute;
		readonly Func<T, bool> _canExecuteFunc;
		bool _canExecute = true;

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
				throw new ArgumentNullException(nameof(execute));

			_execute = execute;
			_canExecuteFunc = canExecute;
		}

		/// <summary>
		/// Gets or sets the delay used for CanExecute.
		/// </summary>
		/// <value>The delay.</value>
		public int Delay { get; set; } = 500;

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
			return _canExecute && (_canExecuteFunc == null || _canExecuteFunc((T)parameter));
		}

		/// <summary>
		/// Execute the specified action.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		public void Execute(object parameter)
		{
			try
			{
				_canExecute = false;
				RaiseCanExecuteChanged();
				if (parameter == null)
					_execute(default(T));
				else
					_execute((T)parameter);
			}
			finally
			{
				_canExecute = true;
				RaiseCanExecuteChanged();
			}
		}		
	}

	/// <summary>
	/// PageFactory ICommand implementation.
	/// </summary>
	public class BaseCommand : IBaseCommand
	{
		readonly Action<object> _execute;
		readonly Func<bool> _canExecuteFunc;
		bool _canExecute = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="DLToolkit.PageFactory.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		public BaseCommand(Action<object> execute) : this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DLToolkit.PageFactory.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		/// <param name="canExecute">Can execute.</param>
		public BaseCommand(Action<object> execute, Func<bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute Action");
			}

			_execute = execute;
			_canExecuteFunc = canExecute;
		}

		/// <summary>
		/// Gets or sets the delay used for CanExecute.
		/// </summary>
		/// <value>The delay.</value>
		public int Delay { get; set; } = 500;

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
			return _canExecute && (_canExecuteFunc == null || _canExecuteFunc());
		}
			
		/// <summary>
		/// Execute the specified action.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		public void Execute(object parameter)
		{
			try
			{
				_canExecute = false;
				RaiseCanExecuteChanged();
				_execute(parameter);
			}
			finally
			{
				_canExecute = true;
				RaiseCanExecuteChanged();
			}
		}
	}
}

