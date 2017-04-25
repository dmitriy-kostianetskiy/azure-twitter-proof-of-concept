using System;
using System.Threading;
using System.Threading.Tasks;
using AzureTwitter.Models;

namespace AzureTwitter.TwitterFeedHandler.Interfaces
{
	public interface ITweetsProvider
	{
		Task Subscribe(Action<TweetModel> handler, CancellationToken cancellationToken);
	}
}