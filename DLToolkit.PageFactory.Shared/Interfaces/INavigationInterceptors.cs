using System;

namespace DLToolkit.PageFactory
{
	/// <summary>
	/// IBasePage navigation events.
	/// </summary>
    public interface INavigationInterceptors : INavigationRemovingFromCache, INavigationPushing, INavigationPushed, 
        INavigationPopping, INavigationPopped, INavigationInserting, INavigationInserted, INavigationRemoving, INavigationRemoved
	{
	}

    public interface INavigationRemovingFromCache
    {
        /// <summary>
        /// Triggered when removing from cache.
        /// </summary>
        void PageFactoryRemovingFromCache();
    }

    public interface INavigationPushing
    {
        /// <summary>
        /// Triggered when pushing. If <c>false</c>returned push is cancelled.
        /// </summary>
        bool PageFactoryPushing();
    }

    public interface INavigationPushed
    {
        /// <summary>
        /// Triggered when pushed.
        /// </summary>
        void PageFactoryPushed();
    }

    public interface INavigationPopping
    {
        /// <summary>
        /// Triggered when popping. If <c>false</c>returned pop is cancelled.
        /// </summary>
        bool PageFactoryPopping();
    }

    public interface INavigationPopped
    {
        /// <summary>
        /// Triggered when popped.
        /// </summary>
        void PageFactoryPopped();
    }

    public interface INavigationInserting
    {
        /// <summary>
        /// Triggered when inserting. If <c>false</c>returned insert is cancelled.
        /// </summary>
        bool PageFactoryInserting();
    }

    public interface INavigationInserted
    {
        /// <summary>
        /// Triggered when inserted.
        /// </summary>
        void PageFactoryInserted();
    }

    public interface INavigationRemoving
    {
        /// <summary>
        /// Triggered when removing. If <c>false</c>returned remove is cancelled.
        /// </summary>
        bool PageFactoryRemoving();
    }

    public interface INavigationRemoved
    {
        /// <summary>
        /// Triggered when removed.
        /// </summary>
        void PageFactoryRemoved();
    }
}

