using Xamvvm;
using System.Windows.Input;

namespace Examples
{
    public class SampleNavigationViewPageModel : BasePageModel
    {
        readonly IViewNavigation<SampleNavigationViewNavigationModel> _nav;

        public SampleNavigationViewPageModel(IViewNavigation<SampleNavigationViewNavigationModel> nav)
        {
            _nav = nav;

            NavigateFirstCommand = new BaseCommand(async (arg) =>
            {
                await _nav.SetMainViewAsync<SampleFirstViewModel>();
            });

            NavigateSecondCommand = new BaseCommand(async (arg) =>
            {
                await _nav.SetMainViewAsync<SampleSecondViewModel>();
            });
        }

        public ICommand NavigateFirstCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }

        public ICommand NavigateSecondCommand
        {
            get { return GetField<ICommand>(); }
            set { SetField(value); }
        }
    }
}
