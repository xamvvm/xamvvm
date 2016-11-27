# ![xamvvm](http://res.cloudinary.com/escamoteur/image/upload/c_scale,w_150/v1476723686/xamvvm2_ufjcqq.png) 
# Simple, fast and lightweight MVVM Framework for Xamarin.Forms with fluent API

## Features

- **Very Easy to use. Just mark your pages / models with empty interfaces `IBasePage<TPageModelType>` / `IBasePageModel`**
- **PageModel first oriented Navigation**
- **Automatic wiring of BindingContext (PageModels)**
- **Pages / PageModels caching** - more responsive UI experience!
- **You're not limited to any concrete implementation of Pages, PageModels**
- **Fluent style extensions methods to write less code**
- Helper classes with ready to use `INotifyPropertyChanged` implementation eg. `BasePageModel`
- Pages have override methods to respond / intercept navigation (eg. NavgationPushing, NavigationCanPush, etc.) 
- Strongly typed classes / methods / messaging
- Dependency free ICommand implementation prevents multiple execution when previous execution not finished yet



## Getting Started

## Initialize the Framework

You have to create an instance of a IBaseFactory implementation and set it as the current factory to use

```C#
var factory = new XamvvmFormsPageFactory(this);
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

// OR even shorter way:
this.PushPageFromCache<DetailPageModel>();
```

You can pass an int action too that is executed on the Pagemodel before displaying the page

```C#
await this.PushPageAsync(pageToPush, (pm) => pm.Init("blue", Color.Blue));

// OR even shorter way:
this.PushPageFromCache<DetailPageModel>((pm) => pm.Init("blue", Color.Blue));
```

Popping is as easy

```C#
await this.PopPageAsync();
```


All a page has to do is derive from IBasePage<PageModelType> with the PageModelType this Page should be linked to.For the above example calls the used classes would like this.

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
public class DetailPageModel : IBasePageModel
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


Please look into the [Wiki](https://github.com/xamvvm/xamvvm/wiki) for Detailed Information

## NuGet

- [Xamvvm.Forms](https://www.nuget.org/packages/Xamvvm.Forms/) - Xamarin.Forms
- [Xamvvm.Forms.RxUI](https://www.nuget.org/packages/Xamvvm.Forms.RxUI/) - Xamarin.Forms.RxUI
- [Xamvvm.Mock](https://www.nuget.org/packages/Xamvvm.Mock/) - Unit tests
- [Xamvvm.Core](https://www.nuget.org/packages/Xamvvm.Core/) - Platform independent

## Support
Please ask questions in [this issue](https://github.com/xamvvm/xamvvm/issues/16)
We also have a channel in the xamarin slack channel **#xamvvm** (invitation https://xamarinchat.herokuapp.com/)

## Example project

https://github.com/xamvvm/xamvvm/tree/master/examples
