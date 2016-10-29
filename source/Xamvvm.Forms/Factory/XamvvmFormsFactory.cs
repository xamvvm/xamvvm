using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Xamvvm
{
    public partial class XamvvmFormsFactory : IBaseFactory
    {
		int _maxPageCacheItems;
		readonly Dictionary<Type, Type> _pageModelTypes = new Dictionary<Type, Type>();
		readonly Dictionary<Tuple<Type, string>, int> _pageCacheHits = new Dictionary<Tuple<Type, string>, int>();
		readonly Dictionary<Tuple<Type, string>, object> _pageCache = new Dictionary<Tuple<Type, string>, object>();
		readonly Dictionary<Type, Func<object>> _pageCreation = new Dictionary<Type, Func<object>>();
		readonly Dictionary<Type, Func<object>> _pageModelCreation = new Dictionary<Type, Func<object>>();
		readonly ConditionalWeakTable<IBasePageModel, object> _weakPageCache = new ConditionalWeakTable<IBasePageModel, object>();

		public XamvvmFormsFactory(Application appInstance, int maxPageCacheItems = 6, bool automaticAssembliesDiscovery = true, params Assembly[] additionalPagesAssemblies)
		{
			_maxPageCacheItems = maxPageCacheItems;
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

						//var pageModelTypeInfo = pageModelType.GetTypeInfo();
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

		[Obsolete("Use RegisterPage")]
		public virtual void RegisterView<TPageModel, TPage>(Func<TPageModel> createPageModel = null, Func<IBasePage<TPageModel>> createPage = null) where TPageModel : class, IBasePageModel where TPage : class, IBasePage<IBasePageModel>
		{
			RegisterPage(createPageModel, createPage);
		}

		public virtual void RegisterPage<TPageModel>(Func<TPageModel> createPageModel = null, Func<IBasePage<TPageModel>> createPage = null) where TPageModel : class, IBasePageModel
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

		public virtual void RegisterNavigation<TPageModel>(Func<IBasePage<IBasePageModel>> initialPage = null, Func<TPageModel> createNavModel = null, Func<IBasePage<IBasePageModel>, IBasePage<TPageModel>> createNav = null) where TPageModel : class, IBasePageModel
		{
			if (createNavModel != null)
			{
				Func<object> found = null;
				if (_pageModelCreation.TryGetValue(typeof(TPageModel), out found))
					_pageModelCreation[typeof(TPageModel)] = createNavModel;
				else
					_pageModelCreation.Add(typeof(TPageModel), createNavModel);
			}

			if (createNav == null)
			{
				var pageModelType = typeof(TPageModel);
				var pageType = GetPageType(pageModelType);

				if (pageType == null)
				{
					if (initialPage == null)
						createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
							(page) => new BaseNavigationPage<TPageModel>());
					else
						createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
							(page) => new BaseNavigationPage<TPageModel>((Page)page));
				}
				else
				{
					if (initialPage == null)
						createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
							(page) => Activator.CreateInstance(pageType) as IBasePage<TPageModel>);
					else
						createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
							(page) => Activator.CreateInstance(pageType, page) as IBasePage<TPageModel>);
				}
			}

			var createNavWithPages = new Func<IBasePage<TPageModel>>(() =>
			{
				var page = initialPage?.Invoke();
				return createNav(page);
			});

			Func<object> foundPageCreation = null;
			if (_pageCreation.TryGetValue(typeof(TPageModel), out foundPageCreation))
				_pageCreation[typeof(TPageModel)] = createNavWithPages;
			else
				_pageCreation.Add(typeof(TPageModel), createNavWithPages);
		}

		public virtual void RegisterTabbedPage<TPageModel>(Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages, Func<TPageModel> createNavModel = null, Func<IBasePage<TPageModel>> createNav = null) where TPageModel : class, IBasePageModel
		{
			throw new NotImplementedException();
		}

		public virtual void RegisterCarouselPage<TPageModel>(Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages, Func<TPageModel> createNavModel = null, Func<IBasePage<TPageModel>> createNav = null) where TPageModel : class, IBasePageModel
		{
			throw new NotImplementedException();
		}

		public virtual void RegisterMasterDetail<TPageModel>(Func<IBasePage<IBasePageModel>> createMasterPage, Func<IBasePage<IBasePageModel>> createDetailPage, Func<TPageModel> createNavModel = null, Func<IBasePage<TPageModel>> createNav = null) where TPageModel : class, IBasePageModel
		{
			throw new NotImplementedException();
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

			return null;
        }

		internal Type GetPageModelType(IBasePage<IBasePageModel> page)
        {
            var found = page.GetType().GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IBasePage<>));
            var viewModelType = found.GenericTypeArguments.First();
            return viewModelType;
        }
    }
}

