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
        void NavigationRemovingFromCache();
    }

    /// <summary>
    /// INavigationPushing.
    /// </summary>
    public interface INavigationPushing
    {
        /// <summary>
        /// Triggered when pushing. If <c>false</c>returned push is cancelled.
        /// </summary>
        bool NavigationPushing();
    }

    /// <summary>
    /// INavigationPushed.
    /// </summary>
    public interface INavigationPushed
    {
        /// <summary>
        /// Triggered when pushed.
        /// </summary>
        void NavigationPushed();
    }

    /// <summary>
    /// INavigationPopping.
    /// </summary>
    public interface INavigationPopping
    {
        /// <summary>
        /// Triggered when popping. If <c>false</c>returned pop is cancelled.
        /// </summary>
        bool NavigationPopping();
    }

    /// <summary>
    /// INavigationPopped.
    /// </summary>
    public interface INavigationPopped
    {
        /// <summary>
        /// Triggered when popped.
        /// </summary>
        void NavigationPopped();
    }

    /// <summary>
    /// INavigationInserting.
    /// </summary>
    public interface INavigationInserting
    {
        /// <summary>
        /// Triggered when inserting. If <c>false</c>returned insert is cancelled.
        /// </summary>
        bool NavigationInserting();
    }

    /// <summary>
    /// INavigationInserted.
    /// </summary>
    public interface INavigationInserted
    {
        /// <summary>
        /// Triggered when inserted.
        /// </summary>
        void NavigationInserted();
    }

    /// <summary>
    /// INavigationRemoving.
    /// </summary>
    public interface INavigationRemoving
    {
        /// <summary>
        /// Triggered when removing. If <c>false</c>returned remove is cancelled.
        /// </summary>
        bool NavigationRemoving();
    }

    /// <summary>
    /// INavigationRemoved.
    /// </summary>
    public interface INavigationRemoved
    {
        /// <summary>
        /// Triggered when removed.
        /// </summary>
        void NavigationRemoved();
    }
}

