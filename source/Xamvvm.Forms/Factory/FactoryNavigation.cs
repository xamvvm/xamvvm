using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace Xamvvm
{
    public partial class XamvvmFormsFactory : IBaseFactoryNavigation
    {
		public async virtual Task<bool> PushPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = pageToPush as INavigationCanPush;
			if (navigation == null || (navEventsPage != null && !navEventsPage.NavigationCanPush()))
				return false;

			var navEventsPageModel = pageToPush.GetPageModel() as INavigationCanPush;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPush())
				return false;

			await navigation.PushAsync((Page)pageToPush, animated);

			var navEventsPage2 = navEventsPageModel as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPushed();

			var navEventsPageModel2 = pageToPush.GetPageModel() as INavigationPushed;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPushed();

			return true;
		}

		public async virtual Task<bool> PushModalPageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToPush, bool animated = true) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = pageToPush as INavigationCanPush;
			if (navigation == null || (navEventsPage != null && !navEventsPage.NavigationCanPush()))
				return false;

			var navEventsPageModel = pageToPush.GetPageModel() as INavigationCanPush;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPush())
				return false;

			await navigation.PushModalAsync((Page)pageToPush, animated);


			var navEventsPage2 = pageToPush as INavigationPushed;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPushed();

			var navEventsPageModel2 = pageToPush.GetPageModel() as INavigationPushed;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPushed();

			return true;
		}

		public virtual Task<bool> InsertPageBeforeAsync<TPageModel, TBeforePageModel>(IBasePage<TPageModel> pageToInsert, IBasePage<TBeforePageModel> beforePage) where TPageModel : class, IBasePageModel where TBeforePageModel : class, IBasePageModel
		{
			var navigation = ((Page)beforePage)?.Navigation;
			var navEventsPage = pageToInsert as INavigationCanInsert;
			if (navigation == null || (navEventsPage != null && !navEventsPage.NavigationCanInsert()))
				return Task.FromResult(false);

			var navEventsPageModel = pageToInsert.GetPageModel() as INavigationCanInsert;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanInsert())
				return Task.FromResult(false);

			navigation.InsertPageBefore((Page)pageToInsert, (Page)beforePage);

			var navEventsPage2 = pageToInsert as INavigationInserted;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationInserted();

			var navEventsPageModel2 = pageToInsert.GetPageModel() as INavigationInserted;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationInserted();

			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = currentPage as INavigationCanPop;

			if (navigation == null || (navEventsPage != null && !navEventsPage.NavigationCanPop()))
				return false;

			var navEventsPageModel = currentPage.GetPageModel() as INavigationCanPop;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPop())
				return false;

			await navigation.PopAsync(animated);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPopped();

			var navEventsPageModel2 = currentPage.GetPageModel() as INavigationPopped;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPopped();

			return true;
		}

		public async virtual Task<bool> PopModalPageAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = currentPage as INavigationCanPop;

			if (navigation == null || (navEventsPage != null && !navEventsPage.NavigationCanPop()))
				return false;

			var navEventsPageModel = currentPage.GetPageModel() as INavigationCanPop;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanPop())
				return false;

			await navigation.PopModalAsync(animated);

			var navEventsPage2 = currentPage as INavigationPopped;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationPopped();

			var navEventsPageModel2 = currentPage.GetPageModel() as INavigationPopped;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationPopped();

			return true;
		}

		public virtual Task<bool> RemovePageAsync<TCurrentPageModel, TPageModel>(IBasePage<TCurrentPageModel> currentPage, IBasePage<TPageModel> pageToRemove) where TCurrentPageModel : class, IBasePageModel where TPageModel : class, IBasePageModel
		{
			var navigation = ((Page)currentPage)?.Navigation;
			var navEventsPage = pageToRemove as INavigationCanRemove;
			if (navigation == null || (navEventsPage != null && !navEventsPage.NavigationCanRemove()))
				return Task.FromResult(false);

			var navEventsPageModel = pageToRemove.GetPageModel() as INavigationCanRemove;
			if (navEventsPageModel != null && !navEventsPageModel.NavigationCanRemove())
				return Task.FromResult(false);

			navigation.RemovePage((Page)pageToRemove);

			var navEventsPage2 = pageToRemove as INavigationRemoved;
			if (navEventsPage2 != null)
				navEventsPage2.NavigationRemoved();

			var navEventsPageModel2 = pageToRemove.GetPageModel() as INavigationRemoved;
			if (navEventsPageModel2 != null)
				navEventsPageModel2.NavigationRemoved();

			return Task.FromResult(true);
		}

		public async virtual Task<bool> PopPagesToRootAsync<TCurrentPageModel>(IBasePage<TCurrentPageModel> currentPage, bool clearCache = false, bool animated = true) where TCurrentPageModel : class, IBasePageModel
		{
			var navigation = ((Page)currentPage)?.Navigation;
			if (navigation == null)
				return false;

			await navigation.PopToRootAsync(animated);

			if (clearCache)
				ClearPageCache();

			return true;
		}

		public virtual Task<bool> SetNewRootAndResetAsync<TPageModel>(IBasePage<TPageModel> newRootPage, bool clearCache = true)  where TPageModel : class, IBasePageModel
		{
			if (clearCache)
				ClearPageCache();

			Application.Current.MainPage = (Page)newRootPage;

			return Task.FromResult(true);
		}

        public virtual Task<bool> SetNewRootAndResetAsync<TPageModelOfNewRoot>(bool clearCache = true) where TPageModelOfNewRoot : class, IBasePageModel
        {
            if (clearCache)
                ClearPageCache();

            Application.Current.MainPage = (Page) GetPageFromCache<TPageModelOfNewRoot>();

            return Task.FromResult(true);
        }

    }
}

