using Xamarin.Forms;
using Xamvvm;

namespace Examples
{
    public class SampleMasterDetailPage : MasterDetailPage, IBasePage<SampleMasterDetailPageModel>
    {
        public SampleMasterDetailPage()
        {
            Master = this.GetPageFromCache<SampleMasterDetailMenuPageModel>() as Page;
            Detail = new NavigationPage(this.GetPageFromCache<DetailPageModel>() as Page);
        }
    }
}

