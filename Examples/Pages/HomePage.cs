using System;
using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
	public class HomePage : PFContentPage<HomeViewModel>
	{
		public HomePage()
		{
			Title = "PageFactory Example";

			var menuListView = new ListView() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,	
				RowHeight = 60,
				ItemTemplate = new DataTemplate(() => {
					var cell = new TextCell();
					cell.SetBinding<Models.MenuItem>(TextCell.TextProperty, v => v.Title);
					cell.SetBinding<Models.MenuItem>(TextCell.DetailProperty, v => v.Detail);
					cell.SetBinding<Models.MenuItem>(TextCell.CommandProperty, v => v.Command);
					cell.SetBinding<Models.MenuItem>(TextCell.CommandParameterProperty, v => v.CommandParameter);
					return cell;
				}),
			};

			menuListView.ItemSelected += (sender, e) => { menuListView.SelectedItem = null; };
			menuListView.SetBinding<HomeViewModel>(ListView.ItemsSourceProperty, v => v.MenuItems);

			Content = menuListView;
		}
	}
}


