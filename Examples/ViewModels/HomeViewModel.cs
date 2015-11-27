using System;
using DLToolkit.PageFactory;
using System.Collections.ObjectModel;
using PageFactory.Examples.Models;

namespace PageFactory.Examples.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel()
		{
			OpenSimpleExampleCommand = new PageFactoryCommand(() => 
				PageFactory.GetMessagablePageFromCache<SimpleFirstViewModel>()
				.PushPage());

			OpenSimpleXamlExampleCommand = new PageFactoryCommand(() => 
				PageFactory.GetMessagablePageFromCache<SimpleXamlFirstViewModel>()
				.PushPage());

			MenuItems = new ObservableCollection<MenuItem>() {

				new MenuItem() {
					Section = "Basic",
					Title = "Basic example",
					Command = OpenSimpleExampleCommand
				},

				new MenuItem() {
					Section = "Basic",
					Title = "Basic Xaml example",
					Command = OpenSimpleXamlExampleCommand
				},

			};
		}

		public ObservableCollection<MenuItem> MenuItems
		{
			get { return GetField<ObservableCollection<MenuItem>>(); }
			set { SetField(value); }
		}

		public IPageFactoryCommand OpenSimpleExampleCommand { get; private set; }

		public IPageFactoryCommand OpenSimpleXamlExampleCommand { get; private set; }
	}
}

