using System.Reactive.Disposables;
using PageFactory.Examples.RxUI.ViewModels;
using ReactiveUI;

namespace PageFactory.Examples.RxUI.Views
{
    public partial class ListViewItemView 
    {
        public ListViewItemView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                ViewModel = (DogsItemViewModel) BindingContext;
                this.OneWayBind(ViewModel, vm => vm.Name, v => v.Name.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Race, v => v.Race.Text).DisposeWith(d);
             });


        }

        
    }
}
