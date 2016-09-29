using System;
using DLToolkit.PageFactory;
using ReactiveUI;
using Splat;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace PageFactory.Examples.RxUI.ViewModels
{
    public class MainPageViewModel : ReactiveObject, IRoutableViewModel, ISupportsActivation
    {

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }


        public MainPageViewModel()
        {

            SavedGuid = Guid.NewGuid();

            NavigateToListView = ReactiveCommand.Create(() =>
            {
                var newPage = PF.Factory.GetPageFromCache<DemoListViewViewModel>().PushPage();
            });


            //this.WhenActivated(d => 
            //{
            //    d(NavigateToListView = ReactiveCommand.CreateFromObservable(
            //        () => HostScreen.Router.Navigate.Execute(new DemoListViewViewModel(HostScreen))));
            //});

        }


        public ReactiveCommand NavigateToListView { get; protected set; }


        Guid savedGuid;
        private readonly ViewModelActivator activator = new ViewModelActivator();

        public Guid SavedGuid {
            get { return savedGuid; }
            set { this.RaiseAndSetIfChanged(ref savedGuid, value); }
        }


        ViewModelActivator ISupportsActivation.Activator
        {
            get { return activator; }
        }
    }
}

