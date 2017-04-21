using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzureTwitter.Configuration;
using AzureTwitter.MessageBus.Interfaces;
using AzureTwitter.TwitterFeedHandler.Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace AzureTwitter.TwitterFeedHandler
{
	/// <summary>
	/// An instance of this class is created for each service instance by the Service Fabric runtime.
	/// </summary>
	internal sealed class TwitterFeedHandler : StatelessService
	{
		private readonly ITweetsProvider _provider;
		private readonly ISettingsManager _settingsManager;
		private readonly IMessageBus _messageBus;

		public TwitterFeedHandler(StatelessServiceContext context, 
			ITweetsProvider provider,
		    IMessageBus messageBus,
			ISettingsManager settingsManager)
			: base(context)
		{
			_provider = provider;
			_messageBus = messageBus;
			_settingsManager = settingsManager;
		}

		/// <summary>
		/// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
		/// </summary>
		/// <returns>A collection of listeners.</returns>
		protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
		{
			return new ServiceInstanceListener[0];
		}

		/// <summary>
		/// This is the main entry point for your service instance.
		/// </summary>
		/// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
		protected override async Task RunAsync(CancellationToken cancellationToken)
		{
			while (true)
			{
				cancellationToken.ThrowIfCancellationRequested();
				var handlers = _settingsManager.TwitterFeedUsers.Select(user => _provider.GetLatestAsync(user)).ToList();
                
				await Task.WhenAll(handlers)
					.ContinueWith((task) =>
					{
						foreach (var tweet in task.Result)
						{
						    if (tweet == null)
                                continue;

						    ServiceEventSource.Current.ServiceMessage(Context, "{0} - {1}", tweet.Content, tweet.User);
						    _messageBus.Send(tweet);
						}
					}, cancellationToken);

				await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
			}
		}
	}
}
