using System;

namespace Xamvvm
{
	internal class SimpleXamvvmIoC : IXamvvmIoC
	{
		public void RegisterMultiInstance<TRegisterType, TRegisterImplementation>() where TRegisterType : class where TRegisterImplementation : class, TRegisterType
		{
			TinyIoC.TinyIoCContainer.Current.Register<TRegisterType, TRegisterImplementation>().AsMultiInstance();
		}

		public void RegisterMultiInstance(Type registerType, Type registerImplementation)
		{
			TinyIoC.TinyIoCContainer.Current.Register(registerType, registerImplementation).AsMultiInstance();
		}

		public void RegisterSingleton<TRegisterType, TRegisterImplementation>() where TRegisterType : class where TRegisterImplementation : class, TRegisterType
		{
			TinyIoC.TinyIoCContainer.Current.Register<TRegisterType, TRegisterImplementation>().AsSingleton();
		}

		public void RegisterSingleton(Type registerType, Type registerImplementation)
		{
			TinyIoC.TinyIoCContainer.Current.Register(registerType, registerImplementation).AsSingleton();
		}

		public void RegisterSingleton<TRegisterType>(TRegisterType instance) where TRegisterType : class
		{
			TinyIoC.TinyIoCContainer.Current.Register(instance);
		}

		public TResolveType Resolve<TResolveType>() where TResolveType : class
		{
			return TinyIoC.TinyIoCContainer.Current.Resolve<TResolveType>();
		}

		public object Resolve(Type resolveType)
		{
			return TinyIoC.TinyIoCContainer.Current.Resolve(resolveType);
		}
	}
}
