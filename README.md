#<img style="vertical-align:middle" src="http://res.cloudinary.com/dqeaiomo8/image/upload/v1442721091/PageFactory-logo-128_mlrygy.png" width="64"/> DLToolkit.PageFactory [![PayPayl donate button](http://img.shields.io/paypal/donate.png?color=green)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=T54TSWGPZGNDY "Donate to this project using Paypal") [![Bitcoin donate button](http://img.shields.io/bitcoin/donate.png?color=green)](https://blockchain.info/address/16CvewT3QyAc5ATTVNHQ2EomxLQPXxyKQ7 "Donate to this project using Bitcoin")

###Simple MVVM (Model, View, ViewModel) Framework for .Net

*Currently only implemented IPageFactory is* ***Xamarin.Forms*** *PageFactory*

The main reason for making PageFactory was that I needed a simple to use MVVM library which would free me from implementing the same things for any Xamarin.Forms projects I created all over again.

```C#
PageFactory.GetMessagablePageFromCache<SimpleSecondViewModel>()
    .ResetViewModel()
    .SendMessageToViewModel("ViewModelTestMessage", sender: this, arg: Guid.NewGuid())
    .SendMessageToPage("PageTestMessage", sender: this, arg: Guid.NewGuid())
    .PushPage();
```

## Features

- **Easy to use. Just declare your pages as `PF[PageType]` classes and have access to all features**
- **ViewModel oriented Navigation**
- **Simple ViewModel and Page messaging** 
- **Pages / ViewModels caching** - more responsive UI experience
- **Pure ViewModels - only requirement is `INotifyPropertyChanged` or `IBaseMessagable` implementation (when view model has to receive messages)**
- **Fluent style extensions methods to write less code**
- Helper classes with `INotifyPropertyChanged` implementation *(Fody `INotifyPropertyChanged` compatible)*:
  - `BaseViewModel` for ViewModels. It implements `INotifyPropertyChanged`, `IBaseMessagable`) and has `PageFactory` property which is an alias to `PF.Factory` static property.
  - `BaseModel` for Models.  It implements `INotifyPropertyChanged`.
- Every page has access to typed ViewModel instance which is automatically instantiated
- Pages have override methods to respond / intercept navigation (eg. PageFactoryPushing, PageFactoryPushed, etc.) 
- Not limited to any concrete implementation of Pages, ViewModels or PageFactory
- Strongly typed classes / methods
- Dependency free ICommand implementation
- PCL compatible with dependency free DLToolkit.PageFactory.Shared.dll for ViewModels

## NuGet

- Xamarin.Forms: [https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/](https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/)
- Shared: [https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/](https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/)

## Example project

https://github.com/daniel-luberda/DLToolkit.PageFactory/tree/master/Examples

## Basic XAML example

https://github.com/daniel-luberda/DLToolkit.PageFactory/wiki/Simple-Xaml-Example

## Basic C# only example

https://github.com/daniel-luberda/DLToolkit.PageFactory/wiki/Simple-C%23-Example

**That's all! You'll have access to all PageFactory features!**

## Blog post
[http://daniel-luberda.github.io/20150922/Page-Factory-MVVM-library-for-Xamarin-Forms/](http://daniel-luberda.github.io/20150922/Page-Factory-MVVM-library-for-Xamarin-Forms/)

## PageFactory Basics

### Pages and ViewModels

`PageFactory` uses `Pages` and `ViewModels`. 

- Every `Page` implements `IBasePage<INotifyPropertyChanged>`, the generic argument is a `ViewModel` type
- Every `ViewModel` must implement `INotifyPropertyChanged` (if only `Page` has to receive messages) or `IBaseMessagable` (if both `Page` and `ViewModel` have to receive messages). 
- If ViewModel doesn't have a default parameterless constructor Page has to override ViewModelInitializer method. It has to return a new instance of ViewModel (it's used for Page ViewModel initialization)

There are no other requirements.

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
- ViewModel must implement `INotifyPropertyChanged` or `IBaseMessagable`
- If you want to receive messages also on ViewModel it must also implement `IBaseMessagable` interface (without it, only Page can receive messages)
- `BaseViewModel` class implements both interfaces
- You can get the Page from ViewModel:
  -  Static methods: `GetPageByViewModel` or `GetMessagablePageByViewModel`
  -  Extension methods `.GetPage()` and `.GetMessagablePage()`
- All pages have access to their ViewModels through `ViewModel` property 
- If ViewModel doesn't have a default parameterless constructor Page has to override ViewModelInitializer method. It has to return a new instance of ViewModel (it's used for Page ViewModel initialization)

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