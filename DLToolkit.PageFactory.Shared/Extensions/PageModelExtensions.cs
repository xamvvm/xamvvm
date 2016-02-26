using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    /// <summary>
    /// Page model extensions.
    /// </summary>
    public static class PageModelExtensions
    {
        /// <summary>
        /// Gets the page by page model.
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="pageModel">Page model.</param>
        /// <typeparam name="TPageModel">Page model type.</typeparam>
        public static IBasePage<TPageModel> GetPage<TPageModel>(this TPageModel pageModel) where TPageModel : class, INotifyPropertyChanged
        {
            var checkIfNotPageAlready = pageModel as IBasePage<TPageModel>;
            if (checkIfNotPageAlready != null)
                return checkIfNotPageAlready;

            return PF.Factory.GetPageByModel(pageModel);
        }

        /// <summary>
        /// Gets the Page PageModel.
        /// </summary>
        /// <returns>The page model.</returns>
        /// <param name="page">Page.</param>
        /// <typeparam name="TPageModel">The 1st type parameter.</typeparam>
        public static TPageModel GetPageModel<TPageModel>(this IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            return PF.Factory.GetPageModel(page);
        }

        /// <summary>
        /// Resets page page model.
        /// </summary>
        /// <param name = "page">Page.</param>
        public static IBasePage<TPageModel> ResetPageModel<TPageModel>(this IBasePage<TPageModel> page) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.ResetPageModel(page);
            return page;
        }
            
        /// <summary>
        /// Replaces page page model.
        /// </summary>
        /// <param name = "page">Page.</param>
        /// <param name="newPageModel">New page model.</param>
        public static IBasePage<TPageModel> ReplacePageModel<TPageModel>(this IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, INotifyPropertyChanged
        {
            PF.Factory.ReplacePageModel(page, newPageModel);
            return page;
        }
    }
}

