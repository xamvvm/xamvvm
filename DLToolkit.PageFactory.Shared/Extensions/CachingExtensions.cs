using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    /// <summary>
    /// Caching extensions.
    /// </summary>
    public static class CachingExtensions
    {
        /// <summary>
        /// Removes the page instance from cache if instance exists in cache.
        /// </summary>
        /// <returns>Page.</returns>
        /// <param name="page">Page.</param>
        public static IBasePage<TPageModel> RemovePageInstanceFromCache<TPageModel>(this IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.RemovePageInstanceFromCache(page);
            return page;
        }

        /// <summary>
        /// Removes the page instance from cache if instance exists in cache.
        /// </summary>
        /// <returns>Page.</returns>
        /// <param name="page">Page.</param>
        public static IBaseMessagablePage<TPageModel> RemovePageInstanceFromCache<TPageModel>(this IBaseMessagablePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.RemovePageInstanceFromCache(page);
            return page;
        }

        /// <summary>
        /// Removes the page type from cache if type exists in cache.
        /// </summary>
        /// <returns>Page.</returns>
        /// <param name="page">Page.</param>
        public static IBasePage<TPageModel> RemovePageTypeFromCache<TPageModel>(this IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.RemovePageTypeFromCache(page.GetType());
            return page;
        }

        /// <summary>
        /// Removes the page type from cache if type exists in cache.
        /// </summary>
        /// <returns>Page.</returns>
        /// <param name="page">Page.</param>
        public static IBaseMessagablePage<TPageModel> RemovePageTypeFromCache<TPageModel>(this IBaseMessagablePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.RemovePageTypeFromCache(page.GetType());
            return page;
        }
    }
}

