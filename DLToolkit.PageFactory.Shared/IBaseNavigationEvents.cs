using System;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IBasePage navigation events.
	/// </summary>
	public interface IBaseNavigationEvents
	{
		/// <summary>
		/// Triggered when removing from cache.
		/// </summary>
		void PageFactoryRemovingFromCache();

		/// <summary>
		/// Triggered when pushing. If <c>false</c>returned push is cancelled.
		/// </summary>
		bool PageFactoryPushing();

		/// <summary>
		/// Triggered when popping. If <c>false</c>returned pop is cancelled.
		/// </summary>
		bool PageFactoryPopping();

		/// <summary>
		/// Triggered when removing. If <c>false</c>returned remove is cancelled.
		/// </summary>
		bool PageFactoryRemoving();

		/// <summary>
		/// Triggered when inserting. If <c>false</c>returned insert is cancelled.
		/// </summary>
		bool PageFactoryInserting();

		/// <summary>
		/// Triggered when pushed.
		/// </summary>
		void PageFactoryPushed();

		/// <summary>
		/// Triggered when popped.
		/// </summary>
		void PageFactoryPopped();

		/// <summary>
		/// Triggered when removed.
		/// </summary>
		void PageFactoryRemoved();

		/// <summary>
		/// Triggered when inserted.
		/// </summary>
		void PageFactoryInserted();
	}
}

