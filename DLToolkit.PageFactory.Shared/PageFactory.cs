using System;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// Page factory.
	/// </summary>
	public static class PageFactory
	{ 
		static IPageFactory current;

		/// <summary>
		/// Gets the PageFactory instance.
		/// </summary>
		/// <value>The factory.</value>
		public static IPageFactory Current
		{
			get 
			{
				if (current == null)
				{
					throw new NullReferenceException("PageFactory is null. Please initialize PageFactory with PageFactory.Init method");
				}

				return current;
			}
		}

		/// <summary>
		/// Initializes PageFactory.
		/// </summary>
		/// <param name="factory">Factory.</param>
		public static void SetCurrentFactory(IPageFactory factory)
		{
			current = factory;
		}
	}
}

