using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamvvm;

namespace App1
{
    public class MainPageModel : INotifyPropertyChanged, IBasePageModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string HelloText { get; set; } = "Hello from Xamvvm";

    }
}