﻿using Xamvvm;
using Xamarin.Forms;

namespace Examples
{
    public class SampleMasterDetailPageModel : BasePageModel
    {
        public SampleMasterDetailPageModel()
        {
        }

        public void SetDetail<TPageModel>(IBasePage<TPageModel> page) where TPageModel: class, IBasePageModel
        {
            var masterDetailPage = this.GetCurrentPage() as MasterDetailPage;
            masterDetailPage.Detail = new NavigationPage(page as Page);
            masterDetailPage.IsPresented = false;
        }
    }
}
