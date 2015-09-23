using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// PageFactory IBaseViewModel implementation.
	/// </summary>
	public class BaseViewModel : BaseNotifyPropertyChanged, INotifyPropertyChanged, IBaseViewModel, IBaseMessagable
	{
		/// <summary>
		/// Gets the PageFactory.Factory.
		/// </summary>
		/// <value>The page factory.</value>
		public IPageFactory PageFactory
		{
			get
			{
				return PF.Factory;
			}
		}

		/// <summary>
		/// Handles received message.
		/// </summary>
		/// <param name = "sender">Sender.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		public virtual void PageFactoryMessageReceived(string message, object sender, object arg)
		{
		}
	}
}

