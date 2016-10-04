using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactoryNavigation
    {
		public async virtual Task<bool> PushPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel, new() where TPageModel : class, IBasePageModel, new()
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = pageToPush as INavigationPushing;
			if (navigation == null || (navEventsPage != null && !navEventsPage.PageFactoryPushing()))
				return false;

			await navigation.PushAsync((Page)pageToPush, animated);

			var navEventsPage2 = pageToPush as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.PageFactoryPushed();

			return true;
		}

		public async virtual Task<bool> PushModalPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel, new() where TPageModel : class, IBasePageModel, new()
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = pageToPush as INavigationPushing;
			if (navigation == null || (navEventsPage != null && !navEventsPage.PageFactoryPushing()))
				return false;

			await navigation.PushModalAsync((Page)pageToPush, animated);

			var navEventsPage2 = pageToPush as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.PageFactoryPushed();

			return true;
		}

		public virtual Task<bool> InsertPageBeforeAsync<TPageModel, TBeforePageModel>(IBasePage<TPageModel> pageToInsert, IBasePage<TBeforePageModel> beforePage) where TPageModel : class, IBasePageModel, new() where TBeforePageModel : class, IBasePageModel, new()
		{
			var navigation = ((Page)beforePage)?.Navigation;
			var navEventsPage = pageToInsert as INavigationInserting;
			if (navigation == null || (navEventsPage != null && !navEventsPage.PageFactoryInserting()))
				return Task.FromResult(false);

			navigation.InsertPageBefore((Page)pageToInsert, (Page)beforePage);

			var navEventsPage2 = pageToInsert as INavigationInserted;
			if (navEventsPage2 != null)
				navEventsPage2.PageFactoryInserted();

			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel, new()
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = currentPage as INavigationPopping;

			if (navigation == null || (navEventsPage != null && !navEventsPage.PageFactoryPopping()))
				return false;

			await navigation.PopAsync(animated);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
				navEventsPage2.PageFactoryPopped();

			return true;
		}

		public async virtual Task<bool> PopModalPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel, new()
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = currentPage as INavigationPopping;

			if (navigation == null || (navEventsPage != null && !navEventsPage.PageFactoryPopping()))
				return false;

			await navigation.PopModalAsync(animated);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
				navEventsPage2.PageFactoryPopped();

			return true;
		}

		public virtual Task<bool> RemovePageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToRemove) where TCurrentPageModel : class, IBasePageModel, new() where TPageModel : class, IBasePageModel, new()
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = pageToRemove as INavigationRemoving;
			if (navigation == null || (navEventsPage != null && !navEventsPage.PageFactoryRemoving()))
				return Task.FromResult(false);

			navigation.RemovePage((Page)pageToRemove);

			var navEventsPage2 = pageToRemove as INavigationRemoved;
			if (navEventsPage2 != null)
				navEventsPage2.PageFactoryRemoved();

			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPagesToRootAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool clearCache = false, bool animated = true) where TCurrentPageModel : class, IBasePageModel, new()
		{
			var navigation = ((Page)currentPage)?.Navigation;
			if (navigation == null)
				return false;

			await navigation.PopToRootAsync(animated);

			if (clearCache)
				ClearCache();

			return true;
		}

		public virtual Task<bool> SetNewRootAndResetAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> newRootPage, bool clearCache = true) where TCurrentPageModel : class, IBasePageModel, new() where TPageModel : class, IBasePageModel, new()
		{
			if (clearCache)
				ClearCache();

			Application.Current.MainPage = (Page)newRootPage;

			return Task.FromResult(true);
		}
    }
}

