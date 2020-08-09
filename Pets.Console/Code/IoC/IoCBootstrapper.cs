using Autofac;

namespace Pets.Console.Code.IoC
{
	public class IoCBootstrapper
	{
		public static IContainer Bootstrap()
		{
			var container = BuildContainer(new ContainerBuilder()).Build();
			return container;
		}

		public static ContainerBuilder BuildContainer(ContainerBuilder builder = null)
		{
			builder = builder ?? new ContainerBuilder();
			builder.RegisterModule<HttpModule>();
			builder.RegisterModule<ServicesModule>();
			return builder;
		}
	}
}
