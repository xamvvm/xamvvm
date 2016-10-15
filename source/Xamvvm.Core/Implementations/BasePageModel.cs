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
    public class BasePageModel : BaseNotifyPropertyChanged, INotifyPropertyChanged, IBasePageModel
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
    }
}

