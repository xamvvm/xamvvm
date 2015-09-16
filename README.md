# DLToolkit.PageFactory *(alpha)*

###Simple MVVM (Model, ViewModel, View) Framework for .Net - Xamarin.Forms compatibile
*Currently only implemented Factory is* ***Xamarin.Forms*** *PageFactory*

The main reason for creation of PageFactory was that I needed very simple to use library which would free me from implementing the same things for any Xamarin.Forms project I created all over again. Those things were eg. Page Caching, ViewModel oriented Navigation, INotifyPropertyChanged implementation, ViewModel and Page messaging, etc. I also wanted my ViewModels to be as clean as possible (dependency free).

## Features

- Easy to use. Just declare your views as PF[PageType] classes and have access to all features
- Pure ViewModels (only requirement is `INotifyPropertyChanged` implementation)
- ViewModel oriented Navigation
- Simple ViewModel and Page messaging
- Page caching
- Helper classes with `INotifyPropertyChanged` implementation *(Fody `INotifyPropertyChanged` compatibile)*:
  - BaseModel for models (implements `INotifyPropertyChanged`)
  - BaseViewModel for view models (implements `INotifyPropertyChanged`, `IBaseViewModel` (PageFactory helpers), `IBaseMessagable` (needed for PageFactory view model messaging support)
- Fluent style extensions to write even less code when using PageFactory
- Every page has access to typed ViewModel instance which is automatically instantiated
- Pages have override methods to respond / intercept navigation (eg. PageFactoryPushing, PageFactoryPushed, etc.) 
- Dependency free ICommand implementation for use with ViewModels
- PCL compatibile with dependency free DLToolkit.PageFactory.Shared.dll

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
`BaseViewModel` inheritance is optional (only requirement is `INotifyPropertyChanged` implementation) `PageFactory.Factory` static class could be used instead.

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
- **All instances of pages must be created by using PageFactory methods.** (without it Messaging won't work)
- Cache can hold only one instance of ViewModel of the same type with its Page
- You can create additional Page instances but they wouldn't be cached or they would replace existsting cache entry (PageFactory [AsNew] methods with appropriate parameters)

## Messaging

#### Page

- All PageFactory PF[PageType] pages have messaging enabled. If you want to create custom pages just inherit from PF[PageType]. Just override PageFactoryMessageReceived method:

```C#
public override void PageFactoryMessageReceived(string message, object sender, object arg)
{
	Console.WriteLine("Message received {0} message from {1} with arg of {2} type",  
		message, sender.GetType(), arg.GetType());
}
```
- To send messages just use PageFactory static Factory methods or use fluent extensions. 

#### ViewModel

- For messaging support ViewModel need to implement `IBaseMessagable` interface. `BaseViewModel` has it. 
- You can use it by overriding PageFactoryMessageReceived method:

```C#
public override void PageFactoryMessageReceived(string message, object sender, object arg)
{
	Console.WriteLine("Message received {0} message from {1} with arg of {2} type",  
		message, sender.GetType(), arg.GetType());
}
```

- To send messages just use PageFactory static Factory methods or use fluent extensions.

## Cheatsheet
More documentation coming soon...

#### PageFactory extensions
```C#
/// <summary>
/// PageFactory Extensions helpers
/// </summary>
public static class Extensions
{
	/// <summary>
	/// Pushes the page into navigation stack.
	/// </summary>
	/// <param name="page">Page to push.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<INotifyPropertyChanged> PushPage(this IBasePage<INotifyPropertyChanged> page, bool animated = true) {}

	/// <summary>
	/// Pushes the messagable page into navigation stack.
	/// </summary>
	/// <param name="page">Page to push.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<IBaseMessagable> PushPage(this IBasePage<IBaseMessagable> page, bool animated = true) {}

	/// <summary>
	/// Pushes the page as modal.
	/// </summary>
	/// <param name="page">Page to push.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<INotifyPropertyChanged> PushModalPage(this  IBasePage<INotifyPropertyChanged> page, bool animated = true) {}

	/// <summary>
	/// Pushes the messagable page as modal.
	/// </summary>
	/// <param name="page">Page to push.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<IBaseMessagable> PushModalPage(this  IBasePage<IBaseMessagable> page, bool animated = true) {}

	/// <summary>
	/// Pops the page from navigation stack.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<INotifyPropertyChanged> PopPage(this  IBasePage<INotifyPropertyChanged> page, bool animated = true) {}

	/// <summary>
	/// Pops the messagable page from navigation stack.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<IBaseMessagable> PopPage(this  IBasePage<IBaseMessagable> page, bool animated = true) {}

	/// <summary>
	/// Pops the modal page.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<INotifyPropertyChanged> PopModalPage(this  IBasePage<INotifyPropertyChanged> page, bool animated = true) {}

	/// <summary>
	/// Pops the messagable modal page.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	public static IBasePage<IBaseMessagable> PopModalPage(this  IBasePage<IBaseMessagable> page, bool animated = true) {}

	/// <summary>
	/// Sends the message to page and view model.
	/// </summary>
	/// <returns>The message.</returns>
	/// <param name="page">Page.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	/// <param name="sender">Sender.</param>
	public static IBasePage<IBaseMessagable> SendMessageToPageAndViewModel(this IBasePage<IBaseMessagable> page, string message, object sender = null, object arg = null) {}

	/// <summary>
	/// Sends the message to view model.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name = "sender">Sender.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	public static IBasePage<IBaseMessagable> SendMessageToViewModel(this IBasePage<IBaseMessagable> page, string message, object sender = null, object arg = null) {}

	/// <summary>
	/// Sends the message to page.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name = "sender">Sender.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	public static IBasePage<IBasePage> SendMessageToPage(this IBasePage<IBasePage> page, string message, object sender = null, object arg = null) {}

	/// <summary>
	/// Sends the message to messagable page.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name = "sender">Sender.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	public static IBasePage<IBaseMessagable> SendMessageToPage(this IBasePage<IBaseMessagable> page, string message, object sender = null, object arg = null) {}

	/// <summary>
	/// Replaces page view model.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name="newViewModel">New view model.</param>
	public static IBasePage<INotifyPropertyChanged> ReplaceViewModel<TViewModel>(this IBasePage<INotifyPropertyChanged> page, TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged {}

	/// <summary>
	/// Replaces page view model.
	/// </summary>
	/// <param name = "page">Page.</param>
	/// <param name="newViewModel">New view model.</param>
	public static IBasePage<IBaseMessagable> ReplaceViewModel<TViewModel>(this IBasePage<IBaseMessagable> page, TViewModel newViewModel) where TViewModel : class, IBaseMessagable {}

	/// <summary>
	/// Resets page view model.
	/// </summary>
	/// <param name = "page">Page.</param>
	public static IBasePage<INotifyPropertyChanged> ResetViewModel(this IBasePage<INotifyPropertyChanged> page) {}

	/// <summary>
	/// Resets page view model.
	/// </summary>
	/// <param name = "page">Page.</param>
	public static IBasePage<IBaseMessagable> ResetViewModel(this IBasePage<IBaseMessagable> page) {}

	/// <summary>
	/// Gets the page by view model.
	/// </summary>
	/// <returns>The page.</returns>
	/// <param name="viewModel">View model.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	public static IBasePage<INotifyPropertyChanged> GetPage<TViewModel>(this TViewModel viewModel) where TViewModel : class, INotifyPropertyChanged {}

	/// <summary>
	/// Gets the messagable page by view model.
	/// </summary>
	/// <returns>The page.</returns>
	/// <param name="viewModel">View model.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	public static IBasePage<IBaseMessagable> GetMessagablePage<TViewModel>(this TViewModel viewModel) where TViewModel : class, IBaseMessagable {}
}
```

#### PageFactory interfaces
```C#
/// <summary>
/// DLToolkit.PageFactory
/// </summary>
public interface IPageFactory : IPageFactoryNavigation, IPageFactoryMessaging
{
}

/// <summary>
/// IPageFactory messaging.
/// </summary>
public interface IPageFactoryMessaging
{
	/// <summary>
	/// Gets the page from cache. Creates a new page instances if not exists.
	/// </summary>
	/// <returns>The page from cache.</returns>
	/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	IBasePage<IBaseMessagable> GetMessagablePageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged;

	/// <summary>
	/// Gets the page from cache. Creates a new page instances if not exists.
	/// </summary>
	/// <returns>The page from cache.</returns>
	/// <param name="viewModelType">View model type.</param>
	/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
	IBasePage<IBaseMessagable> GetMessagablePageFromCache(Type viewModelType, bool resetViewModel = false);

	/// <summary>
	/// Gets the page as new instance.
	/// </summary>
	/// <returns>The page as new instance.</returns>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	IBasePage<IBaseMessagable> GetMessagablePageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged;

	/// <summary>
	/// Gets the page as new instance.
	/// </summary>
	/// <returns>The page as new instance.</returns>
	/// <param name="viewModelType">View model type.</param>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
	IBasePage<IBaseMessagable> GetMessagablePageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false);

	/// <summary>
	/// Gets the page by view model.
	/// </summary>
	/// <returns>The page by view model.</returns>
	/// <param name="viewModelInstance">View model instance.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	IBasePage<IBaseMessagable> GetMessagablePageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, IBaseMessagable, INotifyPropertyChanged;

	/// <summary>
	/// Sends the message to page only.
	/// </summary>
	/// <returns><c>true</c>, if message to view model was received, <c>false</c> otherwise.</returns>
	/// <param name = "sender">Sender.</param>
	/// <param name="page">Page instance.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	/// <typeparam name="TPage">Page type.</typeparam>
	bool SendMessageByPage<TPage>(TPage page, string message, object sender = null, object arg = null) where TPage : class, IBasePage<INotifyPropertyChanged>;

	/// <summary>
	/// Sends the message to page.
	/// </summary>
	/// <returns><c>true</c>, if message to view model was received, <c>false</c> otherwise.</returns>
	/// <param name = "consumer">Message consumer.</param>
	/// <param name = "sender">Sender.</param>
	/// <param name="page">Page instance.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	/// <typeparam name="TPage">Page type.</typeparam>
	bool SendMessageByPage<TPage>(MessageConsumer consumer, TPage page, string message, object sender = null, object arg = null) where TPage : class, IBasePage<IBaseMessagable>;

	/// <summary>
	/// Sends the message to page.
	/// </summary>
	/// <returns><c>true</c>, if message to view model was received, <c>false</c> otherwise.</returns>
	/// <param name = "consumer">Message consumer.</param>
	/// <param name = "sender">Sender.</param>
	/// <param name="viewModelInstance">View model instance.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	bool SendMessageByViewModel<TViewModel>(MessageConsumer consumer, TViewModel viewModelInstance, string message, object sender = null, object arg = null) where TViewModel : class, INotifyPropertyChanged, IBaseMessagable;

	/// <summary>
	/// Sends the message to cached page.
	/// </summary>
	/// <returns><c>true</c>, if message to cached view model was received, <c>false</c> otherwise.</returns>
	/// <param name = "consumer">Message consumer.</param>
	/// <param name = "sender">Sender.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	/// <param name="createPageIfNotExists">If set to <c>true</c> creates page instance if not exists in cache.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	bool SendMessageToCached<TViewModel>(MessageConsumer consumer, string message, object sender = null, object arg = null, bool createPageIfNotExists = true) where TViewModel : class, INotifyPropertyChanged, IBaseMessagable;
}
	
/// <summary>
/// IPageFactory navigation.
/// </summary>
public interface IPageFactoryNavigation
{
	/// <summary>
	/// Gets the page from cache. Creates a new page instances if not exists.
	/// </summary>
	/// <returns>The page from cache.</returns>
	/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	IBasePage<INotifyPropertyChanged> GetPageFromCache<TViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Gets the page from cache. Creates a new page instances if not exists.
	/// </summary>
	/// <returns>The page from cache.</returns>
	/// <param name="viewModelType">View model type.</param>
	/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
	IBasePage<INotifyPropertyChanged> GetPageFromCache(Type viewModelType, bool resetViewModel = false);

	/// <summary>
	/// Gets the page as new instance.
	/// </summary>
	/// <returns>The page as new instance.</returns>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	IBasePage<INotifyPropertyChanged> GetPageAsNewInstance<TViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Gets the page as new instance.
	/// </summary>
	/// <returns>The page as new instance.</returns>
	/// <param name="viewModelType">View model type.</param>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> saves or replaces page in cache.</param>
	IBasePage<INotifyPropertyChanged> GetPageAsNewInstance(Type viewModelType, bool saveOrReplaceInCache = false);

	/// <summary>
	/// Gets the page by view model.
	/// </summary>
	/// <returns>The page by view model.</returns>
	/// <param name="viewModelInstance">View model instance.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	IBasePage<INotifyPropertyChanged> GetPageByViewModel<TViewModel>(TViewModel viewModelInstance) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Replaces the cached page view model.
	/// </summary>
	/// <returns><c>true</c>, if cached page view model was replaced, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
	/// <param name="newViewModel">New view model.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	bool ReplaceCachedPageViewModel<TViewModel>(TViewModel newViewModel) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Resets the cached page view model.
	/// </summary>
	/// <returns><c>true</c>, if cached page view model was reset, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	bool ResetCachedPageViewModel<TViewModel>() where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Removes the page from cache.
	/// </summary>
	/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
	/// <param name="viewModelType">View model type.</param>
	bool RemovePageFromCache(Type viewModelType);

	/// <summary>
	/// Removes the page from cache.
	/// </summary>
	/// <returns><c>true</c>, if page was removed from cache, <c>false</c> otherwise (eg. if page type doesn't exist in cache).</returns>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	bool RemovePageFromCache<TViewModel>() where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Clears the cache.
	/// </summary>
	void ClearCache();

	/// <summary>
	/// Pushes the page into navigation stack.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="page">Page to push.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	Task<bool> PushPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true);

	/// <summary>
	/// Pushes the page as modal.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="page">Page to push.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	Task<bool> PushModalPageAsync(IBasePage<INotifyPropertyChanged> page, bool animated = true);

	/// <summary>
	/// Pushes the cached page into navigation stack. Creates a new page instances if not exists.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="resetViewModel">If set to <c>true</c> resets page view model.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pushes the cached page into navigation stack and replaces view model. Creates a new page instances if not exists.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="replaceViewModel">View model to replace existing one.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pushes the cached page as modal. Creates a new page instances if not exists.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="resetViewModel">If set to <c>true</c> resets view model.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushModalPageFromCacheAsync<TViewModel>(bool resetViewModel = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pushes the cached page as modal. Creates a new page instances if not exists.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="replaceViewModel">Replaces view model.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushModalPageFromCacheAsync<TViewModel>(TViewModel replaceViewModel, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pushes a new page instance into navigation stack and replaces view model.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="replaceViewModel">View model to replace existing one.</param>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pushes a new page instance into navigation stack.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pushes a new page instance as modal.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="replaceViewModel">View model to replace existing one.</param>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushModalPageAsNewAsync<TViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pushes a new page instance as modal.
	/// </summary>
	/// <returns><c>true</c>, if page was pushed, push was not interrupted, <c>false</c> otherwise (eg. PageFactoryPushing returned <c>false</c>)</returns>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	/// <typeparam name="TViewModel">View model type.</typeparam>
	Task<bool> PushModalPageAsNewAsync<TViewModel>(bool saveOrReplaceInCache = false, bool animated = true) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Inserts the page before another page into navigation stack.
	/// </summary>
	/// <returns><c>true</c>, if page was insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
	/// <param name="page">Page to insert</param>
	/// <param name="before">Before page</param>
	bool InsertPageBefore(IBasePage<INotifyPropertyChanged> page, IBasePage<INotifyPropertyChanged> before);

	/// <summary>
	/// Inserts the cached page before another page into navigation stack. Creates a new page instances if not exists.
	/// </summary>
	/// <returns><c>true</c>, if page insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
	/// <param name="resetViewModel">If set to <c>true</c> resets the view model.</param>
	/// <typeparam name="TViewModel">Type of page to insert.</typeparam>
	/// <typeparam name="TBeforeViewModel">Type of before page.</typeparam>
	bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Inserts the cached page before another page into navigation stack and replaces the view model. Creates a new page instances if not exists.
	/// </summary>
	/// <returns><c>true</c>, if page insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
	/// <param name="replaceViewModel">View model to replace existing one.</param>
	/// <typeparam name="TViewModel">Type of page to insert.</typeparam>
	/// <typeparam name="TBeforeViewModel">Type of before page.</typeparam>
	bool InsertPageBeforeFromCache<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Inserts the page before another page into navigation stack as new instance.
	/// </summary>
	/// <returns><c>true</c>, if page insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
	/// <typeparam name="TViewModel">Type of page to insert.</typeparam>
	/// <typeparam name="TBeforeViewModel">Type of before page.</typeparam>
	bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Inserts the page before another page into navigation stack as new instance and replaces view model.
	/// </summary>
	/// <returns><c>true</c>, if page insert was not interrupted, <c>false</c> otherwise (eg. PageFactoryInserting returned <c>false</c>)</returns>
	/// <param name="replaceViewModel">View model to replace existing one.</param>
	/// <param name="saveOrReplaceInCache">If set to <c>true</c> will save page to cache or replace existing one in cache.</param>
	/// <typeparam name="TViewModel">Type of page to insert.</typeparam>
	/// <typeparam name="TBeforeViewModel">Type of before page.</typeparam>
	bool InsertPageBeforeAsNew<TViewModel, TBeforeViewModel>(TViewModel replaceViewModel, bool saveOrReplaceInCache = false) where TViewModel : class, INotifyPropertyChanged where TBeforeViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pops the page from navigation stack.
	/// </summary>
	/// <returns><c>true</c>, if page was pop was not interrupted, <c>false</c> otherwise (eg. PageFactoryPopping returned <c>false</c>)</returns>
	/// <param name="resetViewModel">If set to <c>true</c> resets the view model.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	Task<bool> PopPageAsync(bool resetViewModel = false, bool animated = true);

	/// <summary>
	/// Pops the modal page.
	/// </summary>
	/// <returns><c>true</c>, if page pop was not interrupted, <c>false</c> otherwise (eg. PageFactoryPopping returned <c>false</c>)</returns>
	/// <param name="resetViewModel">If set to <c>true</c> resets the view model.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	Task<bool> PopModalPageAsync(bool resetViewModel = false, bool animated = true);

	/// <summary>
	/// Removes the page from navigation stack.
	/// </summary>
	/// <returns><c>true</c>, if page remove was not interrupted, <c>false</c> otherwise (eg. PageFactoryRemoving returned <c>false</c>)</returns>
	/// <param name="pageToRemove">Page to remove.</param>
	bool RemovePage(IBasePage<INotifyPropertyChanged> pageToRemove);

	/// <summary>
	/// Removes the cached page from navigation stack.
	/// </summary>
	/// <returns><c>true</c>, if page remove was not interrupted, <c>false</c> otherwise (eg. PageFactoryRemoving returned <c>false</c>)</returns>
	/// <param name="removeFromCache">If set to <c>true</c> removes the page from cache.</param>
	/// <param name="resetViewModel">If set to <c>true</c> resets the view model.</param>
	/// <typeparam name="TViewModel">Cached view model type.</typeparam>
	bool RemoveCachedPage<TViewModel>(bool removeFromCache = false, bool resetViewModel = false) where TViewModel : class, INotifyPropertyChanged;

	/// <summary>
	/// Pops the pages to root.
	/// </summary>
	/// <param name="clearCache">If set to <c>true</c> clears cache.</param>
	/// <param name="animated">If set to <c>true</c> animation enabled.</param>
	Task PopPagesToRootAsync(bool clearCache = false, bool animated = true);

	/// <summary>
	/// Sets the new root and resets.
	/// </summary>
	/// <typeparam name="TViewModel">New root view model type.</typeparam>
	void SetNewRootAndReset<TViewModel>() where TViewModel : class, INotifyPropertyChanged;
}	
```
#### ViewModel interfaces
```C#
/// <summary>
/// IBasePage base messaging for pages and view models.
/// </summary>
public interface IBaseMessagable : INotifyPropertyChanged
{
	/// <summary>
	/// Handles received message.
	/// </summary>
	/// <param name = "sender">Sender.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	void PageFactoryMessageReceived(string message, object sender, object arg);
}

/// <summary>
/// Base view model interface.
/// </summary>
public interface IBaseViewModel : IBaseMessagable, INotifyPropertyChanged
{
	/// <summary>
	/// Gets the PageFactory.Factory.
	/// </summary>
	/// <value>The page factory.</value>
	IPageFactory PageFactory { get; }

	/// <summary>
	/// Handles received messages.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="message">Message.</param>
	/// <param name="arg">Argument.</param>
	void PageFactoryMessageReceived(string message, object sender, object arg);
}	
```
#### Page interfaces
```C#
/// <summary>
/// I base page.
/// </summary>
public interface IBasePage<out TViewModel> : IBasePage, IBaseMessagable, IBaseNavigationEvents where TViewModel: INotifyPropertyChanged
{
	/// <summary>
	/// Gets the view model.
	/// </summary>
	/// <value>The view model.</value>
	TViewModel ViewModel { get; }

	/// <summary>
	/// Resets the view model.
	/// </summary>
	void PageFactoryResetViewModel();

	/// <summary>
	/// Replaces the view model.
	/// </summary>
	/// <param name="newViewModel">New view model.</param>
	void PageFactoryReplaceViewModel(object newViewModel);
}	

/// <summary>
/// I base page.
/// </summary>
public interface IBasePage : IBaseMessagable, IBaseNavigationEvents
{
	/// <summary>
	/// Gets the PageFactory.Factory.
	/// </summary>
	/// <value>The page factory.</value>
	IPageFactory PageFactory { get; }
}

/// <summary>
/// IBasePage navigation events.
/// </summary>
public interface IBaseNavigationEvents
{
	/// <summary>
	/// Triggered when removing from cache.
	/// </summary>
	void PageFactoryRemovingFromCache();

	/// <summary>
	/// Triggered when pushing. If <c>false</c>returned push is cancelled.
	/// </summary>
	bool PageFactoryPushing();

	/// <summary>
	/// Triggered when popping. If <c>false</c>returned pop is cancelled.
	/// </summary>
	bool PageFactoryPopping();

	/// <summary>
	/// Triggered when removing. If <c>false</c>returned remove is cancelled.
	/// </summary>
	bool PageFactoryRemoving();

	/// <summary>
	/// Triggered when inserting. If <c>false</c>returned insert is cancelled.
	/// </summary>
	bool PageFactoryInserting();

	/// <summary>
	/// Triggered when pushed.
	/// </summary>
	void PageFactoryPushed();

	/// <summary>
	/// Triggered when popped.
	/// </summary>
	void PageFactoryPopped();

	/// <summary>
	/// Triggered when removed.
	/// </summary>
	void PageFactoryRemoved();

	/// <summary>
	/// Triggered when inserted.
	/// </summary>
	void PageFactoryInserted();
}
```