using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using AzureTwitter.Configuration;
using AzureTwitter.MessageBus.Interfaces;
using AzureTwitter.TwitterFeedHandler.Dependencies;
using AzureTwitter.TwitterFeedHandler.Interfaces;
using AzureTwitter.TwitterFeedHandler.Providers;
using Microsoft.ServiceFabric.Services.Runtime;

namespace AzureTwitter.TwitterFeedHandler
{
	internal static class Program
	{
		/// <summary>
		/// This is the entry point of the service host process.
		/// </summary>
		private static void Main()
		{
			try
			{
				// The ServiceManifest.XML file defines one or more service type names.
				// Registering a service maps a service type name to a .NET type.
				// When Service Fabric creates an instance of this service type,
				// an instance of the class is created in this host process.

			    var container = ContainerManager.BuildContainer();

				ServiceRuntime.RegisterServiceAsync("TwitterFeedHandlerType",
					context => new TwitterFeedHandler(context, 
                        container.Resolve<ITweetsProvider>(),
					    container.Resolve<IMessageBus>(),
					    container.Resolve<ISettingsManager>())).GetAwaiter().GetResult();

				ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(TwitterFeedHandler).Name);

				// Prevents this host process from terminating so services keep running.
				Thread.Sleep(Timeout.Infinite);
			}
			catch (Exception e)
			{
				ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
				throw;
			}
		}
	}
}
