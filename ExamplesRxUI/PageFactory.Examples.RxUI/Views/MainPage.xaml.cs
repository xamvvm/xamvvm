using DLToolkit.PageFactory;
using PageFactory.Examples.RxUI.ViewModels;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PageFactory.Examples.RxUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, IViewFor<MainPageViewModel>, IBasePage<MainPageViewModel>
    {    
        public MainPage ()
        {
            InitializeComponent ();

            this.WhenActivated(d =>
            {
                ViewModel = (MainPageViewModel) BindingContext;

                this.OneWayBind(ViewModel, x => x.SavedGuid, x => x.savedGuid.Text);
                this.BindCommand(ViewModel, x => x.NavigateToListView, x => x.GotoListView);
            });

        }

        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create<MainPage, MainPageViewModel>(
            x => x.ViewModel, null, BindingMode.OneWay);

        public MainPageViewModel ViewModel {
            get { return (MainPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel {
            get { return ViewModel; }
            set { ViewModel = (MainPageViewModel)value; }
        }
    }
}

