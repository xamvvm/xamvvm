using System;
using System.Threading.Tasks;

namespace Xamvvm
{
	public partial class XamvvmMockFactory : IBaseFactory
	{
		public async virtual Task<bool> PushPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			var navEventsPage = pageToPush as INavigationCanPush;
			if (navEventsPage != null && !navEventsPage.NavigationCanPush())
				return false;

			var navEventsPageModel = pageToPush.GetPageModel() as INavigationCanPush;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPush())
				return false;

			if (animated)
				await Task.Delay(500);

			var navEventsPage2 = pageToPush as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPushed();

			return true;
		}

		public async virtual Task<bool> PushModalPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			var navEventsPage = pageToPush as INavigationCanPush;
			if (navEventsPage != null && !navEventsPage.NavigationCanPush())
				return false;

			var navEventsPageModel = pageToPush.GetPageModel() as INavigationCanPush;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPush())
				return false;

			if (animated)
				await Task.Delay(500);

			var navEventsPage2 = pageToPush as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPushed();

			return true;
		}

		public virtual Task<bool> InsertPageBeforeAsync<TPageModel, TBeforePageModel>(IBasePage<TPageModel> pageToInsert, IBasePage<TBeforePageModel> beforePage) where TPageModel : class, IBasePageModel where TBeforePageModel : class, IBasePageModel
		{
			var navEventsPage = pageToInsert as INavigationCanInsert;
			if (navEventsPage != null && !navEventsPage.NavigationCanInsert())
				return Task.FromResult(false);

			var navEventsPageModel = pageToInsert.GetPageModel() as INavigationCanInsert;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanInsert())
				return Task.FromResult(false);

			var navEventsPage2 = pageToInsert as INavigationInserted;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationInserted();

			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
			var navEventsPage = currentPage as INavigationCanPop;

			if (navEventsPage != null && !navEventsPage.NavigationCanPop())
				return false;

			var navEventsPageModel = currentPage.GetPageModel() as INavigationCanPop;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPop())
				return false;

			if (animated)
				await Task.Delay(500);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPopped();

			return true;
		}

		public async virtual Task<bool> PopModalPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
			var navEventsPage = currentPage as INavigationCanPop;

			if (navEventsPage != null && !navEventsPage.NavigationCanPop())
				return false;

			var navEventsPageModel = currentPage.GetPageModel() as INavigationCanPop;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPop())
				return false;

			if (animated)
				await Task.Delay(500);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPopped();

			return true;
		}

		public virtual Task<bool> RemovePageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToRemove) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			var navEventsPage = pageToRemove as INavigationCanRemove;
			if (navEventsPage != null && !navEventsPage.NavigationCanRemove())
				return Task.FromResult(false);

			var navEventsPageModel = pageToRemove.GetPageModel() as INavigationCanRemove;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanRemove())
				return Task.FromResult(false);

			var navEventsPage2 = pageToRemove as INavigationRemoved;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationRemoved();

			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPagesToRootAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool clearCache = false, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
			if (animated)
				await Task.Delay(500);

			if (clearCache)
				ClearPageCache();

			return true;
		}

		public virtual Task<bool> SetNewRootAndResetAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> newRootPage, bool clearCache = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			if (clearCache)
				ClearPageCache();

			return Task.FromResult(true);
		}
	}
}
