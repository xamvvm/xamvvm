using System;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// Message consumer.
	/// </summary>
	public enum MessageConsumer
	{
		/// <summary>
		/// Only ViewModel will receive the message.
		/// </summary>
		ViewModel,
		/// <summary>
		/// Only Page will receive the message.
		/// </summary>
		Page,
		/// <summary>
		/// Page and ViewModel will receive the message.
		/// </summary>
		PageAndViewModel,
	}
}
