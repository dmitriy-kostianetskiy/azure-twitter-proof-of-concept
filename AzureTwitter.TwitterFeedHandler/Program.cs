using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using AzureTwitter.TwitterFeedHandler.Providers;
using AzureTwitter.TwitterFeedHandler.Settings;
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

				//need some sort of IoC here
				var settings = new ServiceSettings();
				var provider = new TweetsProvider(settings);
				var messageBus = new RedisMessageBus.RedisMessageBus(settings.RedisHost, settings.RedisPipeName);

				ServiceRuntime.RegisterServiceAsync("TwitterFeedHandlerType",
					context => new TwitterFeedHandler(context, provider, messageBus, settings)).GetAwaiter().GetResult();

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
