using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DLToolkit.PageFactory
{
    public partial class XamarinFormsPageFactory : IPageFactory
    {
		int _maxPageCacheItems;
		readonly Dictionary<Type, Type> _pageModelTypes = new Dictionary<Type, Type>();
		readonly Dictionary<Tuple<Type, string>, int> _pageCacheHits = new Dictionary<Tuple<Type, string>, int>();
		readonly Dictionary<Tuple<Type, string>, object> _pageCache = new Dictionary<Tuple<Type, string>, object>();
		readonly Dictionary<Type, Func<object>> _pageCreation = new Dictionary<Type, Func<object>>();
		readonly Dictionary<Type, Func<object>> _pageModelCreation = new Dictionary<Type, Func<object>>();
		readonly ConditionalWeakTable<IBasePageModel, object> _weakPageCache = new ConditionalWeakTable<IBasePageModel, object>();

		public XamarinFormsPageFactory(Application appInstance, int maxPageCacheItems = 6, bool automaticAssembliesDiscovery = true, params Assembly[] additionalPagesAssemblies)
		{
			_maxPageCacheItems = maxPageCacheItems;
			_pageModelTypes.Clear();
			_pageCache.Clear();
			_pageModelCreation.Clear();
			_pageCreation.Clear();

			var pagesAssemblies = additionalPagesAssemblies.ToList();

			if (automaticAssembliesDiscovery)
				pagesAssemblies.Add(appInstance.GetType().GetTypeInfo().Assembly);

			foreach (var assembly in pagesAssemblies.Distinct())
			{
				foreach (var pageTypeInfo in assembly.DefinedTypes.Where(t => t.IsClass && !t.IsAbstract
					 && t.ImplementedInterfaces != null && !t.IsGenericTypeDefinition))
				{
					var found = pageTypeInfo.ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType &&
						t.GetGenericTypeDefinition() == typeof(IBasePage<>));

					if (found != default(Type))
					{
						var pageType = pageTypeInfo.AsType();

						//var pageParameterlessCtors = (pageTypeInfo.DeclaredConstructors
						//	.Where(c => c.IsPublic && c.GetParameters().Length == 0));

						//if (!pageParameterlessCtors.Any())
						//	throw new Exception(string.Format("Page {0} must have a public parameterless constructor", pageType));

						var pageModelType = found.GenericTypeArguments.First();
						var pageModelTypeInfo = pageModelType.GetTypeInfo();

						//var parameterlessCtors = (pageModelTypeInfo.DeclaredConstructors
						//	.Where(c => c.IsPublic && c.GetParameters().Length == 0));

						//if (!parameterlessCtors.Any())
						//{
						//	throw new Exception(string.Format("PageModel {0} must have a public parameterless constructor",
						//									  pageModelType, pageType));
						//}

						if (!_pageModelTypes.ContainsKey(pageModelType))
						{
							_pageModelTypes.Add(pageModelType, pageType);
						}
						else
						{
							var oldPageType = _pageModelTypes[pageModelType];

							if (pageTypeInfo.IsSubclassOf(oldPageType))
							{
								_pageModelTypes.Remove(pageModelType);
								_pageModelTypes.Add(pageModelType, pageType);
							}
						}
					}
				}
			}
		}

		public virtual void RegisterView<TPageModel, TPage>(Func<IBasePageModel> createPageModel = null, Func<object> createPage = null) where TPageModel : class, IBasePageModel where TPage : class
		{
			if (createPageModel != null)
			{
				Func<object> found = null;
				if (_pageModelCreation.TryGetValue(typeof(TPageModel), out found))
					_pageModelCreation[typeof(TPageModel)] = createPageModel;
				else
					_pageModelCreation.Add(typeof(TPageModel), createPageModel);
			}

			if (createPage != null)
			{
				Func<object> found = null;
				if (_pageCreation.TryGetValue(typeof(TPageModel), out found))
					_pageCreation[typeof(TPageModel)] = createPage;
				else
					_pageCreation.Add(typeof(TPageModel), createPage);
			}
		}

		internal void AddToWeakCacheIfNotExists<TPageModel>(IBasePage<TPageModel> page, TPageModel pageModel) where TPageModel : class, IBasePageModel
		{
			if (pageModel == null)
				return;

			object weakExists;
			if (!_weakPageCache.TryGetValue(pageModel, out weakExists))
				_weakPageCache.Add(pageModel, page);
		}

        internal Type GetPageType(Type pageModelType)
        {
			Type pageType = null;
			if (_pageModelTypes.TryGetValue(pageModelType, out pageType))
            	return pageType;

            throw new KeyNotFoundException(
                string.Format("Page definition for {0} PageModel could not be found", pageModelType.ToString()));
        }

		internal Type GetPageModelType(IBasePage<IBasePageModel> page)
        {
            var found = page.GetType().GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IBasePage<>));
            var viewModelType = found.GenericTypeArguments.First();
            return viewModelType;
        }
    }
}

