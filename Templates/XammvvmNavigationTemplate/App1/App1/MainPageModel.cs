using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;

namespace App1
{
    public class MainPageModel : INotifyPropertyChanged, IBasePageModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string HelloText { get; set; } = "Hello from Xamvvm";

        public ICommand PushCommand { get; set; }


        public MainPageModel()
        {
            PushCommand = new Command(() 
                => this.PushPageFromCacheAsync<Page1PageModel>(model => model.LabelText = "This Page was just pushed" ));
        }
    }
}