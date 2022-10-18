using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Xamvvm
{
	public partial class XamvvmMockFactory : IBaseFactory
	{
		readonly Dictionary<Tuple<Type, string>, object> _pageCache = new Dictionary<Tuple<Type, string>, object>();
		readonly ConditionalWeakTable<IBasePageModel, object> _weakPageCache = new ConditionalWeakTable<IBasePageModel, object>();
		readonly Dictionary<Type, Func<object>> _pageCreation = new Dictionary<Type, Func<object>>();
		readonly Dictionary<Type, Func<object>> _pageModelCreation = new Dictionary<Type, Func<object>>();

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

		internal Type GetPageModelType(IBasePage<IBasePageModel> page)
		{
			var found = page.GetType().GetTypeInfo().ImplementedInterfaces.FirstOrDefault(t => t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IBasePage<>));
			var viewModelType = found.GenericTypeArguments.First();
			return viewModelType;
		}
	}
}
