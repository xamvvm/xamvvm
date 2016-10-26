using System;
namespace Xamvvm
{
	public partial class XamvvmMockFactory : IBaseFactory
	{
	    public enum XammvvmAction
	    {
            PagePushed, 
            ModalPagePushed, 
            PagePopped, 
            ModalPagePopped, 
            PoppedToRoot, 
            SetNewRootAndReset,

            PageInserted, 
            PageRemoved,

            PageModelChanged,

            PageRemovedFromCache,
            CacheCleared,
        };

        // Last Action that was executed
	    public static XammvvmAction LastAction { get; set; }

        // Flag that tells if the LastAction was successfull, e.g. if a Nagigation was intercepted or not.
        public static bool LastActionSuccess { get; set; }

        // PageModel on which the LastAction was executed. If no PageModel was involved it is null 
	    public static IBasePageModel TargetPageModel { get; set; }
	}
}
