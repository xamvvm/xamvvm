using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
    public abstract class PFCarouselPage<TPageModel> : Xamarin.Forms.CarouselPage, IBasePageAll<TPageModel> where TPageModel : class, INotifyPropertyChanged
	{
        public TPageModel PageModel
        {
            get 
            {
                return BindingContext == null ? default(TPageModel) : (TPageModel)BindingContext;
            }
        }

        public virtual TPageModel PageModelInitializer()
        {
            return Activator.CreateInstance<TPageModel>();
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

