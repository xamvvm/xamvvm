namespace Xamvvm
{
	public class MockPage<TPageModel> : IBasePage<TPageModel> where TPageModel : class, IBasePageModel
	{
		public TPageModel BindingContext { get; set; }
	}
}
