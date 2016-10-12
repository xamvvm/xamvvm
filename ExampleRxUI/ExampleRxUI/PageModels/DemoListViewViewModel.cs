

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PageFactory.Examples.RxUI.ViewModels
{
    public class DemoListViewViewModel : INotifyPropertyChanged 
    {
        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }


        public ReactiveList<DogsItemViewModel> DogViewModelList;

        public DemoListViewViewModel()
        {

            DogViewModelList = new ReactiveList<DogsItemViewModel>();

            DogViewModelList.Add(new DogsItemViewModel() { Name = "Rex", Race = "German Sheppard" });
            DogViewModelList.Add(new DogsItemViewModel() { Name = "Barney", Race = "Poodle" });
            DogViewModelList.Add(new DogsItemViewModel() { Name = "Jimmy", Race = "Beagle" });
            DogViewModelList.Add(new DogsItemViewModel() { Name = "Rob", Race = "Labrador" });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    // Should be something that makes a bit of sense, so why not dogs
    public class DogsItemViewModel : ReactiveObject
    {
        private string name;
        private string race;

        public string Name
        {
            get { return name; }
            set { this.RaiseAndSetIfChanged(ref name, value); }
        }

        public string Race
        {
            get { return race; }
            set { this.RaiseAndSetIfChanged(ref race, value); }
        }
    }
}