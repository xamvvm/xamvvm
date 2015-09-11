# DLToolkit.PageFactory *(alpha)*

####Simple Model / ViewModel / Page / Navigation toolkit (MVVM). *Currently only implemented PageFactory is* ***Xamarin.Forms*** *PageFactory*

The main reason for creation of PageFactory was that I needed very simple library which would free me from implementing the same things for any Xamarin.Forms project I created all over again. Those things were eg. Page Caching, ViewModel oriented Navigation, INotifyPropertyChanged implementation, ViewModel->Page messaging, etc. I also wanted my ViewModels to be as clean as possible (Xamarin.Forms dependency free).

## Features

- Easy to use (Just declare your Pages as PF*[PageType]* classes) and have access to all features
- Pure ViewModels (only requirement `INotifyPropertyChanged` implementation)
- Base `INotifyPropertyChanged` implementation helper classes (also Fody `INotifyPropertyChanged` compatibile)
- Every page has access to typed ViewModel instance which is auto-initialized
- ViewModel oriented Navigation
- Pages have override methods to respond / intercept navigation (eg. PageFactoryPushing, PageFactoryPushed, etc.) 
- Dependency free Command replacement for use with ViewModels
- Page / ViewModel caching
- Simple ViewModel -> Page messaging
- Just two PCL dll's (or one when it comes to ViewModels which is dependency free).

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

#### ViewModel
```C#
public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
{
	public HomeViewModel()
	{
		PageTitle = "HomePage";
		LabelText = "This is first Page";

		OpenPageCommand = new PageFactoryCommand(() => 
			PageFactory.PushPageFromCacheAsync<DetailsViewModel>());
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
You don't need to inherit from `BaseViewModel` (only requirement is `INotifyPropertyChanged` implementation) but `BaseViewModel` does it for you and contains some helpers. If you don't use it just use `PageFactory.Factory` static class.

#### Page
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

## Page Caching
- Cache can hold only one instance of Page with ViewModel of the same type - they wouldn't be put into cache or they would replace existsting cache entry (by using methods parameters)
- You can bypass cache ("AsNew" methods) and force new instances.

## Messaging

#### ViewModel
```C#
PageFactory.SendMessageToPage(this, "ExampleMessage", new TestObject());
```

#### Page
```C#
public override void PageFactoryMessageReceived(string message, object arg)
{
	Console.WriteLine("Message received: {0} with arg of {1} type", message, arg.GetType().ToString());
}
```

You can also send messages to other pages by using its ViewModel signature, etc

## Cheatsheet
More documentation coming soon...

#### PageFactory methods
```C#
public interface IPageFactory
{
	IBasePage<INotifyPropertyChanged> GetPageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged;

	IBasePage<INotifyPropertyChanged> GetPageFromCache(Type viewModelType, bool resetViewModel = false);

	IBasePage<INotifyPropertyChanged> GetPageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged;

	IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false);
	
	IBasePage<INotifyPropertyChanged> GetPageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, INotifyPropertyChanged;


	bool ReplaceCachedPageViewModel<TViewModel>(TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged;

	bool ResetCachedPageViewModel<TViewModel>() where TViewModel : class, INotifyPropertyChanged;

	bool RemovePageFromCache(Type viewModelType);

	bool RemovePageFromCache<TViewModel>() where TViewModel : class, INotifyPropertyChanged;

	void ClearCache();


	bool SendMessageToPage<TViewModel>(TViewModel viewModelInstance, string message, object arg = null) where TViewModel : class, INotifyPropertyChanged;

	bool SendMessageToCachedPage<TViewModel>(string message, object arg = null, bool createPageIfNotExists = true) where TViewModel : class, INotifyPropertyChanged;


	Task<bool> PushPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true);

	Task<bool> PushModalPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true);


	Task<bool> PushPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	Task<bool> PushPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	Task<bool> PushModalPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	Task<bool> PushModalPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged;


	Task<bool> PushPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	Task<bool> PushPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	Task<bool> PushModalPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	Task<bool> PushModalPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;


	bool InsertPageBefore(IBasePage<INotifyPropertyChanged> page, IBasePage<INotifyPropertyChanged> before);


	bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

	bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

	bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

	bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;


	Task<bool> PopPageAsync(bool removeFromCache = false, bool resetViewModel = false, bool animated = true);

	Task<bool> PopModalPageAsync(bool removeFromCache = false, bool resetViewModel = false, bool animated = true);


	bool RemovePage(IBasePage<INotifyPropertyChanged> pageToRemove);

	bool RemoveCachedPage<TViewModel>(bool removeFromCache = false, bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged;


	Task PopPagesToRootAsync(bool clearCache = false, bool animated = true);

	void SetNewRootAndReset<TViewModel>() where TViewModel : class, INotifyPropertyChanged;
}
```
#### Xamarin.Forms Page class extensions
```C#
public interface IBasePage<out TViewModel> : IBasePage where TViewModel: INotifyPropertyChanged
{
	TViewModel ViewModel { get; }

	void ResetViewModel();

	void ReplaceViewModel(object newViewModel);
}	

public interface IBasePage
{
	IPageFactory PageFactory { get; }

	void PageFactoryMessageReceived(string message, object arg);

	void PageFactoryRemovingFromCache();

	bool PageFactoryPushing();

	bool PageFactoryPopping();

	bool PageFactoryRemoving();

	bool PageFactoryInserting();

	void PageFactoryPushed();

	void PageFactoryPopped();

	void PageFactoryRemoved();

	void PageFactoryInserted();
}

public interface IBaseViewModel
{
	IPageFactory PageFactory { get; }
}
```