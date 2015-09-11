using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	public static class PageFactory
	{ 
		static IPageFactory instance;

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

		public static void SetPageFactory(IPageFactory factory)
		{
			instance = factory;
		}
	}
}

