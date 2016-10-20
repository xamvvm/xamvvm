using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Diagnostics;

namespace Xamvvm
{
    /// <summary>
    /// xamvvm BasePageModel implementation.
    /// </summary>
	public class BasePageModel : BaseNotifyPropertyChanged, INotifyPropertyChanged, IBasePageModel, INavigationInterceptors
    {
        private readonly object syncRoot = new object();

        readonly Dictionary<string, object> fieldValuesDictionary = new Dictionary<string, object>();

        /// <summary>
        /// Sets the field and calls OnPropertyChanged when field value was changed. 
        /// Uses internal dictionary for field storing
        /// </summary>
        /// <returns><c>true</c>, if field was changed, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <typeparam name="T">The property type.</typeparam>
        protected bool SetField<T>(T value, [CallerMemberName] string propertyName = null)
        {
            T field = GetField<T>(propertyName);

            if (fieldValuesDictionary.ContainsKey(propertyName) && EqualityComparer<T>.Default.Equals(field, value)) 
                return false;

            lock(syncRoot)
            {
                fieldValuesDictionary[propertyName] = value;
            }

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets the field and calls OnPropertyChanged when field value was changed. 
        /// Uses internal dictionary for field storing
        /// </summary>
        /// <returns><c>true</c>, if field was changed, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="propertyNameSelector">Property name selector.</param>
        /// <param name="additonalPropertiesToNotify">Additonal properties to notify when changed.</param>
        protected bool SetField<T>(T value, Expression<Func<T>> propertyNameSelector, params Expression<Func<object>>[] additonalPropertiesToNotify)
        {
            var propertyName = GetPropertyNameFromExpression(propertyNameSelector);
            T field = GetField<T>(propertyName);

            if (fieldValuesDictionary.ContainsKey(propertyName) && EqualityComparer<T>.Default.Equals(field, value)) 
                return false;

            lock(fieldValuesDictionary)
            {
                fieldValuesDictionary[propertyName] = value;
            }

            OnPropertyChanged(propertyName);

            foreach (var item in additonalPropertiesToNotify)
                NotifyPropertyChanged(item);

            return true;
        }

        /// <summary>
        /// Gets the field.
        /// Uses internal dictionary to get field value
        /// </summary>
        /// <returns>The field.</returns>
        /// <param name="propertyName">Property name.</param>
        /// <typeparam name="T">The property type.</typeparam>
        protected T GetField<T>([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName cannot be null");

            object value = null;
            if (fieldValuesDictionary.TryGetValue(propertyName, out value))
            {
                return value == null ? default(T) : (T)value;
            }

            return default(T);
        }

		/// <summary>
		/// Called when page is being removed from cache.
		/// </summary>
		public virtual void NavigationRemovingFromCache()
		{
		}

		/// <summary>
		/// Determines if page can be pushed
		/// </summary>
		/// <returns><c>true</c>, if can ve pushed, <c>false</c> otherwise.</returns>
		public virtual bool NavigationCanPush()
		{
			return true;
		}

		/// <summary>
		/// Called after page is pushed to navigation
		/// </summary>
		public virtual void NavigationPushed()
		{
		}

		/// <summary>
		/// Determines if page can be popped
		/// </summary>
		/// <returns><c>true</c>, if can be popped, <c>false</c> otherwise.</returns>
		public virtual bool NavigationCanPop()
		{
			return true;
		}

		/// <summary>
		/// Called after page is popped from navigation
		/// </summary>
		public virtual void NavigationPopped()
		{
		}

		/// <summary>
		/// Determines if page can be inserted
		/// </summary>
		/// <returns><c>true</c>, if can be inserted, <c>false</c> otherwise.</returns>
		public virtual bool NavigationCanInsert()
		{
			return true;
		}

		/// <summary>
		/// Called after page is inserted to navigation
		/// </summary>
		public virtual void NavigationInserted()
		{
		}

		/// <summary>
		/// Determines if page can be removed
		/// </summary>
		/// <returns><c>true</c>, if can be removed, <c>false</c> otherwise.</returns>
		public virtual bool NavigationCanRemove()
		{
			return true;
		}

		/// <summary>
		/// Called after page is removed from navigation
		/// </summary>
		public virtual void NavigationRemoved()
		{
		}
	}
}

