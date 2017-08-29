using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : IBasePage<Page1PageModel>
    {
        public Page1()
        {
            InitializeComponent();
        }
    }
}