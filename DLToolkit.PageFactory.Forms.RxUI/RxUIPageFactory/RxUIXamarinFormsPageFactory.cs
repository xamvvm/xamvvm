using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Logging;
using ReactiveUI;

namespace DLToolkit.PageFactory
{
    public partial class RxUIPageFactory : XamarinFormsPageFactory
    {
        private static bool staticInitialization = false;

        static Dictionary<Type, Func<IBasePage<INotifyPropertyChanged>>> viewModelToViewCreationMap = new Dictionary<Type, Func<IBasePage<INotifyPropertyChanged>>>();
        static Dictionary<Type, Func<INotifyPropertyChanged>> viewToViewModelCreationMap = new Dictionary<Type, Func<INotifyPropertyChanged>>();



        public static void RegisterViews<viewModelType, viewType>(Func<viewModelType> viewModelCreationFunc, Func<viewType> viewCreationFunc)
            where viewType : class, IViewFor<viewModelType>, IBasePage<viewModelType>, new()
            where viewModelType : class, INotifyPropertyChanged, new()
        {
            staticInitialization = true;
            viewModelToViewCreationMap.Add(typeof(viewModelType), viewCreationFunc);
            viewToViewModelCreationMap.Add(typeof(viewType), viewModelCreationFunc);
        }



        public static void RegisterViews<viewModelType, viewType>()
            where viewType : class, IViewFor<viewModelType>, IBasePage<viewModelType>, new()
            where viewModelType : class, INotifyPropertyChanged, new()
        {
            staticInitialization = true;
            viewModelToViewCreationMap.Add(typeof(viewModelType), () => new viewType());
            viewToViewModelCreationMap.Add(typeof(viewType), () => new viewModelType());
        }


        public NavigationPage InitStatic<TMainPageModel, TNavigationPage>()
            where TMainPageModel : class, INotifyPropertyChanged
            where TNavigationPage : NavigationPage, IBasePage<INotifyPropertyChanged>, new()
        {

            PF.SetPageFactory(this);

            var page = GetPageFromCache(typeof(TMainPageModel));

            // Not sure if this is clean
            navigationPage = new TNavigationPage();
            navigationPage.PushAsync((Page) page);

            return NavigationPage;
        }



        public NavigationPage InitWithPageTypes<TMainPageModel, TNavigationPage>(params Type[] pageTypes)
            where TMainPageModel : class, INotifyPropertyChanged
            where TNavigationPage : NavigationPage, IBasePage<INotifyPropertyChanged>
        {
            PF.SetPageFactory(this);
            staticInitialization = false;

            viewModelToViewMap.Clear();

            foreach (var pageType in pageTypes)
            {
                var basePageInterface =
                    pageType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType &&
                                                                                        t.GetGenericTypeDefinition() ==
                                                                                        typeof(IBasePage<>));

                var viewModelType = basePageInterface.GenericTypeArguments.First();

                if (!viewModelToViewMap.ContainsKey(viewModelType))
                {
                    viewModelToViewMap.Add(viewModelType, pageType);
                }
                else
                {
                    var pageTypeInfo = pageType.GetTypeInfo();

                    var oldPageType = viewModelToViewMap[viewModelType];

                    if (pageTypeInfo.IsSubclassOf(oldPageType))
                    {
                        viewModelToViewMap.Remove(viewModelType);
                        viewModelToViewMap.Add(viewModelType, pageType);
                    }
                }

            }
            var page = GetPageFromCache(typeof(TMainPageModel));

            navigationPage = (NavigationPage) Activator.CreateInstance(typeof(TNavigationPage), page);

            return NavigationPage;
        }
    }
}

