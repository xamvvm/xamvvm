using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DLToolkit.PageFactory
{
	public class PFNavigationPage : PFNavigationPage<PFNavigationPage.NavigationPageViewModel>
	{
		public PFNavigationPage() : base()
		{
		}

		public PFNavigationPage(IBasePage<INotifyPropertyChanged> root) : base(root)
		{
		}

		public class NavigationPageViewModel : BasePageModel
		{
		}
	}

	public abstract class PFNavigationPage<TNavigationModel, TPageModel> : PFNavigationPage<TNavigationModel>, IBaseNavigationPage<TNavigationModel, TPageModel>
		where TNavigationModel : class, INotifyPropertyChanged
		where TPageModel : class, INotifyPropertyChanged
	{
		protected PFNavigationPage() : base(PF.Factory.GetPageFromCache<TPageModel>())
		{ 
		}

		protected PFNavigationPage(IBasePage<INotifyPropertyChanged> root) : base(root)
		{
		}

		public IBasePage<TPageModel> GetCurrentPage()
		{
			return CurrentPage as IBasePage<TPageModel>;
		}

		public IBasePage<TPageModel> GetRootPage()
		{
			return Navigation.NavigationStack.Count > 0 ? 
				Navigation.NavigationStack[0] as IBasePage<TPageModel> : null;

		}
	}

	public abstract class PFNavigationPage<TNavigationModel> : NavigationPage, IBaseNavigationPage<TNavigationModel> where TNavigationModel : class, INotifyPropertyChanged
	{
		protected PFNavigationPage() : base()
		{ 
		}

		protected PFNavigationPage(IBasePage<INotifyPropertyChanged> root) : base((Page)root)
		{
		}

        public TNavigationModel PageModel
        {
            get 
            {
                return BindingContext == null ? default(TNavigationModel) : (TNavigationModel)BindingContext;
            }
        }

        public virtual TNavigationModel PageModelInitializer()
        {
            return Activator.CreateInstance<TNavigationModel>();
        }
            
		public virtual void PageFactoryMessageReceived(string message, object sender, object arg)
		{
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

