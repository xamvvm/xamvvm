using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	public class PFPage<TViewModel> : Xamarin.Forms.Page, IBasePage<TViewModel> where TViewModel : class, INotifyPropertyChanged, new()
	{
		public PFPage(bool forcedConstructor = true)
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

		public virtual void PageFactoryRemovedFromNavigation()
		{
		}

		public virtual void PageFactoryAddedToNavigation()
		{
		}

		public virtual void PageFactoryRemovingFromCache()
		{
		}
	}		
}

