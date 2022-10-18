using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
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
				readonly WeakEventManager _weakEventManager = new WeakEventManager();
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
				public event EventHandler CanExecuteChanged
				{
						add { _weakEventManager.AddEventHandler(value); }
						remove { _weakEventManager.RemoveEventHandler(value); }
				}

				/// <summary>
				/// Raises the can execute changed.
				/// </summary>
				public void RaiseCanExecuteChanged()
				{
						_weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));
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
				readonly Func<object, Task> _execute;
				readonly Func<bool> _canExecuteFunc;
				readonly WeakEventManager _weakEventManager = new WeakEventManager();
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
				public event EventHandler CanExecuteChanged
				{
						add { _weakEventManager.AddEventHandler(value); }
						remove { _weakEventManager.RemoveEventHandler(value); }
				}

				/// <summary>
				/// Raises the can execute changed.
				/// </summary>
				public void RaiseCanExecuteChanged()
				{
						_weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));
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

		class WeakEventManager
		{
				readonly Dictionary<string, List<Subscription>> _eventHandlers = new Dictionary<string, List<Subscription>>();

				public void AddEventHandler<TEventArgs>(EventHandler<TEventArgs> handler, [CallerMemberName]string eventName = null)
						where TEventArgs : EventArgs
				{
						if (string.IsNullOrEmpty(eventName))
								throw new ArgumentNullException(nameof(eventName));

						if (handler == null)
								throw new ArgumentNullException(nameof(handler));

						AddEventHandler(eventName, handler.Target, handler.GetMethodInfo());
				}

				public void AddEventHandler(EventHandler handler, [CallerMemberName]string eventName = null)
				{
						if (string.IsNullOrEmpty(eventName))
								throw new ArgumentNullException(nameof(eventName));

						if (handler == null)
								throw new ArgumentNullException(nameof(handler));

						AddEventHandler(eventName, handler.Target, handler.GetMethodInfo());
				}

				public void HandleEvent(object sender, object args, string eventName)
				{
						var toRaise = new List<(object subscriber, MethodInfo handler)>();
						var toRemove = new List<Subscription>();

						if (_eventHandlers.TryGetValue(eventName, out List<Subscription> target))
						{
								for (int i = 0; i < target.Count; i++)
								{
										Subscription subscription = target[i];
										bool isStatic = subscription.Subscriber == null;
										if (isStatic)
										{
												// For a static method, we'll just pass null as the first parameter of MethodInfo.Invoke
												toRaise.Add((null, subscription.Handler));
												continue;
										}

										object subscriber = subscription.Subscriber.Target;

										if (subscriber == null)
												// The subscriber was collected, so there's no need to keep this subscription around
												toRemove.Add(subscription);
										else
												toRaise.Add((subscriber, subscription.Handler));
								}

								for (int i = 0; i < toRemove.Count; i++)
								{
										Subscription subscription = toRemove[i];
										target.Remove(subscription);
								}
						}

						for (int i = 0; i < toRaise.Count; i++)
						{
								(var subscriber, var handler) = toRaise[i];
								handler.Invoke(subscriber, new[] { sender, args });
						}
				}

				public void RemoveEventHandler<TEventArgs>(EventHandler<TEventArgs> handler, [CallerMemberName]string eventName = null)
						where TEventArgs : EventArgs
				{
						if (string.IsNullOrEmpty(eventName))
								throw new ArgumentNullException(nameof(eventName));

						if (handler == null)
								throw new ArgumentNullException(nameof(handler));

						RemoveEventHandler(eventName, handler.Target, handler.GetMethodInfo());
				}

				public void RemoveEventHandler(EventHandler handler, [CallerMemberName]string eventName = null)
				{
						if (string.IsNullOrEmpty(eventName))
								throw new ArgumentNullException(nameof(eventName));

						if (handler == null)
								throw new ArgumentNullException(nameof(handler));

						RemoveEventHandler(eventName, handler.Target, handler.GetMethodInfo());
				}

				void AddEventHandler(string eventName, object handlerTarget, MethodInfo methodInfo)
				{
						if (!_eventHandlers.TryGetValue(eventName, out List<Subscription> targets))
						{
								targets = new List<Subscription>();
								_eventHandlers.Add(eventName, targets);
						}

						if (handlerTarget == null)
						{
								// This event handler is a static method
								targets.Add(new Subscription(null, methodInfo));
								return;
						}

						targets.Add(new Subscription(new WeakReference(handlerTarget), methodInfo));
				}

				void RemoveEventHandler(string eventName, object handlerTarget, MemberInfo methodInfo)
				{
						if (!_eventHandlers.TryGetValue(eventName, out List<Subscription> subscriptions))
								return;

						for (int n = subscriptions.Count; n > 0; n--)
						{
								Subscription current = subscriptions[n - 1];

								if (current.Subscriber?.Target != handlerTarget || current.Handler.Name != methodInfo.Name)
										continue;

								subscriptions.Remove(current);
								break;
						}
				}

				struct Subscription
				{
						public Subscription(WeakReference subscriber, MethodInfo handler)
						{
								Subscriber = subscriber;
								Handler = handler ?? throw new ArgumentNullException(nameof(handler));
						}

						public readonly WeakReference Subscriber;
						public readonly MethodInfo Handler;
				}
		}
}

