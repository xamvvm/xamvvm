# DLToolkit.PageFactory [![PayPayl donate button](http://img.shields.io/paypal/donate.png?color=green)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=PNL8H3JQ7XLP4 "Donate to this project using Paypal") [![Bitcoin donate button](http://img.shields.io/bitcoin/donate.png?color=green)](https://blockchain.info/address/16CvewT3QyAc5ATTVNHQ2EomxLQPXxyKQ7 "Donate to this project using Bitcoin")

###Simple MVVM (Model, ViewModel, View) Framework for .Net - Xamarin.Forms compatible
*Currently only implemented Factory is* ***Xamarin.Forms*** *PageFactory*

The main reason for creation of PageFactory was that I needed very simple to use library which would free me from implementing the same things for any Xamarin.Forms project all over again. Those things were eg. Page Caching, ViewModel oriented Navigation, INotifyPropertyChanged implementation, ViewModel and Page messaging, etc. I also wanted my ViewModels to be dependency free.

## Features

- **Easy to use. Just declare your views as PF[PageType] classes and have access to all features**
- **ViewModel oriented Navigation**
- **Simple ViewModel and Page messaging**
- **Page caching**
- **Pure ViewModels (only requirement is `INotifyPropertyChanged` implementation or `IBaseMessagable` when view model has to receive messages) and parameterless constructor**
- **Fluent style extensions methods to write even less code**
- Helper classes with `INotifyPropertyChanged` implementation *(Fody `INotifyPropertyChanged` compatible)*:
  - BaseModel for models (implements `INotifyPropertyChanged`)
  - BaseViewModel for view models (implements `INotifyPropertyChanged`, `IBaseViewModel` (PageFactory helpers), `IBaseMessagable` (needed for PageFactory view model messaging support)
- Every page has access to typed ViewModel instance which is automatically instantiated
- Pages have override methods to respond / intercept navigation (eg. PageFactoryPushing, PageFactoryPushed, etc.) 
- Dependency free ICommand implementation for use with ViewModels
- PCL compatible with dependency free DLToolkit.PageFactory.Shared.dll for ViewModels

## NuGet

- Dependency Free: [https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/](https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/)
- Xamarin.Forms: [https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/](https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/)

## Basic example

#### Initialization
```C#
public App()
{
	var pageFactory = new XamarinFormsPageFactory();
	var navigationPage = pageFactory.Init<HomeViewModel, PFNavigationPage>();
	MainPage = navigationPage;
}
```

#### HomeViewModel:
```C#
public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
{
	public HomeViewModel()
	{
		PageTitle = "HomePage";
		LabelText = "This is first Page";

		OpenPageCommand = new PageFactoryCommand(() => {

			PageFactory.GetMessagablePageFromCache<DetailsViewModel>()
				.ResetViewModel()
				.SendMessageToViewModel("ViewModelTestMessage", sender: this, arg: new object())
				.SendMessageToPage("PageTestMessage", sender: this, arg: new object())
				.PushPage();
		});	
	}

	string labelText;
	public string LabelText
	{
		get { return labelText; }
		set { SetField(ref labelText, value, () => LabelText); }
	}

	string pageTitle;
	public string PageTitle
	{
		get { return pageTitle; }
		set { SetField(ref pageTitle, value, () => PageTitle); }
	}

	public ICommand OpenPageCommand { get; private set; }
}
```
`BaseViewModel` inheritance is optional - `PageFactory.Factory` static class could be used instead.

#### HomePage:
```C#
public class HomePage : PFContentPage<HomeViewModel>
{
	public HomePage()
	{
		this.SetBinding<HomeViewModel>(Page.TitleProperty, v => v.PageTitle);

		var label = new Label() {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			VerticalOptions = LayoutOptions.FillAndExpand,
		};

		label.SetBinding<HomeViewModel>(Label.TextProperty, v => v.LabelText);

		var button = new Button() {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			Text = "Open Details Page"
		};
		button.SetBinding<HomeViewModel>(Button.CommandProperty, v => v.OpenPageCommand);

		Content = new StackLayout { 
			Children = {
				label,
				button
			}
		};
	}
}
```

#### DetailsViewModel:
```C#
public class DetailsViewModel : BaseViewModel
{
	public DetailsViewModel()
	{
		PageTitle = "DetailsPage";

		PopPageCommand = new PageFactoryCommand(() => {
			this.GetPage().PopPage();
		});	
	}

	string pageTitle;
	public string PageTitle
	{
		get { return pageTitle; }
		set { SetField(ref pageTitle, value, () => PageTitle); }
	}

	public ICommand PopPageCommand { get; private set; }

	public override void PageFactoryMessageReceived(string message, object sender, object arg)
	{
		Console.WriteLine("DetailsViewModel received {0} message from {1} with arg of {2} type",  
			message, sender.GetType(), arg.GetType());
	}
}
```

#### DetailsPage:
```C#
public class DetailsPage : PFContentPage<DetailsViewModel>
{
	public DetailsPage()
	{
		this.SetBinding<DetailsViewModel>(Page.TitleProperty, v => v.PageTitle);

		var button = new Button() {
			HorizontalOptions = LayoutOptions.FillAndExpand,
			Text = "Pop page"
		};
		button.SetBinding<DetailsViewModel>(Button.CommandProperty, v => v.PopPageCommand);

		Content = button;
	}

	public override void PageFactoryMessageReceived(string message, object sender, object arg)
	{
		Console.WriteLine("DetailsPage received {0} message from {1} with arg of {2} type",  
			message, sender.GetType(), arg.GetType());
	}
}
```

## Page Caching
- Cache can hold only one instance of ViewModel of the same type (with its Page)
- You can create additional Page instances but they wouldn't be cached or they would replace existsting cache entry (PageFactory [AsNew] methods with appropriate parameters)
- `GetPageFromCache`, `GetMessagablePageFromCache`, `GetPageAsNewInstance`, `GetMessagablePageAsNewInstance`, `GetPageByViewModel`, `GetMessagablePageByViewModel` - those methods return platform independed pages (`IBasePage<INotifyPropertyChanged>` or `IBasePage<IBaseMessagable>` instances) and are used as a base entry for fluid extension methods (you can also get Page from ViewModel with `.GetPage()` and `.GetMessagablePage()` extension methods).

##### Examples:
```C#
var page1 = PageFactory.GetPageAsNewInstance(saveOrReplaceInCache: true).PushPage();
var page2 = PageFactory.GetMessagablePageFromCache<HomeViewModel>().PopPage();
```
##View Models
- ViewModel must have a parameterless constructor and implement INotifyPropertyChanged (the only requirements)
- If you want to receive messages also on ViewModel it must also implement IBaseMessagable interface (wuthout it, only Page can receive messages)
- BaseViewModel class implements both interfaces
- You can get the Page from ViewModel:
  -  Static methods: `GetPageByViewModel` or `GetMessagablePageByViewModel`
  -  Extension methods `.GetPage()` and `.GetMessagablePage()`

## Messaging

#### Page

- All PageFactory Pages have messaging enabled. If you want to create custom pages just inherit from PF[PageType]. Just override PageFactoryMessageReceived method:

```C#
public override void PageFactoryMessageReceived(string message, object sender, object arg)
{
	Console.WriteLine("Message received {0} message from {1} with arg of {2} type",  
		message, sender.GetType(), arg.GetType());
}
```

#### ViewModel

- For messaging support ViewModel need to implement `IBaseMessagable` interface. In `BaseViewModel` it's already implemented. 
- You can use it by overriding PageFactoryMessageReceived method:

```C#
public override void PageFactoryMessageReceived(string message, object sender, object arg)
{
	Console.WriteLine("Message received {0} message from {1} with arg of {2} type",  
		message, sender.GetType(), arg.GetType());
}
```

#### Sending messages

- To send messages just use PageFactory static Factory methods or use fluent extensions on Page (you can also get Page from ViewModel with `.GetMessagablePage()` extension method):
  - Static methods: `SendMessageByPage`, `SendMessageByViewModel`, `SendMessageToCached`
  - Page extension methods: `SendMessageToPageAndViewModel`, `SendMessageToViewModel`, `SendMessageToPage`

##### Examples:
```C#
PageFactory.GetMessagablePageFromCache<DetailsViewModel>()
	.ResetViewModel()
	.SendMessageToViewModel("ViewModelTestMessage", sender: this, arg: new object())
	.SendMessageToPage("PageTestMessage", sender: this, arg: new object());
```

## PageFactoryCommand
- Generic PCL ICommand implementation
- *Currently it does not support Command parameters*

## Cheatsheet
More documentation coming soon...