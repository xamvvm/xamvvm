using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DLToolkit.PageFactory
{
	public class PFNavigationPage : PFNavigationPage<PFNavigationPage.NavigationPageViewModel>
	{
		public PFNavigationPage() : base(true)
		{
		}

		public PFNavigationPage(IBasePage<INotifyPropertyChanged> root, bool forcedConstructor = true) : base(root, forcedConstructor)
		{
		}

		public class NavigationPageViewModel : BaseViewModel
		{
		}
	}

	public abstract class PFNavigationPage<TNavigationViewModel, TPageViewModel> : PFNavigationPage<TNavigationViewModel>, IBaseNavigationPage<TNavigationViewModel, TPageViewModel>
		where TNavigationViewModel : class, INotifyPropertyChanged
		where TPageViewModel : class, INotifyPropertyChanged
	{
		protected PFNavigationPage(bool forcedConstructor = true) : base(PF.Factory.GetPageFromCache<TPageViewModel>(), forcedConstructor)
		{ 
		}

		protected PFNavigationPage(IBasePage<INotifyPropertyChanged> root, bool forcedConstructor = true) : base(root, forcedConstructor)
		{
		}

		public IBasePage<TPageViewModel> GetCurrentPage()
		{
			return CurrentPage as IBasePage<TPageViewModel>;
		}

		public IBasePage<TPageViewModel> GetRootPage()
		{
			return Navigation.NavigationStack[0] as IBasePage<TPageViewModel>;
		}
	}

	public abstract class PFNavigationPage<TNavigationViewModel> : NavigationPage, IBaseNavigationPage<TNavigationViewModel> where TNavigationViewModel : class, INotifyPropertyChanged
	{
		protected PFNavigationPage(bool forcedConstructor = true) : base()
		{ 
			PageFactory.ResetPageViewModel(this);
		}

		protected PFNavigationPage(IBasePage<INotifyPropertyChanged> root, bool forcedConstructor = true) : base((Page)root)
		{
			PageFactory.ResetPageViewModel(this);
		}

		public TNavigationViewModel ViewModel
		{
			get 
			{
				return BindingContext == null ? default(TNavigationViewModel) : (TNavigationViewModel)BindingContext;
			}
		}

		public virtual TNavigationViewModel ViewModelInitializer()
		{
			return Activator.CreateInstance<TNavigationViewModel>();
		}

		public IPageFactory PageFactory
		{
			get
			{
				return PF.Factory;
			}
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

	[Obsolete("Use PFNavigationPage instead")]
	public abstract class NavigationPage<TNavigationViewModel> : NavigationPage, IBaseNavigationPage<TNavigationViewModel> where TNavigationViewModel : class, INotifyPropertyChanged
	{
		protected NavigationPage(bool forcedConstructor = true) : base()
		{ 
			PageFactory.ResetPageViewModel(this);
		}

		protected NavigationPage(Page root, bool forcedConstructor = true) : base(root)
		{
			PageFactory.ResetPageViewModel(this);
		}

		public TNavigationViewModel ViewModel
		{
			get 
			{
				return BindingContext == null ? default(TNavigationViewModel) : (TNavigationViewModel)BindingContext;
			}
		}

		public virtual TNavigationViewModel ViewModelInitializer()
		{
			return Activator.CreateInstance<TNavigationViewModel>();
		}

		public IPageFactory PageFactory
		{
			get
			{
				return PF.Factory;
			}
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

