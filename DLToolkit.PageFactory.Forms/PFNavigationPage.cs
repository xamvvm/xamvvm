using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	public class PFNavigationPage : NavigationPage<PFNavigationPage.NavigationPageViewModel>
	{
		public PFNavigationPage() : base(true)
		{
		}

		public PFNavigationPage(Xamarin.Forms.Page root, bool forcedConstructor = true) : base(root, forcedConstructor)
		{
		}

		public class NavigationPageViewModel : BaseViewModel
		{
		}
	}

	public class NavigationPage<TViewModel> : Xamarin.Forms.NavigationPage, IBasePage<TViewModel> where TViewModel : class, INotifyPropertyChanged, new()
	{
		public NavigationPage(bool forcedConstructor = true) : base()
		{ 
			PageFactory.ReplacePageViewModel(this, new TViewModel());
		}

		public NavigationPage(Xamarin.Forms.Page root, bool forcedConstructor = true) : base(root)
		{
			PageFactory.ReplacePageViewModel(this, new TViewModel());
		}

		public TViewModel ViewModel
		{
			get 
			{
				return BindingContext == null ? default(TViewModel) : (TViewModel)BindingContext;
			}
		}

		public IPageFactory PageFactory
		{
			get
			{
				return DLToolkit.PageFactory.PageFactory.Factory;
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

