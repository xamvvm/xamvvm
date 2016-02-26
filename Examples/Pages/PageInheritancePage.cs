using System;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;
using System.ComponentModel;

namespace PageFactory.Examples.Pages
{
	public class PageInheritancePage : PageInheritancePageBase<PageInheritanceViewModel>
	{
		public PageInheritancePage()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}

    public class PageInheritancePageBase<TViewModel> : ContentPage, IBasePage<TViewModel> where TViewModel : class, INotifyPropertyChanged
	{
		public PageInheritancePageBase()
		{
			BackgroundColor = Color.Blue;
			Title = "Inherited title";
		}
	}
}


