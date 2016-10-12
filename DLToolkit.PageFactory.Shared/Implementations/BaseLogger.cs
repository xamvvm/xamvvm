using System;
using System.Diagnostics;

namespace DLToolkit.PageFactory
{
	public class BaseLogger : IBaseLogger
	{
		public void LogDebug(object sender, string message)
		{
			Debug.WriteLine(string.Format("DEBUG {0}: {1}", sender?.GetType()?.Name, message));
		}

		public void LogError(object sender, Exception ex = null, string message = null)
		{
			Debug.WriteLine(string.Format("ERROR {0}: {1}{2}{3}", sender?.GetType()?.Name, message, Environment.NewLine, ex?.ToString()));
		}

		public void LogInfo(object sender, string message)
		{
			Debug.WriteLine(string.Format("INFO {0}: {1}", sender?.GetType()?.Name, message));
		}
	}
}
