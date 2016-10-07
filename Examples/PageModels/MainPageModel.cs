using DLToolkit.PageFactory;
using Xamarin.Forms;

namespace Examples.PageModels
{
	public class MainPageModel : BasePageModel
	{
		public MainPageModel()
		{
			WelcomeText = "Welcome to PageFactory!";


            OnButtonListViewDemoPage = new BaseCommand(async (_) =>
            {
                var page = PageFactory.Instance.GetPageFromCache<DemoListViewPageModel>();
                await PageFactory.Instance.PushPageAsync(this.GetPage(), page);
                //var page = this.GetPage<DemoListViewPageModel>();
                //await this.PushPageAsync<DemoListViewPageModel>(page);
            });
		}


	    public BaseCommand OnButtonListViewDemoPage;

		public string WelcomeText
		{
			get { return GetField<string>(); }
			set { SetField(value); }
		}
	}
}
