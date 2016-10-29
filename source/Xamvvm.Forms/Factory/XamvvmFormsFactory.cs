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

				if (initialPage == null)
					createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
						(page) => pageType == null ? 
							new BaseNavigationPage<TPageModel>() : Activator.CreateInstance(pageType) as IBasePage<TPageModel>);
				else
					createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
						(page) => pageType == null ? 
							new BaseNavigationPage<TPageModel>((Page)page) : Activator.CreateInstance(pageType, page) as IBasePage<TPageModel>);
			}

			var createNavWithPage = new Func<IBasePage<TPageModel>>(() =>
			{
				var page = initialPage?.Invoke();
				return createNav(page);
			});

			Func<object> foundPageCreation = null;
			if (_pageCreation.TryGetValue(typeof(TPageModel), out foundPageCreation))
				_pageCreation[typeof(TPageModel)] = createNavWithPage;
			else
				_pageCreation.Add(typeof(TPageModel), createNavWithPage);
		}

		public virtual void RegisterMultiPage<TPageModel, TFormsPageType, TFormsSubPageType>(Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages, Func<TPageModel> createMultModel = null, Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TPageModel>> createMult = null) where TPageModel : class, IBasePageModel where TFormsPageType : MultiPage<TFormsSubPageType>, new() where TFormsSubPageType: Page
		{
			if (createMultModel != null)
			{
				Func<object> found = null;
				if (_pageModelCreation.TryGetValue(typeof(TPageModel), out found))
					_pageModelCreation[typeof(TPageModel)] = createMultModel;
				else
					_pageModelCreation.Add(typeof(TPageModel), createMultModel);
			}

			if (createMult == null)
			{
				var pageModelType = typeof(TPageModel);
				var pageType = GetPageType(pageModelType);

				if (createSubPages == null)
					createMult = new Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TPageModel>>(
						(pages) => pageType == null ? 
							new TFormsPageType() as IBasePage<TPageModel> : Activator.CreateInstance(pageType) as IBasePage<TPageModel>);
				else
					createMult = new Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TPageModel>>(
						(pages) =>
						{
							var multiPage = pageType == null ?
								new TFormsPageType() as IBasePage<TPageModel> : Activator.CreateInstance(pageType) as IBasePage<TPageModel>;
							var multiPageXam = (TabbedPage)multiPage;

							foreach (var page in pages)
							{
								multiPageXam.Children.Add((Page)page);
							}

							return multiPage;
						});
			}

			var createMultWithPages = new Func<IBasePage<TPageModel>>(() =>
			{
				var pages = createSubPages?.Invoke();
				return createMult(pages);
			});

			Func<object> foundPageCreation = null;
			if (_pageCreation.TryGetValue(typeof(TPageModel), out foundPageCreation))
				_pageCreation[typeof(TPageModel)] = createMultWithPages;
			else
				_pageCreation.Add(typeof(TPageModel), createMultWithPages);
		}

		public virtual void RegisterTabbedPage<TPageModel>(Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages, Func<TPageModel> createTabModel = null, Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TPageModel>> createTab = null) where TPageModel : class, IBasePageModel
		{
			RegisterMultiPage<TPageModel, TabbedPage, Page>(createSubPages, createTabModel, createTab);
		}

		public virtual void RegisterCarouselPage<TPageModel>(Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages, Func<TPageModel> createCarModel = null, Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TPageModel>> createCar = null) where TPageModel : class, IBasePageModel
		{
			RegisterMultiPage<TPageModel, CarouselPage, ContentPage>(createSubPages, createCarModel, createCar);
		}

		public virtual void RegisterMasterDetail<TPageModel>(Func<IBasePage<IBasePageModel>> createMasterPage, Func<IBasePage<IBasePageModel>> createDetailPage, Func<TPageModel> createMasDetModel = null, Func<IBasePage<IBasePageModel>, IBasePage<IBasePageModel>, IBasePage<TPageModel>> createMasDet = null) where TPageModel : class, IBasePageModel
		{
			if (createMasDetModel != null)
			{
				Func<object> found = null;
				if (_pageModelCreation.TryGetValue(typeof(TPageModel), out found))
					_pageModelCreation[typeof(TPageModel)] = createMasDetModel;
				else
					_pageModelCreation.Add(typeof(TPageModel), createMasDetModel);
			}

			if (createMasDet == null)
			{
				var pageModelType = typeof(TPageModel);
				var pageType = GetPageType(pageModelType);

				createMasDet = new Func<IBasePage<IBasePageModel>, IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
					(master, detail) =>
					{
						var masdetPage = pageType == null ? 
							new BaseMasterDetailPage<TPageModel>() : Activator.CreateInstance(pageType) as IBasePage<TPageModel>;
						var masdetPageXam = (MasterDetailPage)masdetPage;
						masdetPageXam.Master = master as Page;
						masdetPageXam.Detail = detail as Page;
						return masdetPage;
					});
			}

			var createMasDetWithPages = new Func<IBasePage<TPageModel>>(() =>
			{
				var masterPage = createMasterPage?.Invoke();
				var detailPage = createDetailPage?.Invoke();
				return createMasDet(masterPage, detailPage);
			});

			Func<object> foundPageCreation = null;
			if (_pageCreation.TryGetValue(typeof(TPageModel), out foundPageCreation))
				_pageCreation[typeof(TPageModel)] = createMasDetWithPages;
			else
				_pageCreation.Add(typeof(TPageModel), createMasDetWithPages);
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

