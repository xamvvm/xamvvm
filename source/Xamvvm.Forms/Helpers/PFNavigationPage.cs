using System;
using Xamarin.Forms;

namespace Xamvvm
{
	public class PFNavigationPage : PFNavigationPage<PFNavigationPage.NavigationPageViewModel>
	{
		public PFNavigationPage() : base()
		{
		}

		public PFNavigationPage(IBasePage<IBasePageModel> root) : base(root)
		{
		}

		public class NavigationPageViewModel : BasePageModel
		{
		}
	}

	public abstract class PFNavigationPage<TNavigationModel> : NavigationPage, IBasePage<TNavigationModel>, INavigationInterceptors where TNavigationModel : class, IBasePageModel, new()
	{
		protected PFNavigationPage() : base()
		{ 
		}

		protected PFNavigationPage(IBasePage<IBasePageModel> root) : base((Page)root)
		{
		}

        public TNavigationModel PageModel
        {
            get 
            {
				return BindingContext as TNavigationModel;
            }
        }

		public virtual bool PageFactoryPushing() 
		{
			return true;
		}

		public virtual bool PageFactoryPopping()
		{
			return true;
		}

		public virtual bool PageFactoryInserting() 
		{
			return true;
		}

		public virtual bool PageFactoryRemoving()
		{
			return true;
		}

		public virtual void PageFactoryPushed() 
		{
		}

		public virtual void PageFactoryPopped()
		{
		}

		public virtual void PageFactoryInserted() 
		{
		}

		public virtual void PageFactoryRemoved()
		{
		}

		public virtual void PageFactoryRemovingFromCache()
		{
		}
	}
}

