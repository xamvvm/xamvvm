using System;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// Page factory.
	/// </summary>
	public static class PageFactory
	{ 
		static IPageFactory instance;

		/// <summary>
		/// Gets the PageFactory instance.
		/// </summary>
		/// <value>The factory.</value>
		public static IPageFactory Instance
		{
			get 
			{
				if (instance == null)
				{
					throw new NullReferenceException("PageFactory is null. Please initialize PageFactory with PageFactory.Init method");
				}

				return instance;
			}
		}

		/// <summary>
		/// Initializes PageFactory.
		/// </summary>
		/// <param name="factory">Factory.</param>
		public static void Init(IPageFactory factory)
		{
			instance = factory;
		}
	}
}

