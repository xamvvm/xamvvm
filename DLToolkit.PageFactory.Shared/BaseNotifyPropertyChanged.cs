using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace DLToolkit.PageFactory
{
	public class BaseNotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) 
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, Expression<Func<T>> selectorExpression, params Expression<Func<object>>[] additonalPropertiesNotify)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) 
				return false;

			field = value;
			NotifyPropertyChanged(selectorExpression);

			foreach (var item in additonalPropertiesNotify)
				NotifyPropertyChanged(item);

			return true;
		}

		protected bool SetField<T>(ref T field, T value, Expression<Func<T>> selectorExpression, 
			Action propertyChanged, params Expression<Func<object>>[] additonalPropertiesNotify)
		{
			bool changed = SetField(ref field, value, selectorExpression, additonalPropertiesNotify);

			if (changed)
			{
				propertyChanged();
			}

			return changed;
		}

		protected void NotifyPropertyChanged<T>(Expression<Func<T>> selectorExpression)
		{
			if (selectorExpression == null)
				throw new ArgumentNullException("NotifyPropertyChanged Selector Expression");
			var me = selectorExpression.Body as MemberExpression;

			// Nullable properties can be nested inside of a convert function
			if (me == null)
			{
				var ue = selectorExpression.Body as UnaryExpression;
				if (ue != null)
					me = ue.Operand as MemberExpression;
			}

			if (me == null)
				throw new ArgumentException("The body must be a member expression");

			OnPropertyChanged(me.Member.Name);
		}

		public void NotifyAllPropertiesChanged()
		{
			var properties = GetType().GetRuntimeProperties();

			foreach (var prop in properties)
			{
				OnPropertyChanged(prop.Name);
			}
		}
	}
}

