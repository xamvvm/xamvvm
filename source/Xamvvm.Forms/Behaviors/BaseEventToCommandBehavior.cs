using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace Xamvvm
{
	public class BaseEventToCommandBehavior : BaseBehavior<View>
	{
		Delegate eventHandler;

		public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(BaseEventToCommandBehavior), null, propertyChanged: OnEventNameChanged);
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(BaseEventToCommandBehavior), null);
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(BaseEventToCommandBehavior), null);
		public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(BaseEventToCommandBehavior), null);

		public string EventName
		{
			get { return (string)GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public IValueConverter Converter
		{
			get { return (IValueConverter)GetValue(InputConverterProperty); }
			set { SetValue(InputConverterProperty, value); }
		}

		protected override void OnAttachedTo(View bindable)
		{
			base.OnAttachedTo(bindable);
			RegisterEvent(EventName);
		}

		protected override void OnDetachingFrom(View bindable)
		{
			DeregisterEvent(EventName);
			base.OnDetachingFrom(bindable);
		}

		void RegisterEvent(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return;
			}

			EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
			if (eventInfo == null)
			{
				throw new ArgumentException(string.Format("BaseEventToCommandBehavior: Can't register the '{0}' event.", EventName));
			}
			MethodInfo methodInfo = typeof(BaseEventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
			eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
			eventInfo.AddEventHandler(AssociatedObject, eventHandler);
		}

		void DeregisterEvent(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return;
			}

			if (eventHandler == null)
			{
				return;
			}
			EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);
			if (eventInfo == null)
			{
				throw new ArgumentException(string.Format("BaseEventToCommandBehavior: Can't de-register the '{0}' event.", EventName));
			}
			eventInfo.RemoveEventHandler(AssociatedObject, eventHandler);
			eventHandler = null;
		}

		void OnEvent(object sender, object eventArgs)
		{
			if (Command == null)
			{
				return;
			}

			object resolvedParameter;
			if (CommandParameter != null)
			{
				resolvedParameter = CommandParameter;
			}
			else if (Converter != null)
			{
				resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
			}
			else {
				resolvedParameter = eventArgs;
			}

			if (Command.CanExecute(resolvedParameter))
			{
				Command.Execute(resolvedParameter);
			}
		}

		static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var behavior = (BaseEventToCommandBehavior)bindable;
			if (behavior.AssociatedObject == null)
			{
				return;
			}

			string oldEventName = (string)oldValue;
			string newEventName = (string)newValue;

			behavior.DeregisterEvent(oldEventName);
			behavior.RegisterEvent(newEventName);
		}
	}
}
