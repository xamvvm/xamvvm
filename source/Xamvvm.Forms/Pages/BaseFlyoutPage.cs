using Xamarin.Forms;

namespace Xamvvm;

public class BaseFlyoutPage<TPageModel> : FlyoutPage, IBasePage<TPageModel> where TPageModel : class, IBasePageModel
{ }