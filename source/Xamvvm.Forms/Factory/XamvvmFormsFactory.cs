using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Xamvvm
{
	public partial class XamvvmFormsFactory : IBaseFactory
	{
		int _maxPageCacheItems;
		readonly Dictionary<Type, Type> _pageModelTypes = new Dictionary<Type, Type>();
		readonly Dictionary<Type, Type> _viewModelTypes = new Dictionary<Type, Type>();
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
						var pageModelType = found.GenericTypeArguments.First();

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

					var foundView = pageTypeInfo.ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType &&
					                                                                       t.GetGenericTypeDefinition() == typeof(IBaseView<>));

					if (foundView != default(Type))
					{
						var viewType = pageTypeInfo.AsType();
						var viewModelType = foundView.GenericTypeArguments.First();

						if (!_viewModelTypes.ContainsKey(viewModelType))
						{
							_viewModelTypes.Add(viewModelType, viewType);
						}
						else
						{
							var oldPageType = _viewModelTypes[viewModelType];

							if (pageTypeInfo.IsSubclassOf(oldPageType))
							{
								_viewModelTypes.Remove(viewModelType);
								_viewModelTypes.Add(viewModelType, viewType);
							}
						}
					}
				}
			}
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

		public virtual void RegisterNavigationPage<TNavPageModel, TInitalPageModel>(bool initialPageFromCache = true)
			where TNavPageModel : class, IBasePageModel
			where TInitalPageModel : class, IBasePageModel
		{
			RegisterNavigationPage<TNavPageModel>(() =>
				initialPageFromCache ? GetPageFromCache<TInitalPageModel>() : GetPageAsNewInstance<TInitalPageModel>(), null, null);
		}

		/// <summary>
		/// Used to register the initial navigation page model
		/// </summary>
		/// <typeparam name="TNavPageModel">Page Model for the navigation page</typeparam>
		/// <param name="initialPage">Page Model for the initial page to navigate to</param>
		/// <param name="createNavModel">TBD, defaults to null</param>
		/// <param name="createNav">TBD, defaults to null</param>
		public virtual void RegisterNavigationPage<TNavPageModel>(Func<IBasePage<IBasePageModel>> initialPage = null,
			Func<TNavPageModel> createNavModel = null,
			Func<IBasePage<IBasePageModel>, IBasePage<TNavPageModel>> createNav = null) where TNavPageModel : class, IBasePageModel
		{
			// This defaults to null, when is it used?
			if (createNavModel != null)
			{
				Func<object> found = null;
				if (_pageModelCreation.TryGetValue(typeof(TNavPageModel), out found))
					_pageModelCreation[typeof(TNavPageModel)] = createNavModel;
				else
					_pageModelCreation.Add(typeof(TNavPageModel), createNavModel);
			}

			// This defaults to null, when is it used?
			if (createNav == null)
			{
				var pageModelType = typeof(TNavPageModel);
				var pageType = GetPageType(pageModelType) ?? typeof(BaseNavigationPage<TNavPageModel>);
				_pageModelTypes[pageModelType] = pageType;

				if (initialPage == null)
				{
					createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TNavPageModel>>(
						(page) => XamvvmIoC.Resolve(pageType) as IBasePage<TNavPageModel>);
				}
				else
				{
					try
					{
						createNav = new Func<IBasePage<IBasePageModel>, IBasePage<TNavPageModel>>(
							(page) => Activator.CreateInstance(pageType, page) as IBasePage<TNavPageModel>);
					}
					catch (MissingMemberException)
					{
						throw new MissingMemberException(pageType + " is missing a constructor that accepts a child page as parameter");
					}
				}
			}

			// this creates a new lambda function that will be later invoked
			var createNavWithPage = new Func<IBasePage<TNavPageModel>>(() =>
			{
				// Take the initial page and invoke it. The page is passed in as a lambda expression that returns something of type IBasePage<T>
				var page = initialPage?.Invoke();
				return createNav(page);
			});

			Func<object> foundPageCreation = null;
			if (_pageCreation.TryGetValue(typeof(TNavPageModel), out foundPageCreation))
				_pageCreation[typeof(TNavPageModel)] = createNavWithPage;
			else
				_pageCreation.Add(typeof(TNavPageModel), createNavWithPage);
		}

		public virtual void RegisterMultiPage<TContainerPageModel, TFormsContainerPageType, TFormsSubPageType>(
			Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages,
			Func<TContainerPageModel> createMultModel = null,
			Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TContainerPageModel>> createMult = null)
			where TContainerPageModel : class, IBasePageModel
			where TFormsContainerPageType : MultiPage<TFormsSubPageType>, new()
			where TFormsSubPageType : Page
		{
			if (createMultModel != null)
			{
				Func<object> found = null;
				if (_pageModelCreation.TryGetValue(typeof(TContainerPageModel), out found))
					_pageModelCreation[typeof(TContainerPageModel)] = createMultModel;
				else
					_pageModelCreation.Add(typeof(TContainerPageModel), createMultModel);
			}

			if (createMult == null)
			{
				var pageModelType = typeof(TContainerPageModel);
				var pageType = GetPageType(pageModelType) ?? typeof(TFormsContainerPageType);
				_pageModelTypes[pageModelType] = pageType;

				if (createSubPages == null)
				{
					createMult = new Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TContainerPageModel>>(
						(pages) => XamvvmIoC.Resolve(pageType) as IBasePage<TContainerPageModel>);
				}
				else
				{
					createMult = new Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TContainerPageModel>>(
						(pages) =>
						{
							var multiPage = XamvvmIoC.Resolve(pageType) as IBasePage<TContainerPageModel>;
							var multiPageXam = (TabbedPage)multiPage;

							foreach (var page in pages)
							{
								multiPageXam.Children.Add((Page)page);
							}

							return multiPage;
						});
				}
			}

			var createMultWithPages = new Func<IBasePage<TContainerPageModel>>(() =>
			{
				var pages = createSubPages?.Invoke();
				return createMult(pages);
			});

			Func<object> foundPageCreation = null;
			if (_pageCreation.TryGetValue(typeof(TContainerPageModel), out foundPageCreation))
				_pageCreation[typeof(TContainerPageModel)] = createMultWithPages;
			else
				_pageCreation.Add(typeof(TContainerPageModel), createMultWithPages);
		}

		public virtual void RegisterTabbedPage<TTabbedPageModel>(IEnumerable<Type> subPageModelTypes, bool subPagesFromCache = true) where TTabbedPageModel : class, IBasePageModel
		{
			var createSubPages = new Func<IEnumerable<IBasePage<IBasePageModel>>>(
				() => subPageModelTypes.Select(t => subPagesFromCache ? GetPageFromCache(t) : GetPageAsNewInstance(t)));

			RegisterMultiPage<TTabbedPageModel, TabbedPage, Page>(createSubPages, null, null);
		}


		public virtual void RegisterTabbedPage<TTabbedPageModel>(Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages,
			Func<TTabbedPageModel> createTabModel = null,
			Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TTabbedPageModel>> createTabPage = null) where TTabbedPageModel : class, IBasePageModel
		{
			RegisterMultiPage<TTabbedPageModel, TabbedPage, Page>(createSubPages, createTabModel, createTabPage);
		}

		public virtual void RegisterCarouselPage<TPageModel>(IEnumerable<Type> subPageModelTypes, bool subPagesFromCache = true) where TPageModel : class, IBasePageModel
		{
			var createSubPages = new Func<IEnumerable<IBasePage<IBasePageModel>>>(
				() => subPageModelTypes.Select(t => subPagesFromCache ? GetPageFromCache(t) : GetPageAsNewInstance(t)));

			RegisterMultiPage<TPageModel, CarouselPage, ContentPage>(createSubPages, null, null);
		}

		public virtual void RegisterCarouselPage<TPageModel>(Func<IEnumerable<IBasePage<IBasePageModel>>> createSubPages, Func<TPageModel> createCarModel = null, Func<IEnumerable<IBasePage<IBasePageModel>>, IBasePage<TPageModel>> createCar = null) where TPageModel : class, IBasePageModel
		{
			RegisterMultiPage<TPageModel, CarouselPage, ContentPage>(createSubPages, createCarModel, createCar);
		}

		public virtual void RegisterFlyout<TPageModel, TFlyoutPageModel, TDetailPageModel>(bool masterFromCache = true, bool detailFromCache = true)
			where TPageModel : class, IBasePageModel where TFlyoutPageModel : class, IBasePageModel where TDetailPageModel : class, IBasePageModel
		{
			RegisterFlyout<TPageModel>(
				() => masterFromCache ? GetPageFromCache<TFlyoutPageModel>() : GetPageAsNewInstance<TFlyoutPageModel>(),
				() => detailFromCache ? GetPageFromCache<TDetailPageModel>() : GetPageAsNewInstance<TDetailPageModel>());
		}

		public virtual void RegisterFlyout<TPageModel>(Func<IBasePage<IBasePageModel>> createMasterPage, Func<IBasePage<IBasePageModel>> createDetailPage, Func<TPageModel> createMasDetModel = null, Func<IBasePage<IBasePageModel>, IBasePage<IBasePageModel>, IBasePage<TPageModel>> createFlyoutPage = null) where TPageModel : class, IBasePageModel
		{
			if (createMasDetModel != null)
			{
				Func<object> found = null;
				if (_pageModelCreation.TryGetValue(typeof(TPageModel), out found))
					_pageModelCreation[typeof(TPageModel)] = createMasDetModel;
				else
					_pageModelCreation.Add(typeof(TPageModel), createMasDetModel);
			}

			if (createFlyoutPage == null)
			{
				var pageModelType = typeof(TPageModel);
				var pageType = GetPageType(pageModelType) ?? typeof(BaseFlyoutPage<TPageModel>);
				_pageModelTypes[pageModelType] = pageType;

				createFlyoutPage = new Func<IBasePage<IBasePageModel>, IBasePage<IBasePageModel>, IBasePage<TPageModel>>(
					(master, detail) =>
					{
						var masdetPage = XamvvmIoC.Resolve(pageType) as IBasePage<TPageModel>;
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
				return createFlyoutPage(masterPage, detailPage);
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

		internal Type GetViewType(Type viewModelType)
		{
			Type viewType = null;
			if (_viewModelTypes.TryGetValue(viewModelType, out viewType))
				return viewType;

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