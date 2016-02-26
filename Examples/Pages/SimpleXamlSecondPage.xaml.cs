using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DLToolkit.PageFactory;
using PageFactory.Examples.ViewModels;

namespace PageFactory.Examples.Pages
{
    public partial class SimpleXamlSecondPage : ContentPage, IBaseMessagablePage<SimpleXamlSecondViewModel>
	{
		public SimpleXamlSecondPage()
		{
			InitializeComponent();
		}

        public void PageFactoryMessageReceived(string message, object sender, object arg)
        {
        }
	}
}

