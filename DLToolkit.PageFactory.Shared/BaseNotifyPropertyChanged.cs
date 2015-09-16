using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// PageFactory IBaseNotifyPropertyChanged implementation.
	/// </summary>
	public class BaseNotifyPropertyChanged
	{
		/// <summary>
		/// Occurs when property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the property changed event.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) 
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Sets the field.
		/// </summary>
		/// <returns><c>true</c>, if field was set, <c>false</c> otherwise.</returns>
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		/// <param name="selectorExpression">Property name selector expression.</param>
		/// <param name="additonalPropertiesNotify">Additonal properties names to notify when changed.</param>
		/// <typeparam name="T">Property type.</typeparam>
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

		/// <summary>
		/// Sets the field.
		/// </summary>
		/// <returns><c>true</c>, if field was set, <c>false</c> otherwise.</returns>
		/// <param name="field">Field.</param>
		/// <param name="value">Value.</param>
		/// <param name="selectorExpression">Property name selector expression.</param>
		/// <param name="propertyChanged">Action if property was changed.</param>
		/// <param name="additonalPropertiesNotify">Additonal properties names to notify when changed.</param>
		/// <typeparam name="T">Property type.</typeparam>
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

		/// <summary>
		/// Notifies the property changed.
		/// </summary>
		/// <param name="selectorExpression">Property name selector expression.</param>
		/// <typeparam name="T">Property type.</typeparam>
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

		/// <summary>
		/// Notifies all properties changed.
		/// </summary>
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

