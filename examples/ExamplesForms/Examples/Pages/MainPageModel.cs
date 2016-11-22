using System;
using Xamvvm;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Examples
{
	public class MainPageModel : BasePageModel
	{
		public MainPageModel()
		{
			WelcomeText = "Welcome to xamvvm!";

			DetailButtonCommand = BaseCommand.FromTask<string>((param) => DetailButtonCommandExecute(param));
			DemoListButtonCommand = BaseCommand.FromTask((param) => this.PushPageFromCacheAsync<DemoListViewPageModel>());
		}

		async Task DetailButtonCommandExecute(string param)
		{
			var pageToPush = this.GetPageFromCache<DetailPageModel>();

			if (param == "red")
			{
				await this.PushPageAsync(pageToPush, (v) => v.Init("red", Color.Red));
			}
			else if (param == "green")
			{
				await this.PushPageAsync(pageToPush, (v) => v.Init("green", Color.Green));
			}
			else if (param == "blue")
			{
				await this.PushPageAsync(pageToPush, (v) => v.Init("blue", Color.Blue));
			}
		}

		public string WelcomeText
		{
			get { return GetField<string>(); }
			set { SetField(value); }
		}

		public ICommand DetailButtonCommand
		{
			get { return GetField<ICommand>(); }
			set { SetField(value); }
		}

		public ICommand DemoListButtonCommand
		{
			get { return GetField<ICommand>(); }
			set { SetField(value); }
		}
	}
}
