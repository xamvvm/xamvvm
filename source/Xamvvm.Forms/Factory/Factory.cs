using System;
using Xamarin.Forms;

namespace Xamvvm
{
    public partial class XamvvmFormsFactory : IBaseFactory
    {
		IBaseLogger logger;
		public IBaseLogger Logger
		{
			get
			{
				if (logger == null)
					logger = new BaseLogger();

				return logger;
			}

			set
			{
				logger = value;
			}
		}

		public virtual IBasePage<TPageModel> GetPageByModel<TPageModel>(TPageModel pageModel) where TPageModel : class, IBasePageModel
		{
			object page = null;
			_weakPageCache.TryGetValue(pageModel, out page);
			return page as IBasePage<TPageModel>;
		}

		public virtual TPageModel GetPageModel<TPageModel>(IBasePage<TPageModel> page) where TPageModel : class, IBasePageModel
		{
			var xfPage = page as Page;
			return xfPage?.BindingContext as TPageModel;
		}

		public virtual void SetPageModel<TPageModel>(IBasePage<TPageModel> page, TPageModel newPageModel) where TPageModel : class, IBasePageModel
		{
            var formsPage = (Page)page;

            var oldVisChange = formsPage.BindingContext as IPageVisibilityChange;
            if (oldVisChange != null)
            {
                formsPage.Appearing -= FormsPage_Appearing;
                formsPage.Disappearing -= FormsPage_Disappearing;
            }

			formsPage.BindingContext = newPageModel;

            var newVisChange = newPageModel as IPageVisibilityChange;
            if (newVisChange != null)
            {
                formsPage.Appearing += FormsPage_Appearing;
                formsPage.Disappearing += FormsPage_Disappearing;
            }

			AddToWeakCacheIfNotExists(page, newPageModel);
		}

        void FormsPage_Appearing(object sender, EventArgs e)
        {
            var model = ((sender as Page).BindingContext as IPageVisibilityChange);
            if (model != null)
            {
                model.OnAppearing();
            }
        }

        void FormsPage_Disappearing(object sender, EventArgs e)
        {
            var model = ((sender as Page).BindingContext as IPageVisibilityChange);
            if (model != null)
            {
                model.OnDisappearing();
            }
        }
    }
}

