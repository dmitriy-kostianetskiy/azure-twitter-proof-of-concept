using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.WebApi;
using AzureTwitter.Storage.Interfaces.Repositories;
using AzureTwitter.Storage.Repositories;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace AzureTwitter.ApiService.Dependencies
{
    internal static class ContainerManager
    {
        public static IContainer BuildContainer(HubConfiguration hubConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ApiServiceModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<TweetsRepository>().As<ITweetsRepository>();
            builder.Register(i => hubConfiguration.Resolver.Resolve<IConnectionManager>()).As<IConnectionManager>().ExternallyOwned();

            return builder.Build();
        }
    }
}
