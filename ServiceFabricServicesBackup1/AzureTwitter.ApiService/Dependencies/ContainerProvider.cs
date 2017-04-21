using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using AzureTwitter.Storage.Interfaces.Repositories;
using AzureTwitter.Storage.Repositories;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace AzureTwitter.ApiService.Dependencies
{
	public static class ContainerProvider
	{
		public static IContainer GetContainer(HubConfiguration hubConfiguration)
		{
			var builder = new ContainerBuilder();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			builder.RegisterType<TweetsRepository>().As<ITweetsRepository>();

			//signalR
			builder.Register(i => hubConfiguration.Resolver.Resolve<IConnectionManager>()).As<IConnectionManager>().ExternallyOwned();

			return builder.Build();
		}
	}
}
