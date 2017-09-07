using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace Xamvvm
{
    public abstract class ViewNavigation<TViewNavigationPageModel> : ContentView, IViewNavigation<TViewNavigationPageModel> where TViewNavigationPageModel: class, IBaseViewModel
    {
        readonly Dictionary<Type, View> _cached = new Dictionary<Type, View>();
        readonly List<View> _navigationStack = new List<View>();
        readonly List<IBaseViewModel> _navigationModelsStack = new List<IBaseViewModel>();

        public ViewNavigation()
        {
            BindingContext = XamvvmIoC.Resolve<TViewNavigationPageModel>();
            XamvvmIoC.RegisterSingleton<IViewNavigation<TViewNavigationPageModel>>(this);
        }

        protected IList<Type> CachedViewModels { get; set; }

        public IList<IBaseViewModel> NavigationStack => _navigationModelsStack;

        public IBaseViewModel CurrentModel => _navigationModelsStack.LastOrDefault();

        protected virtual View CreateView(Type modelType)
        {
            var viewType = ((XamvvmFormsFactory)XamvvmCore.CurrentFactory).GetViewType(modelType);
            View view = XamvvmIoC.Resolve(viewType) as View;
            view.BindingContext = XamvvmIoC.Resolve(modelType);

            return view;
        }

        protected virtual View GetView(Type modelType)
        {
            View cached = null;

            if (_cached.TryGetValue(modelType, out cached))
            {
                if (cached != null)
                {
                    return _cached[modelType];
                }
            }

            var view = CreateView(modelType);

            if (CachedViewModels != null && CachedViewModels.Contains(modelType))
            {
                _cached[modelType] = view;
            }

            return view;
        }

        protected virtual Task SetContentAsync(View newContent)
        {
            var tcs = new TaskCompletionSource<bool>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    Content = newContent;
                    OnPropertyChanged(nameof(CurrentModel));
                    tcs.SetResult(true);
                }
                catch (Exception)
                {
                    tcs.SetResult(false);
                }
            });

            return tcs.Task;
        }

        public virtual async Task PopViewAsync()
        {
            if (_navigationStack.Count > 1)
            {
                _navigationStack.RemoveAt(_navigationStack.Count - 1);
                _navigationModelsStack.RemoveAt(_navigationModelsStack.Count - 1);

                await Animate(true);
                await SetContentAsync(_navigationStack.Last());
                await Animate(false);
            }
        }

        public virtual async Task PushViewAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IBaseViewModel
        {
            var view = GetView(typeof(TViewModel));
            var model = view.BindingContext as TViewModel;
            _navigationStack.Add(view);
            _navigationModelsStack.Add(model);
            action?.Invoke(model);

            await Animate(false);
            await SetContentAsync(view);
            await Animate(true);
        }

        public virtual async Task SetMainViewAsync<TViewModel>(Action<TViewModel> action = null) where TViewModel : class, IBaseViewModel
        {
            var animate = _navigationStack.Count > 0;

            var view = GetView(typeof(TViewModel));
            var model = view.BindingContext as TViewModel;

            _navigationStack.Clear();
            _navigationModelsStack.Clear();
            _navigationStack.Add(view);
            _navigationModelsStack.Add(model);
            action?.Invoke(model);

            if (animate)
                await Animate(true);

            await SetContentAsync(view);

            if (animate)
                await Animate(false);
        }

        protected virtual Task Animate(bool hide)
        {
            return Task.Delay(110);
        }

        public virtual async Task PopToRootAsync()
        {
            if (_navigationStack.Count <= 1)
                return;

            var animate = _navigationStack.Count > 0;

            if (animate)
                await Animate(true);

            var firstView = _navigationStack.First();
            var firstModel = _navigationModelsStack.First();

            _navigationStack.Clear();
            _navigationModelsStack.Clear();
            _navigationStack.Add(firstView);
            _navigationModelsStack.Add(firstModel);

            await SetContentAsync(firstView);

            if (animate)
                await Animate(false);
        }

        public virtual void ClearCache()
        {
            _cached.Clear();
        }
    }
}
