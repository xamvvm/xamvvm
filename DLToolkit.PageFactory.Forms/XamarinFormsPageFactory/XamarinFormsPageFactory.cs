using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactory
    {
        protected readonly Dictionary<Type, IBasePage<INotifyPropertyChanged>> pageCache = new Dictionary<Type, IBasePage<INotifyPropertyChanged>>();

        protected readonly Dictionary<Type, Type> viewModelToViewMap = new Dictionary<Type, Type>();

        readonly ConditionalWeakTable<INotifyPropertyChanged, IBasePage<INotifyPropertyChanged>> weakPageCache = new ConditionalWeakTable<INotifyPropertyChanged, IBasePage<INotifyPropertyChanged>>();

        public void AddToWeakCacheIfNotExists(IBasePage<INotifyPropertyChanged> page)
        {
            INotifyPropertyChanged viewModel = null;
            var xfPage = page as Page;
            if (xfPage != null)
                viewModel = xfPage.BindingContext as INotifyPropertyChanged;

            if (viewModel == null)
                return;

            IBasePage<INotifyPropertyChanged> weakExists;

            if (!weakPageCache.TryGetValue(viewModel, out weakExists))
            {
                weakPageCache.Add(viewModel, page);
            }
        }

        public void RemoveFromWeakCacheIfExists(IBasePage<INotifyPropertyChanged> page)
        {
            INotifyPropertyChanged viewModel = null;
            var xfPage = page as Page;
            if (xfPage != null)
                viewModel = xfPage.BindingContext as INotifyPropertyChanged;

            if (viewModel == null)
                return;

            IBasePage<INotifyPropertyChanged> weakExists;

            if (weakPageCache.TryGetValue(viewModel, out weakExists))
            {
                weakPageCache.Remove(viewModel);
            }
        }

        public NavigationPage Init<TMainPageModel, TNavigationPage>(params Assembly[] additionalPagesAssemblies) where TMainPageModel : class, INotifyPropertyChanged where TNavigationPage : NavigationPage, IBasePage<INotifyPropertyChanged>
        {
            PF.SetPageFactory(this);

            viewModelToViewMap.Clear();

            var pagesAssemblies = additionalPagesAssemblies.ToList();
            pagesAssemblies.Add(typeof(TMainPageModel).GetTypeInfo().Assembly);
            pagesAssemblies.Add(typeof(TNavigationPage).GetTypeInfo().Assembly);

            foreach (var assembly in pagesAssemblies.Distinct())
            {
                foreach(var pageTypeInfo in assembly.DefinedTypes.Where(t => t.IsClass && !t.IsAbstract 
                    && t.ImplementedInterfaces != null && !t.IsGenericTypeDefinition))
                {                    
                    var found = pageTypeInfo.ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType && 
                        t.GetGenericTypeDefinition() == typeof(IBasePage<>));

                    if (found != default(Type))
                    {
                        var pageType = pageTypeInfo.AsType();
                        var pageParameterlessCtors = (pageTypeInfo.DeclaredConstructors
                            .Where(c => c.IsPublic && c.GetParameters().Length == 0));

                        if (!pageParameterlessCtors.Any())
                            throw new Exception(string.Format("Page {0} must have a public parameterless constructor", pageType));

                        var viewModelType = found.GenericTypeArguments.First();
                        var viewModelTypeInfo = viewModelType.GetTypeInfo();

                        if (!pageTypeInfo.ImplementedInterfaces.Any(t => t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IPageModelInitializer<>)))
                        {
                            var parameterlessCtors = (viewModelTypeInfo.DeclaredConstructors
                                .Where(c => c.IsPublic && c.GetParameters().Length == 0));

                            if (!parameterlessCtors.Any())
                            {
                                throw new Exception(string.Format("PageModel {0} must have a public parameterless constructor OR Page {1} must implement IPageModelInitializer<TPageModel>", viewModelType, pageType));
                            }
                        }

                        if(!viewModelToViewMap.ContainsKey(viewModelType))
                        {
                            viewModelToViewMap.Add(viewModelType, pageType);
                        }
                        else
                        {
                            var oldPageType = viewModelToViewMap[viewModelType];

                            if (pageTypeInfo.IsSubclassOf(oldPageType))
                            {
                                viewModelToViewMap.Remove(viewModelType);
                                viewModelToViewMap.Add(viewModelType, pageType);
                            }
                        }
                    }
                }   
            }

            var page = GetPageFromCache(typeof(TMainPageModel));
            navigationPage = (NavigationPage)Activator.CreateInstance(typeof(TNavigationPage), page);

            return NavigationPage;
        }

        protected NavigationPage navigationPage = null;
        public NavigationPage NavigationPage
        {
            get
            {
                if (navigationPage == null)
                    throw new NullReferenceException("NavigationPage is null. Please set NavigationPage with Init method");

                return navigationPage;  
            }
        }

        protected Type GetPageType(Type viewModelType)
        {
            Type pageType;

            if (viewModelToViewMap.TryGetValue(viewModelType, out pageType))
            {
                return pageType;
            }

            throw new KeyNotFoundException(
                string.Format("Page definition for {0} PageModel could not be found", viewModelType.ToString()));
        }

        protected Type GetPageModelType(IBasePage<INotifyPropertyChanged> page)
        {
            var found = page.GetType().GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IBasePage<>));
            var viewModelType = found.GenericTypeArguments.First();
            return viewModelType;
        }
    }
}

