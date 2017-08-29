using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamvvm;

namespace App1
{
    public class Page1PageModel : INotifyPropertyChanged, IBasePageModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string LabelText { get; set; }

        public ICommand PopCommand { get; set; }


        public Page1PageModel()
        {
            PopCommand = new Command(() => this.PopPageAsync());    
        }
    }
}