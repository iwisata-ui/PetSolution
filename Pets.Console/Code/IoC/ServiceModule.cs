using Autofac;
using Pets.Backend;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace Pets.Console.Code.IoC
{
	public class ServicesModule : Module
	{
		public static Assembly BackendAssembly => Assembly.GetAssembly(typeof(IBackendAssembly));

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(BackendAssembly)
				.Where(t => t.Name.EndsWith("Service") && t.GetInterfaces().Any(i => i.Name.EndsWith("Service")))
				.As(type => type.GetInterfaces().Single(repoInterface => repoInterface.Name.EndsWith("Service")))
				.InstancePerLifetimeScope()
				.PropertiesAutowired();
		}
	}
}
