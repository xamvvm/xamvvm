#<img style="vertical-align:middle" src="http://res.cloudinary.com/dqeaiomo8/image/upload/v1442721091/PageFactory-logo-128_mlrygy.png" width="64"/> DLToolkit.PageFactory [![PayPayl donate button](http://img.shields.io/paypal/donate.png?color=green)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=T54TSWGPZGNDY "Donate to this project using Paypal") [![Bitcoin donate button](http://img.shields.io/bitcoin/donate.png?color=green)](https://blockchain.info/address/16CvewT3QyAc5ATTVNHQ2EomxLQPXxyKQ7 "Donate to this project using Bitcoin")

###Simple MVVM (Model, View, ViewModel) Framework for .Net - Xamarin.Forms compatible
*Currently only implemented Factory is* ***Xamarin.Forms*** *PageFactory*

The main reason for making PageFactory was that I needed a very simple to use MVVM library which would free me from implementing the same things for any Xamarin.Forms project I created all over again. Those things were Page Caching, ViewModel oriented Navigation, INotifyPropertyChanged and ICommand implementations, Messaging between Pages and ViewModels. I also wanted my ViewModels to be dependency free (not forcing any concrete class inheritance).

That’s it. It’s very simple, no dependency injections, no platform specific code - just plain PCL. What comes with it, it’s very very lightweight.

## Features

- **Easy to use. Just declare your views as `PF[PageType]` classes and have access to all features**
- **ViewModel oriented Navigation**
- **Simple ViewModel and Page messaging**
- **Page caching**
- **Pure ViewModels - only requirement is parameterless constructor and `INotifyPropertyChanged` or `IBaseMessagable` implementation (when view model has to receive messages)**
- **Fluent style extensions methods to write less code**
- Helper classes with `INotifyPropertyChanged` implementation *(Fody `INotifyPropertyChanged` compatible)*:
  - `BaseViewModel` for ViewModels. It implements `INotifyPropertyChanged`, `IBaseMessagable`) and has PageFactory property which returns `PF.Factory` instance.
  - `BaseModel` for Models.  It implements `INotifyPropertyChanged`.
- Every page has access to typed ViewModel instance which is automatically instantiated
- Pages have override methods to respond / intercept navigation (eg. PageFactoryPushing, PageFactoryPushed, etc.) 
- Dependency free ICommand implementation
- PCL compatible with dependency free DLToolkit.PageFactory.Shared.dll for ViewModels

## NuGet

- Dependency Free: [https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/](https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/)
- Xamarin.Forms: [https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/](https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/)

