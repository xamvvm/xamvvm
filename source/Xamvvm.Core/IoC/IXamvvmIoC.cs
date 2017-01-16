using System;

namespace Xamvvm
{
	public interface IXamvvmIoC
	{
		void RegisterMultiInstance<RegisterType, RegisterImplementation>() where RegisterType : class where RegisterImplementation : class, RegisterType;

		void RegisterMultiInstance(Type registerType, Type registerImplementation);

		void RegisterSingleton<RegisterType, RegisterImplementation>() where RegisterType : class where RegisterImplementation : class, RegisterType;

		void RegisterSingleton(Type registerType, Type registerImplementation);

		void RegisterSingleton<RegisterType>(RegisterType instance) where RegisterType : class;

		TResolveType Resolve<TResolveType>() where TResolveType : class;

		object Resolve(Type resolveType);
	}
}
