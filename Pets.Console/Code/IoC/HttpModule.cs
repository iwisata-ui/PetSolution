using Autofac;
using System.Net.Http;

namespace Pets.Console.Code.IoC
{
	public class HttpModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register(c => new HttpClient(new HttpClientHandler()))
				.As<HttpClient>()
				.PropertiesAutowired()
				.InstancePerLifetimeScope();
		}
	}
}
