using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	public class PFMasterDetailPage<TViewModel> : Xamarin.Forms.MasterDetailPage, IBasePage<TViewModel> where TViewModel : class, INotifyPropertyChanged, new()
	{
		public PFMasterDetailPage(bool forcedConstructor = true)
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

