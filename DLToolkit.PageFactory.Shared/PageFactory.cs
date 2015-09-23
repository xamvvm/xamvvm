using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// Page factory.
	/// </summary>
	public static class PF
	{ 
		static IPageFactory instance;

		/// <summary>
		/// Gets the factory.
		/// </summary>
		/// <value>The factory.</value>
		public static IPageFactory Factory
		{
			get 
			{
				if (instance == null)
				{
					throw new NullReferenceException("PageFactory is null. Please set PageFactory with PageFactory.Init method");
				}

				return instance;
			}
		}

		/// <summary>
		/// Sets the page factory.
		/// </summary>
		/// <param name="factory">Factory.</param>
		public static void SetPageFactory(IPageFactory factory)
		{
			instance = factory;
		}
	}
}