## Blog post
[http://daniel-luberda.github.io/20150922/Page-Factory-MVVM-library-for-Xamarin-Forms/](http://daniel-luberda.github.io/20150922/Page-Factory-MVVM-library-for-Xamarin-Forms/)

## Basic XAML example

![](http://res.cloudinary.com/dqeaiomo8/image/upload/c_scale,w_250/v1444343288/PageFactory/Examples/xaml_simple1.png) ![](http://res.cloudinary.com/dqeaiomo8/image/upload/c_scale,w_250/v1444343288/PageFactory/Examples/xaml_simple2.png)

#### App.cs:

    public class App : Application
    {
    	public App()
    	{
    		MainPage = new XamarinFormsPageFactory().Init<XamlFirstViewModel, PFNavigationPage>();
    	}	
    }

#### XamlFirstPage.cs:

    public partial class XamlFirstPage : PFContentPage<XamlFirstViewModel>
    {
    	public XamlFirstPage()
    	{
    		InitializeComponent();
    	}
    }

#### XamlFirstPage.xaml:

    <?xml version="1.0" encoding="UTF-8"?>
    <local:PFContentPage 
    	xmlns="http://xamarin.com/schemas/2014/forms" 
    	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    	x:Class="PageFactoryTest.Pages.XamlFirstPage"
    	xmlns:local="clr-namespace:DLToolkit.PageFactory"
    	x:TypeArguments="PageFactoryTest.ViewModels.XamlFirstViewModel">
    	<ContentPage.Content>
    		<Button Text="Open Second Page (and send message)" Command="{Binding OpenSecondPageCommand}"/>
    	</ContentPage.Content>
    </local:PFContentPage>

#### XamlFirstViewModel.cs:

    public class XamlFirstViewModel : BaseViewModel
    {
    	public XamlFirstViewModel()
    	{
    		OpenSecondPageCommand = new PageFactoryCommand(() => 
    			PageFactory.GetMessagablePageFromCache<XamlSecondViewModel>()
    				.SendMessageToViewModel("Hello", this, Guid.NewGuid())
    				.PushPage());
    	}
    
    	public IPageFactoryCommand OpenSecondPageCommand { get; private set; }
    }

#### XamlSecondPage.cs:

    public partial class XamlSecondPage : PFContentPage<XamlSecondViewModel>
    {
    	public XamlSecondPage()
    	{
    		InitializeComponent();
    	}
    }

#### XamlSecondPage.xaml:

    <?xml version="1.0" encoding="UTF-8"?>
    <local:PFContentPage 
    	xmlns="http://xamarin.com/schemas/2014/forms" 
    	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
    	x:Class="PageFactoryTest.Pages.XamlSecondPage"
    	xmlns:local="clr-namespace:DLToolkit.PageFactory"
    	x:TypeArguments="PageFactoryTest.ViewModels.XamlSecondViewModel">
    	<ContentPage.Content>
    		<Label Text="{Binding ReceivedMessage}" VerticalOptions="CenterAndExpand"/>
    	</ContentPage.Content>
    </local:PFContentPage>

#### XamlSecondViewModel.cs:

    public class XamlSecondViewModel : BaseViewModel
    {
    	public override void PageFactoryMessageReceived(string message, object sender, object arg)
    	{
    		ReceivedMessage = string.Format(
    			"Received message: {0} with arg: {1} from: {2}",
    			message, arg, sender.GetType());
    	}
    
    	public string ReceivedMessage {
    		get { return GetField<string>(); }
    		set { SetField(value); }
    	}
    }

## Basic C# only example

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
public class HomeViewModel : BaseViewModel
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
	
	public IPageFactoryCommand OpenPageCommand { get; private set; }

	public string LabelText
	{
		get { return GetField<string>(); }
		set { SetField(value); }
	}

	public string PageTitle
	{
		get { return GetField<string>(); }
		set { SetField(value); }
	}
}
```
***`BaseViewModel` inheritance is optional - `PF.Factory` static class methods could be used instead.***

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
	
	public override void PageFactoryMessageReceived(string message, object sender, object arg)
	{
		Console.WriteLine("DetailsViewModel received {0} message from {1} with arg of {2} type",  
			message, sender.GetType(), arg.GetType());
	}	
	
	public IPageFactoryCommand PopPageCommand { get; private set; }
	
	public string PageTitle
	{
		get { return GetField<string>(); }
		set { SetField(value); }
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
**That's all! You'll have access to all PageFactory features!**

## PageFactory Basics

### Pages and ViewModels

`PageFactory` uses `Pages` and `ViewModels`. 

- Every `Page` implements `IBasePage<INotifyPropertyChanged>`, the generic argument is a `ViewModel` type
- Every `ViewModel` must have a parameterless constructor and implement `INotifyPropertyChanged` (if only `Page` has to receive messages) or `IBaseMessagable` (if both `Page` and `ViewModel` have to receive messages). There are no other requirements.

### PageFactory Factory instance

You can get PageFactory instance:

- using **`PageFactory`** property in `Page` or `ViewModel` (if it inherits from `BaseViewModel`)
- using static class property: **`PF.Factory`**

Some basic `PageFactory` methods you should know: 

- **`GetPageFromCache<TViewModel>()`** - Gets (or creates) cached Page instance.
- **`GetMessagablePageFromCache<TViewModel>()`** - Gets (or creates) cached Page instance with ViewModel messaging support.
- **`GetPageAsNewInstance<TViewModel>()`** - Creates a new Page Instance. New instance can replace existing cached Page instance (`bool saveOrReplaceInCache` argument).
- **`GetMessagablePageAsNewInstance<TViewModel>()`** - Creates a new Page Instance with ViewModel messaging support. New instance can replace existing cached Page instance (`bool saveOrReplaceInCache = false` method parameter).

Cache can hold only one instance of ViewModel of the same type (with its Page). You can remove cache for a ViewModel type or replace it with another instance.

#### Example:
```C#
var page = PageFactory.GetPageAsNewInstance<HomeViewModel>(saveOrReplaceInCache: true);
var theSamePageFromCache = PageFactory.GetMessagablePageFromCache<HomeViewModel>();
```
### ViewModels
- ViewModel must have a parameterless constructor and implement `INotifyPropertyChanged` or `IBaseMessagable`
- If you want to receive messages also on ViewModel it must also implement `IBaseMessagable` interface (without it, only Page can receive messages)
- `BaseViewModel` class implements both interfaces
- You can get the Page from ViewModel:
  -  Static methods: `GetPageByViewModel` or `GetMessagablePageByViewModel`
  -  Extension methods `.GetPage()` and `.GetMessagablePage()`
- All pages have access to their ViewModels through `ViewModel` property 

### Messaging

All PageFactory `Pages` have messaging enabled. If you also want `ViewModel` to receive messages it must implement `IBaseMessagable` interface (`BaseViewModel` does it). 

#### Receiving messages

To receive messages just override `PageFactoryMessageReceived` method (either on Page or ViewModel):

```C#
public override void PageFactoryMessageReceived(string message, object sender, object arg)
{
  Console.WriteLine("HomeViewModel received {0} message from {1} with arg = {2}",  
	  message, sender == null ? "null" : sender.GetType().ToString(), arg ?? "null");
}
```

#### Sending messages

To send messages use:

- PageFactory static methods: `SendMessageByPage`, `SendMessageByViewModel`, `SendMessageToCached`
- Page extension methods: `SendMessageToPageAndViewModel`, `SendMessageToViewModel`, `SendMessageToPage`

```C#
PageFactory.GetMessagablePageAsNewInstance<HomeViewModel>()
  .SendMessageToPageAndViewModel("Message", this, "arg")
  
PageFactory.GetPageFromCache<DetailsViewModel>()
	.SendMessageToPage("Message", this, "arg");
```

```C#
PageFactory.SendMessageToCached<HomeViewModel>(
	MessageConsumer.PageAndViewModel, "Message", this, "arg");

var page = PageFactory.GetPageFromCache<DetailsViewModel>();
PageFactory.SendMessageToPage(Pages, "Message", this, "arg");
```

### Navigation

#### Navigating

To navigate use this methods:

- PageFactory static methods: `PushPageAsync`, `PopPageAsync`, `InsertPageBefore`, `RemovePage`, `PopPagesToRootAsync`, `SetNewRootAndReset`
- Page extension methods: `PushPage`, `PopPage`, `InsertPageBefore`, `RemovePage`, `PopPagesToRoot`, `SetNewRootAndReset`

```C#
var page = PageFactory.GetPageAsNewInstance<DetailsPage>().PushPage();
await Task.Delay(5000);
page.PopPage();
```

```C#
var page = PageFactory.GetPageAsNewInstance<DetailsPage>();
await PageFactory.PushPageAsync(page);
await Task.Delay(5000);
await PageFactory.PopPageAsync();
```

#### Navigation interception

You can intercept navigation. Just override one of this `Page` methods:

- `PageFactoryPushed`, `PageFactoryPopped`, `PageFactoryRemoved`, `PageFactoryInserted` - called after successful navigation
- `PageFactoryPushing`, `PageFactoryPopping`, `PageFactoryRemoving`, `PageFactoryInserting` - called before navigation. If `false` is returned, navigation will be cancelled
- `PageFactoryRemovingFromCache` - called when the page is being removed from cache

```C#
public override void PageFactoryPopped()
{
	// Removes Page instance from PageFactory cache (it will be no longer needed)
	this.RemovePageInstanceFromCache();
}
```

```C#
public override bool PageFactoryPushing()
{
	// Page cannot be pushed if SomeCheckIsValid condition isn't met!!!
	if (!SomeCheckIsValid)
		return false;

	return true;
}
```

### PageFactoryCommand, IPageFactoryCommand
- Generic PCL ICommand implementation
- Supports generic command parameters