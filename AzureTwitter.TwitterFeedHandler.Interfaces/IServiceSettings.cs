using System.Collections.Generic;

namespace AzureTwitter.TwitterFeedHandler.Interfaces
{
	public interface IServiceSettings
	{
		string AccessToken { get; }
		string AccessTokenSecret { get; }
		string ConsumerKey { get; }
		string ConsumerSecret { get; }

		IEnumerable<string> Users { get; }

		string RedisHost { get; }
		string RedisPipeName { get; }
	}
}