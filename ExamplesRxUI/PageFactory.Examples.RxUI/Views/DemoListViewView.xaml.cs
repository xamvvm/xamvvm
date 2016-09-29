using System.Reactive.Disposables;
using DLToolkit.PageFactory;
using PageFactory.Examples.RxUI.ViewModels;
using ReactiveUI;

namespace PageFactory.Examples.RxUI.Views
{
    public partial class DemoListViewView : IBasePage<DemoListViewViewModel> 
    {
        public DemoListViewView ()
        {

            InitializeComponent ();

            this.WhenActivated(d => 
            {

                this.OneWayBind(ViewModel, vm => vm.DogViewModelList, v => v.DogListView.ItemsSource)
                    .DisposeWith(d);
            });
        }

    }
}
