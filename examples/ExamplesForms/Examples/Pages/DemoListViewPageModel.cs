using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamvvm;

namespace Examples
{
    public class DemoListViewPageModel : BasePageModel
    {
        public ObservableCollection<DogsItemViewModel> DogViewModelList
		{
			get { return GetField<ObservableCollection<DogsItemViewModel>>(); }
			set { SetField(value); }
		}

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
public class DogsItemViewModel : BaseModel
{
    private Dog DogObject;
    ICommand AddToCartCommand;

    public DogsItemViewModel(Dog theDog)
	{
	    DogObject = theDog;

        AddToCartCommand = new Xamarin.Forms.Command(AddToCart);
	}

	public string Name => DogObject.Name;
	public string Race => DogObject.Race.ToUpper();

    public bool IsChecked { get; set; }


	void AddToCart()
	{
	    WebService.Instance.AddToCart(DogObject);  // Whatever this will do ;-) 
	}
}


    
    public class Dog : BaseModel
    {
        public string Name { get; set}

        public string Race { get; set; }
    }

}