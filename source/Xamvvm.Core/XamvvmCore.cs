using System;

namespace Xamvvm
{
	/// <summary>
	/// Page factory.
	/// </summary>
	public static class XamvvmCore
	{ 
		static IBaseFactory current;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public static IBaseLogger Logger { get; set; }

		/// <summary>
		/// Gets the current page.
		/// </summary>
		/// <value>The current page.</value>
		public static IBasePage<IBasePageModel> CurrentPage
		{
			get
			{
				return CurrentFactory.CurrentPage;
			}
		}

		/// <summary>
		/// Gets the current page model.
		/// </summary>
		/// <value>The current page model.</value>
		public static IBasePageModel CurrentPageModel
		{
			get
			{
				return CurrentFactory.CurrentPageModel;
			}
		}

        /// <summary>
        /// Gets the Factory instance.
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
		/// Initializes Factory.
		/// </summary>
		/// <param name="factory">Factory.</param>
		public static void SetCurrentFactory(IBaseFactory factory)
		{
			current = factory;
		}




    }
}

