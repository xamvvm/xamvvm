using Xamarin.Forms;

namespace Xamvvm
{
	public class BaseCarouselPage<TPageModel> : CarouselPage, IBasePage<TPageModel> where TPageModel : class, IBasePageModel
	{ }
}