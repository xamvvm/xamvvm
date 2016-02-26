using System;
using DLToolkit.PageFactory;

namespace PageFactory.Examples.ViewModels
{
    public class ViewModelCtorViewModel : BasePageModel
	{
		public ViewModelCtorViewModel(Guid guid)
		{
			LabelText = string.Format("Parameter from non-default constructor is: {0}", guid);
		}

		public string LabelText
		{
			get { return GetField<string>(); }
			set { SetField(value); }
		}
	}
}

