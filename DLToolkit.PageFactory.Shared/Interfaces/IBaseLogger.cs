using System;

namespace DLToolkit.PageFactory
{
	public interface IBaseLogger
	{
		void LogDebug(object sender, string message);

		void LogInfo(object sender, string message);

		void LogError(object sender, Exception ex = null, string message = null);
	}
}
