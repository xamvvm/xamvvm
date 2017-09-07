using Xamarin.Forms;
using Xamvvm;

namespace Examples
{
    public partial class SampleFirstView : ContentView, IBaseView<SampleFirstViewModel>
    {
        public SampleFirstView()
        {
            InitializeComponent();
        }
    }
}
