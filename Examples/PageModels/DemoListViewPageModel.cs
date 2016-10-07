using System.Collections.ObjectModel;
using DLToolkit.PageFactory;
using PropertyChanged;

namespace Examples.PageModels
{

    [ImplementPropertyChanged]
    public class DemoListViewPageModel : BasePageModel
    {
        public string UrlPathSegment { get; }


        public ObservableCollection<DogsItemViewModel> DogViewModelList;

        public DemoListViewPageModel()
        {

            DogViewModelList = new ObservableCollection<DogsItemViewModel>();

            DogViewModelList.Add(new DogsItemViewModel() { Name = "Rex", Race = "German Sheppard" });
            DogViewModelList.Add(new DogsItemViewModel() { Name = "Barney", Race = "Poodle" });
            DogViewModelList.Add(new DogsItemViewModel() { Name = "Jimmy", Race = "Beagle" });
            DogViewModelList.Add(new DogsItemViewModel() { Name = "Rob", Race = "Labrador" });
        }


    }


    // Should be something that makes a bit of sense, so why not dogs
    [ImplementPropertyChanged]
    public class DogsItemViewModel 
    {
        private string name;
        private string race;

        public string Name { get; set; }

        public string Race { get; set; }
    }
}