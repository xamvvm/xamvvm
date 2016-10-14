using System;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// Page factory.
	/// </summary>
	public static class XamvvmCore
	{ 
		static IBaseFactory current;

		/// <summary>
		/// Gets the PageFactory instance.
		/// </summary>
		/// <value>The factory.</value>
		public static IBaseFactory CurrentFactory
		{
			get 
			{
				if (current == null)
				{
					throw new NullReferenceException("CurrentFactory is null. Please initialize it with SetCurrentFactory method");
				}

				return current;
			}
		}

		/// <summary>
		/// Initializes PageFactory.
		/// </summary>
		/// <param name="factory">Factory.</param>
		public static void SetCurrentFactory(IBaseFactory factory)
		{
			current = factory;
		}
	}
}

