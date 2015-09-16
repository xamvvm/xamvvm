using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IBasePage base messaging for pages and view models.
	/// </summary>
	public interface IBaseMessagable : INotifyPropertyChanged
	{
		/// <summary>
		/// Handles received message.
		/// </summary>
		/// <param name = "sender">Sender.</param>
		/// <param name="message">Message.</param>
		/// <param name="arg">Argument.</param>
		void PageFactoryMessageReceived(string message, object sender, object arg);
	}
}

