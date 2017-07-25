using System;

namespace Xamvvm
{
	public static class XamvvmIoC
	{
		public static IXamvvmIoC Instance { get; set; } = new SimpleXamvvmIoC();

		public static void RegisterMultiInstance<RegisterType, RegisterImplementation>() where RegisterType : class where RegisterImplementation : class, RegisterType
		{
			Instance.RegisterMultiInstance<RegisterType, RegisterImplementation>();
		}

		public static void RegisterMultiInstance(Type registerType, Type registerImplementation)
		{
			Instance.RegisterMultiInstance(registerType, registerImplementation);
		}

		public static void RegisterSingleton<RegisterType, RegisterImplementation>() where RegisterType : class where RegisterImplementation : class, RegisterType
		{
			Instance.RegisterSingleton<RegisterType, RegisterImplementation>();
		}

		public static void RegisterSingleton(Type registerType, Type registerImplementation)
		{
			Instance.RegisterSingleton(registerType, registerImplementation);
		}

		public static void RegisterSingleton<RegisterType>(RegisterType instance) where RegisterType : class
		{
			Instance.RegisterSingleton(instance);
		}

		public static TResolveType Resolve<TResolveType>() where TResolveType : class
		{
			return Instance.Resolve<TResolveType>();
		}

		public static object Resolve(Type resolveType)
		{
			return Instance.Resolve(resolveType);
		}
	}
}
