#<img style="vertical-align:middle" src="http://res.cloudinary.com/dqeaiomo8/image/upload/v1442721091/PageFactory-logo-128_mlrygy.png" width="64"/> DLToolkit.PageFactory [![PayPayl donate button](http://img.shields.io/paypal/donate.png?color=green)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=T54TSWGPZGNDY "Donate to this project using Paypal") [![Bitcoin donate button](http://img.shields.io/bitcoin/donate.png?color=green)](https://blockchain.info/address/16CvewT3QyAc5ATTVNHQ2EomxLQPXxyKQ7 "Donate to this project using Bitcoin")

### Simple MVVM Framework for Xamarin.Forms with fluent API

```C#
PageFactory.GetPageFromCache<SecondPageModel>()
    .ResetPageModel()
    .SendActionToPageModel((model) => { model.Message = "Hello World!"; })
    .PushPage();
```
```C#
public class SecondPage : ContentPage, IBasePage<SecondPageModel> 
{
	public SecondPage()
	{
		var label = new Label();
		label.SetBinding<SecondPageModel>(Label.TextProperty, v => v.Message); 
		Content = label;
	}
}

public class SecondPageModel : BasePageModel
{	
	public string Message 
	{ 
		get { return GetField<string>(); } 
		set { SetField(value); } 
	}
}
```

## Features

- **Very Easy to use. Just mark your pages as `IBasePage<TPageModelType>`**
- **PageModel oriented Navigation**
- **Automatic wiring of BindingContext (PageModels)**
- **Simple messaging system**
- **Pages / PageModels caching** - more responsive UI experience!
- **Pure PageModels - minimal requirement is `INotifyPropertyChanged`**
- **You're not limited to any concrete implementation of Pages, PageModels**
- **Fluent style extensions methods to write less code**
- Helper classes with ready to use `INotifyPropertyChanged` implementation eg. `BasePageModel`
- Pages have override methods to respond / intercept navigation (eg. PageFactoryPushing, PageFactoryPushed, etc.) 
- Strongly typed classes / methods
- Dependency free ICommand implementation

## NuGet

- Xamarin.Forms: [https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/](https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/)
- Shared: [https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/](https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/)

## Example project

https://github.com/daniel-luberda/DLToolkit.PageFactory/tree/master/Examples

## Interface based MVVM

#### Initialization

```C#
MainPage = new XamarinFormsPageFactory().Init<HomeViewModel, PFNavigationPage>();
```

#### Minimal requirements

- Every `Page` must implement `IBasePage<TPageModel>`
- Every `PageModel` must implement `INotifyPropertyChanged`.

#### Additional features requirements

- If `PageModel` doesn't have a default parameterless constructor `Page` must implement `IPageModelInitializer` interface.
- If you want to receive messages to PageModel it must also implement `IBaseMessagable` interface
- If you want to intercept navigation, your `Page` must implement operation specific interface as `INavigationPushing`, `INavigationPushed`