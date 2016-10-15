using System;

namespace Xamvvm
{
	/// <summary>
	/// IBasePage navigation events.
	/// </summary>
    public interface INavigationInterceptors : INavigationRemovingFromCache, INavigationPushing, INavigationPushed, 
        INavigationPopping, INavigationPopped, INavigationInserting, INavigationInserted, INavigationRemoving, INavigationRemoved
	{
	}

    /// <summary>
    /// INavigationRemovingFromCache.
    /// </summary>
    public interface INavigationRemovingFromCache
    {
        /// <summary>
        /// Triggered when removing from cache.
        /// </summary>
        void PageFactoryRemovingFromCache();
    }

    /// <summary>
    /// INavigationPushing.
    /// </summary>
    public interface INavigationPushing
    {
        /// <summary>
        /// Triggered when pushing. If <c>false</c>returned push is cancelled.
        /// </summary>
        bool PageFactoryPushing();
    }

    /// <summary>
    /// INavigationPushed.
    /// </summary>
    public interface INavigationPushed
    {
        /// <summary>
        /// Triggered when pushed.
        /// </summary>
        void PageFactoryPushed();
    }

    /// <summary>
    /// INavigationPopping.
    /// </summary>
    public interface INavigationPopping
    {
        /// <summary>
        /// Triggered when popping. If <c>false</c>returned pop is cancelled.
        /// </summary>
        bool PageFactoryPopping();
    }

    /// <summary>
    /// INavigationPopped.
    /// </summary>
    public interface INavigationPopped
    {
        /// <summary>
        /// Triggered when popped.
        /// </summary>
        void PageFactoryPopped();
    }

    /// <summary>
    /// INavigationInserting.
    /// </summary>
    public interface INavigationInserting
    {
        /// <summary>
        /// Triggered when inserting. If <c>false</c>returned insert is cancelled.
        /// </summary>
        bool PageFactoryInserting();
    }

    /// <summary>
    /// INavigationInserted.
    /// </summary>
    public interface INavigationInserted
    {
        /// <summary>
        /// Triggered when inserted.
        /// </summary>
        void PageFactoryInserted();
    }

    /// <summary>
    /// INavigationRemoving.
    /// </summary>
    public interface INavigationRemoving
    {
        /// <summary>
        /// Triggered when removing. If <c>false</c>returned remove is cancelled.
        /// </summary>
        bool PageFactoryRemoving();
    }

    /// <summary>
    /// INavigationRemoved.
    /// </summary>
    public interface INavigationRemoved
    {
        /// <summary>
        /// Triggered when removed.
        /// </summary>
        void PageFactoryRemoved();
    }
}

