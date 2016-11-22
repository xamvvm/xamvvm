using System;
using System.Windows.Input;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Xamvvm
{
	/// <summary>
	/// xamvvm ICommand implementation.
	/// </summary>
	public class BaseCommand<T> : IBaseCommand
	{
		readonly Func<T, Task> _execute;
		readonly Func<T, bool> _canExecuteFunc;
		bool _canExecute = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xamvvm.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public BaseCommand(Action<T> execute) : this(new Func<T, Task>(async (obj) => execute(obj)), null)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xamvvm.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		public BaseCommand(Func<T, Task> execute) : this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xamvvm.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		/// <param name="canExecute">Can execute.</param>
		public BaseCommand(Func<T, Task> execute, Func<T, bool> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));

			_execute = execute;
			_canExecuteFunc = canExecute;
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
			return _canExecute && (_canExecuteFunc == null || _canExecuteFunc((T)parameter));
		}

		/// <summary>
		/// Execute the specified action.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		public async void Execute(object parameter)
		{
			try
			{
				_canExecute = false;
				RaiseCanExecuteChanged();
				if (parameter == null)
					await _execute(default(T));
				else
					await _execute((T)parameter);
			}
			finally
			{
				_canExecute = true;
				RaiseCanExecuteChanged();
			}
		}		
	}

	/// <summary>
	/// xamvvm ICommand implementation.
	/// </summary>
	public class BaseCommand : IBaseCommand
	{
		/// <summary>
		/// Creates command from the task.
		/// </summary>
		/// <returns>The task.</returns>
		/// <param name="task">Task.</param>
		/// <param name="canExecute">Can execute.</param>
		public static BaseCommand FromTask(Task task, Func<bool> canExecute = null)
		{
			return new BaseCommand((arg) => task, canExecute);
		}

		/// <summary>
		/// Creates command from the func.
		/// </summary>
		/// <returns>The func.</returns>
		/// <param name="taskFunc">Task func.</param>
		/// <param name="canExecute">Can execute.</param>
		/// <typeparam name="TParam">The 1st type parameter.</typeparam>
		public static BaseCommand<TParam> FromFunc<TParam>(Func<TParam, Task> taskFunc, Func<TParam, bool> canExecute = null)
		{
			return new BaseCommand<TParam>((arg) => taskFunc?.Invoke(arg), canExecute);
		}

		/// <summary>
		/// Creates command from the action.
		/// </summary>
		/// <returns>The action.</returns>
		/// <param name="action">Action.</param>
		/// <param name="canExecute">Can execute.</param>
		public static BaseCommand FromAction(Action<object> action, Func<bool> canExecute = null)
		{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
			return new BaseCommand(async(arg) => action(arg), canExecute);
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		}

		readonly Func<object, Task> _execute;
		readonly Func<bool> _canExecuteFunc;
		bool _canExecute = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xamvvm.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public BaseCommand(Action<object> execute) : this(new Func<object, Task>(async (obj) => execute(obj)), null)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xamvvm.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		public BaseCommand(Func<object, Task> execute) : this(execute, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Xamvvm.BaseCommand"/> class.
		/// </summary>
		/// <param name="execute">Execute.</param>
		/// <param name="canExecute">Can execute.</param>
		public BaseCommand(Func<object, Task> execute, Func<bool> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException(nameof(execute));
			}

			_execute = execute;
			_canExecuteFunc = canExecute;
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
			return _canExecute && (_canExecuteFunc == null || _canExecuteFunc());
		}
			
		/// <summary>
		/// Execute the specified action.
		/// </summary>
		/// <param name="parameter">Parameter.</param>
		public async void Execute(object parameter)
		{
			try
			{
				_canExecute = false;
				RaiseCanExecuteChanged();
				await _execute(parameter);
			}
			finally
			{
				_canExecute = true;
				RaiseCanExecuteChanged();
			}
		}
	}
}

