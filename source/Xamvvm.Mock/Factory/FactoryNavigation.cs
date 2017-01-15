using System;
using System.Threading.Tasks;

namespace Xamvvm
{
	public partial class XamvvmMockFactory : IBaseFactory
	{
		public  virtual async Task<bool> PushPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
            LastAction = XammvvmAction.PagePushed;
            TargetPageModel = pageToPush.GetPageModel();

            var navEventsPage = pageToPush as INavigationCanPush;
		    if (navEventsPage != null && !navEventsPage.NavigationCanPush())
		    {
		        LastActionSuccess = false;
                return false;
            }

			var navEventsPageModel = pageToPush.GetPageModel() as INavigationCanPush;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPush())
            {
                LastActionSuccess = false;
                return false;
            }

            if (animated)
				await Task.Delay(500);

			var navEventsPage2 = pageToPush as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPushed();

			var navEventsPageModel2 = pageToPush.GetPageModel() as INavigationPushed;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPushed();

            LastAction = XammvvmAction.PagePushed;
            LastActionSuccess = true;
			return true;
		}

		public async virtual Task<bool> PushModalPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
            TargetPageModel = pageToPush.GetPageModel();
            LastAction = XammvvmAction.ModalPagePushed;

            var navEventsPage = pageToPush as INavigationCanPush;
			if (navEventsPage != null && !navEventsPage.NavigationCanPush())
            {
                LastActionSuccess = false;
                return false;
            }

            var navEventsPageModel = pageToPush.GetPageModel() as INavigationCanPush;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPush())
            {
                LastActionSuccess = false;
                return false;
            }

            if (animated)
				await Task.Delay(500);

			var navEventsPage2 = pageToPush as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPushed();

			var navEventsPageModel2 = pageToPush.GetPageModel() as INavigationPushed;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPushed();

            LastActionSuccess = true;
            return true;
		}

		public virtual Task<bool> InsertPageBeforeAsync<TPageModel, TBeforePageModel>(IBasePage<TPageModel> pageToInsert, IBasePage<TBeforePageModel> beforePage) where TPageModel : class, IBasePageModel where TBeforePageModel : class, IBasePageModel
		{
            TargetPageModel = pageToInsert.GetPageModel();
            LastAction = XammvvmAction.PageInserted;

            var navEventsPage = pageToInsert as INavigationCanInsert;
		    if (navEventsPage != null && !navEventsPage.NavigationCanInsert())
		    {
		        LastActionSuccess = false;
		        return Task.FromResult(false);
		    }

		    var navEventsPageModel = pageToInsert.GetPageModel() as INavigationCanInsert;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanInsert())
            {
                LastActionSuccess = false;
                return Task.FromResult(false);
            }

            var navEventsPage2 = pageToInsert as INavigationInserted;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationInserted();

			var navEventsPageModel2 = pageToInsert.GetPageModel() as INavigationInserted;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationInserted();

		    LastActionSuccess = true;
			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
            TargetPageModel = currentPage.GetPageModel();
            LastAction = XammvvmAction.PagePopped;


            var navEventsPage = currentPage as INavigationCanPop;

			if (navEventsPage != null && !navEventsPage.NavigationCanPop())
            {
                LastActionSuccess = false;
                return false;
            }

            var navEventsPageModel = currentPage.GetPageModel() as INavigationCanPop;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPop())
            {
                LastActionSuccess = false;
                return false;
            }

            if (animated)
				await Task.Delay(500);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPopped();

			var navEventsPageModel2 = currentPage.GetPageModel() as INavigationPopped;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPopped();

		    LastActionSuccess = true;
			return true;
		}

		public async virtual Task<bool> PopModalPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
            TargetPageModel = currentPage.GetPageModel();
            LastAction = XammvvmAction.ModalPagePopped;

            var navEventsPage = currentPage as INavigationCanPop;

			if (navEventsPage != null && !navEventsPage.NavigationCanPop())
            {
                LastActionSuccess = false;
                return false;
            }

            var navEventsPageModel = currentPage.GetPageModel() as INavigationCanPop;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPop())
            {
                LastActionSuccess = false;
                return false;
            }

            if (animated)
				await Task.Delay(500);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
            {
                LastActionSuccess = false;
                return false;
            }

			var navEventsPageModel2 = currentPage.GetPageModel() as INavigationPopped;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPopped();

		    LastActionSuccess = true;
            return true;
		}

		public virtual Task<bool> RemovePageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToRemove) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
            TargetPageModel = currentPage.GetPageModel();
            LastAction = XammvvmAction.PageRemoved;

            var navEventsPage = pageToRemove as INavigationCanRemove;
			if (navEventsPage != null && !navEventsPage.NavigationCanRemove())
            {
                LastActionSuccess = false;
                return Task.FromResult(false);
            }

            var navEventsPageModel = pageToRemove.GetPageModel() as INavigationCanRemove;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanRemove())
            {
                LastActionSuccess = false;
                return Task.FromResult(false);
            }

            var navEventsPage2 = pageToRemove as INavigationRemoved;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationRemoved();

			var navEventsPageModel2 = pageToRemove.GetPageModel() as INavigationRemoved;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationRemoved();

		    LastActionSuccess = true;
			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPagesToRootAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool clearCache = false, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
		    TargetPageModel = null;
			if (animated)
				await Task.Delay(500);

			if (clearCache)
				ClearPageCache();

            LastAction = XammvvmAction.PoppedToRoot;
            LastActionSuccess = true;
			return true;
		}

		public virtual Task<bool> SetNewRootAndResetAsync<TPageModel>(IBasePage<TPageModel> newRootPage, bool clearCache = true)  where TPageModel : class, IBasePageModel
		{

            TargetPageModel = newRootPage.GetPageModel();

            if (clearCache)
				ClearPageCache();

            LastAction = XammvvmAction.SetNewRootAndReset;
            LastActionSuccess = true;
			return Task.FromResult(true);
		}

        public virtual Task<bool> SetNewRootAndResetAsync<TPageModelOfNewRoot>(bool clearCache = true) where TPageModelOfNewRoot : class, IBasePageModel
        {
            TargetPageModel = XamvvmCore.CurrentFactory.GetPageFromCache<TPageModelOfNewRoot>().GetPageModel();

            if (clearCache)
                ClearPageCache();

            LastAction = XammvvmAction.SetNewRootAndReset;
            LastActionSuccess = true;
            return Task.FromResult(true);
        }


    }
}
