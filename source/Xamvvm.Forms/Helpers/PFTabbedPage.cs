using System;
using System.ComponentModel;

namespace Xamvvm
{
    public abstract class PFTabbedPage<TPageModel> : Xamarin.Forms.TabbedPage, IBasePage<TPageModel>, INavigationInterceptors where TPageModel : class, IBasePageModel, new()
	{
        public TPageModel PageModel
        {
            get 
            {
                return BindingContext as TPageModel;
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

