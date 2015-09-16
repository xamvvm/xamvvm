using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	public class PFCarouselPage<TViewModel> : Xamarin.Forms.CarouselPage, IBasePage<TViewModel> where TViewModel : INotifyPropertyChanged
	{
		public PFCarouselPage()
		{ 
			PageFactoryResetViewModel();
		}

		public TViewModel ViewModel
		{
			get 
			{
				return BindingContext == null ? default(TViewModel) : (TViewModel)BindingContext;
			}
			set
			{
				BindingContext = value;
			}
		}

		public void PageFactoryResetViewModel()
		{
			TViewModel viewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel));
			BindingContext = viewModel;	
		}

		public void PageFactoryReplaceViewModel(object newViewModel)
		{
			if (!(newViewModel is TViewModel))
				throw new ArgumentException(string.Format("Wrong ViewModel type. Expected {0}", typeof(TViewModel).ToString()));
			
			BindingContext = newViewModel;
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

