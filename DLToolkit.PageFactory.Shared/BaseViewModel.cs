using System;
using System.ComponentModel;

namespace DLToolkit.PageFactory
{
	public class BaseViewModel : BaseNotifyPropertyChanged, INotifyPropertyChanged, IBaseViewModel
	{
		public IPageFactory PageFactory
		{
			get
			{
				return DLToolkit.PageFactory.PageFactory.Factory;
			}
		}
	}
}

