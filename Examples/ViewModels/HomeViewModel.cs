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

			OpenPageInheritanceExampleCommand = new PageFactoryCommand(() => 
				PageFactory.GetMessagablePageFromCache<PageInheritanceViewModel>()
				.PushPage());

			OpenPageViewModelCtrExampleCommand = new PageFactoryCommand(() => 
				PageFactory.GetMessagablePageFromCache<ViewModelCtorViewModel>()
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

				new MenuItem() {
					Section = "Advanced",
					Title = "Page inheritance example",
					Command = OpenPageInheritanceExampleCommand
				},

				new MenuItem() {
					Section = "Advanced",
					Title = "ViewModel with non-default constructor example",
					Command = OpenPageViewModelCtrExampleCommand
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

		public IPageFactoryCommand OpenPageInheritanceExampleCommand { get; private set; }

		public IPageFactoryCommand OpenPageViewModelCtrExampleCommand { get; private set; }
	}
}

