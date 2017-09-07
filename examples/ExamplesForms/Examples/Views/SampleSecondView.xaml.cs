using Xamarin.Forms;
using Xamvvm;

namespace Examples
{
    public partial class SampleSecondView : ContentView, IBaseView<SampleSecondViewModel>
    {
        public SampleSecondView()
        {
            InitializeComponent();
        }
    }
}
