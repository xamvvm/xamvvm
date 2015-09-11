using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	public interface IBasePage<out TViewModel> : IBasePage where TViewModel: INotifyPropertyChanged
	{
		TViewModel ViewModel { get; }

		void ResetViewModel();

		void ReplaceViewModel(object newViewModel);
	}	

	public interface IBasePage
	{
		IPageFactory PageFactory { get; }

		void PageFactoryMessageReceived(string message, object arg);

		void PageFactoryRemovingFromCache();

		bool PageFactoryPushing();

		bool PageFactoryPopping();

		bool PageFactoryRemoving();

		bool PageFactoryInserting();

		void PageFactoryPushed();

		void PageFactoryPopped();

		void PageFactoryRemoved();

		void PageFactoryInserted();
		// TODO
		//			void PageFactoryRemovedFromNavigation();
		//
		//			void PageFactoryAddedToNavigation();
	}
}

