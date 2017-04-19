using Microsoft.AspNetCore.SignalR;
using AzureTwitter.Api.RedisMessageBus;
using AzureTwitter.Storage.Models;

namespace AzureTwitter.Api.Hubs
{
    public class TweetHub: Hub
    {
	    public TweetHub()
	    {
		    var  messageBus = new RedisMessageBus.RedisMessageBus("localhost", "feed");
		    messageBus.Subscribe<TweetModel>(x =>
		    {
			    Clients.All.newTweet(x);
		    });

		}
    }
}
