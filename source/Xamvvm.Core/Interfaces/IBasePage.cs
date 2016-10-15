using System;
using System.ComponentModel;

namespace Xamvvm
{
	/// <summary>
	/// Base page.
	/// </summary>
	public interface IBasePage<TPageModel> where TPageModel: class, IBasePageModel
	{
	}
}

