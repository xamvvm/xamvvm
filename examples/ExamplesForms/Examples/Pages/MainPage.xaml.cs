using Xamarin.Forms;
using Xamvvm;

namespace Examples
{
	public partial class MainPage : ContentPage, IBasePage<MainPageModel>
	{
		public MainPage()
		{
			InitializeComponent();
		}
	}
}
