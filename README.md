 [![PayPayl donate button](http://img.shields.io/paypal/donate.png?color=green)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=T54TSWGPZGNDY "Donate to this project using Paypal") [![Bitcoin donate button](http://img.shields.io/bitcoin/donate.png?color=green)](https://blockchain.info/address/16CvewT3QyAc5ATTVNHQ2EomxLQPXxyKQ7 "Donate to this project using Bitcoin")

 # **xamvvm**

### Simple, lightweight MVVM Framework for Xamarin.Forms with fluent API

## Features

- **Very Easy to use. Just mark your pages as `IBasePage<TPageModelType>`**
- **Only requirement for PageModels is that they implment INotifyPropertyChanged**
- **PageModel first oriented Navigation**
- **Automatic wiring of BindingContext (PageModels)**
- **Pages / PageModels caching** - more responsive UI experience!
- **You're not limited to any concrete implementation of Pages, PageModels**
- **Fluent style extensions methods to write less code**
- Helper classes with ready to use `INotifyPropertyChanged` implementation eg. `BasePageModel`
- Pages have override methods to respond / intercept navigation (eg. NavPushing, NavPushed, etc.) 
- Strongly typed classes / methods
- Dependency free ICommand implementation prevents multiple execution when previous execution not finished yet



## Getting Started

## Initialize the Framework

You have to create an instance of a IBaseFactory implementation and set it as the current factory to use

```C#
var factory = new XamarinFormsPageFactory(this);
XamvvmCore.SetCurrentFactory(factory);
```

That's all :-) 

The PageFactory will scan your assemblies at start up and link Pages and PageModels together according to the IBasePage definition on your Pages.
(You can also register your Pages manually if you want to. See Wiki)


## PageModel first navigation

All pushing and popping is always done from the PagewModel an not from Pages

```C#
var pageToPush = this.GetPageFromCache<DetailPageModel>();
await this.PushPageAsync(pageToPush);
```

You can pass an int action too that is executed on the Pagemodel before displaying the page

```C#
await this.PushPageAsync(pageToPush, (pm) => pm.Init("blue", Color.Blue));
```

Popping is as easy

```C#
await this.PopPageAsync();
```


All a page has to do is derive from IBasePage<PageModelType> with the PageModelType this Page should be linked to.
Matching or above example calls the used classes would like this.

```C#
public partial class DetailPage : ContentPage, IBasePage<DetailPageModel>
{
	public DetailPage()
	{
		InitializeComponent();
	}
}
```

```XML
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" x:Class="Examples.DetailPage"
		Title="Detail Page">
	<ContentPage.Content>
		<Label Text="{Binding Text}" BackgroundColor="{Binding Color}" HorizontalTextAlignment="Center" VerticalTextAlignment="Center"/>
	</ContentPage.Content>
</ContentPage>

```

```C#
public class DetailPageModel : BasePageModel
{
	public void Init(string text, Color color)
	{
		Text = text;
		Color = color;
	}

	public Color Color
	{
		get { return GetField<Color>(); }
		set { SetField(value); }
	}

	public string Text
	{
		get { return GetField<string>(); }
		set { SetField(value); }
	}
}
```
<sub>You don't have to inherit from BasePageModel it's just an included convinience class</sub>


Please look into the [Wiki](https://github.com/xamvvm/wiki) for Detailed Information

## NuGet

*Will all be updated*
- Xamarin.Forms: [https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/](https://www.nuget.org/packages/DLToolkit.PageFactory.Forms/)
- Shared: [https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/](https://www.nuget.org/packages/DLToolkit.PageFactory.Shared/)

## Example project

https://github.com/xamvvm/tree/master/Examples