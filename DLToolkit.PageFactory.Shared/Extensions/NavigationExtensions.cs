using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    /// <summary>
    /// Navigation extensions.
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// Pushes the page into navigation stack.
        /// </summary>
        /// <param name="page">Page to push.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBasePage<TPageModel> PushPage<TPageModel>(this IBasePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PushPageAsync(page, animated);
            return page;
        }

        /// <summary>
        /// Pushes the page into navigation stack.
        /// </summary>
        /// <param name="page">Page to push.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBaseMessagablePage<TPageModel> PushPage<TPageModel>(this IBaseMessagablePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PushPageAsync(page, animated);
            return page;
        }
 
        /// <summary>
        /// Pushes the page as modal.
        /// </summary>
        /// <param name="page">Page to push.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBasePage<TPageModel> PushModalPage<TPageModel>(this  IBasePage<TPageModel> page, bool animated = true)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PushModalPageAsync(page, animated);
            return page;
        }

        /// <summary>
        /// Pushes the page as modal.
        /// </summary>
        /// <param name="page">Page to push.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBaseMessagablePage<TPageModel> PushModalPage<TPageModel>(this  IBaseMessagablePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PushModalPageAsync(page, animated);
            return page;
        }

        /// <summary>
        /// Pushes another page into current navigation page navigation stack.
        /// </summary>
        /// <param name = "navigation">Navigation page.</param>
        /// <param name="page">Page to push.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBaseNavigationPage<TNavigationPageModel> PushPageIntoNavigation<TNavigationPageModel, TPageModel>(this IBaseNavigationPage<TNavigationPageModel> navigation, IBasePage<TPageModel> page, bool animated = true) where TPageModel : class, INotifyPropertyChanged where TNavigationPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PushPageIntoNavigationAsync(navigation, page, animated);
            return navigation;
        }

        /// <summary>
        /// Pops the page from navigation stack.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBasePage<TPageModel> PopPage<TPageModel>(this  IBasePage<TPageModel> page, bool animated = true)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PopPageAsync(false, animated);
            return page;
        }

        /// <summary>
        /// Pops the page from navigation stack.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBaseMessagablePage<TPageModel> PopPage<TPageModel>(this  IBaseMessagablePage<TPageModel> page, bool animated = true)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PopPageAsync(false, animated);
            return page;
        }

        /// <summary>
        /// Pops the modal page.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBasePage<TPageModel> PopModalPage<TPageModel>(this  IBasePage<TPageModel> page, bool animated = true)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PopModalPageAsync(false, animated);
            return page;
        }

        /// <summary>
        /// Pops the modal page.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBaseMessagablePage<TPageModel> PopModalPage<TPageModel>(this  IBaseMessagablePage<TPageModel> page, bool animated = true)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PopModalPageAsync(false, animated);
            return page;
        }

        /// <summary>
        /// Removes page from Navigation.
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="page">Page.</param>
        public static IBasePage<TPageModel> RemovePage<TPageModel>(this  IBasePage<TPageModel> page)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.RemovePage(page);
            return page;
        }

        /// <summary>
        /// Removes page from Navigation.
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="page">Page.</param>
        public static IBaseMessagablePage<TPageModel> RemovePage<TPageModel>(this  IBaseMessagablePage<TPageModel> page)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.RemovePage(page);
            return page;
        }

        /// <summary>
        /// Inserts the page before another page in Navigation stack.
        /// </summary>
        /// <returns>The page before.</returns>
        /// <param name="page">Page.</param>
        /// <param name="before">Before.</param>
        public static IBasePage<TPageModel> InsertPageBefore<TPageModel, TBeforePageModel>(this  IBasePage<TPageModel> page, IBasePage<TBeforePageModel> before) where TPageModel : class, INotifyPropertyChanged where TBeforePageModel : class, INotifyPropertyChanged
        {
            PF.Factory.InsertPageBefore(page, before);
            return page;
        }

        /// <summary>
        /// Inserts the page before another page in Navigation stack.
        /// </summary>
        /// <returns>The page before.</returns>
        /// <param name="page">Page.</param>
        /// <param name="before">Before.</param>
        public static IBaseMessagablePage<TPageModel> InsertPageBefore<TPageModel, TBeforePageModel>(this  IBaseMessagablePage<TPageModel> page, IBasePage<TBeforePageModel> before) where TPageModel : class, INotifyPropertyChanged where TBeforePageModel : class, INotifyPropertyChanged
        {
            PF.Factory.InsertPageBefore(page, before);
            return page;
        }

        /// <summary>
        /// Pops the pages to root.
        /// </summary>
        /// <returns>Page.</returns>
        /// <param name="page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBasePage<TPageModel> PopPagesToRoot<TPageModel>(this  IBasePage<TPageModel> page, bool animated = true)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PopPagesToRootAsync(false, animated);
            return page;
        }

        /// <summary>
        /// Pops the pages to root.
        /// </summary>
        /// <returns>Page.</returns>
        /// <param name="page">Page.</param>
        /// <param name="animated">If set to <c>true</c> animation enabled.</param>
        public static IBaseMessagablePage<TPageModel> PopPagesToRoot<TPageModel>(this IBaseMessagablePage<TPageModel> page, bool animated = true)  where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.PopPagesToRootAsync(false, animated);
            return page;
        }
    }
}

