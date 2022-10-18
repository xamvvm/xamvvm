using Xamvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace Examples
{
    public class SampleMasterDetailMenuPageModel : BasePageModel
    {
        public SampleMasterDetailMenuPageModel()
        {
            Page1Command = new BaseCommand((arg) =>
            {
                var page = this.GetPageFromCache<DemoListViewPageModel>();

                var masterDetailPage = this.GetPageFromCache<SampleMasterDetailPageModel>();
                masterDetailPage.GetPageModel().SetDetail(page);
            });

            Page2Command = new BaseCommand((arg) =>
            {
                var page = this.GetPageFromCache<DetailPageModel>();
                page.GetPageModel().Init("detail!", Color.Red);

                var masterDetailPage = this.GetPageFromCache<SampleMasterDetailPageModel>();
                masterDetailPage.GetPageModel().SetDetail(page);
            });
        }

        public ICommand Page1Command { get; set; }

        public ICommand Page2Command { get; set; }
    }
}
