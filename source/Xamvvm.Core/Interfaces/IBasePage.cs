namespace Xamvvm
{
	/// <summary>
	/// Base page.
	/// </summary>
	public interface IBasePage<out TPageModel> where TPageModel: class, IBasePageModel
	{
	}
}

