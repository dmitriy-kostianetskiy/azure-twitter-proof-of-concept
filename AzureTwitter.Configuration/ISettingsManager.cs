using System.Collections.Generic;

namespace AzureTwitter.Configuration
{
    public interface ISettingsManager
    {
        // Message Bus Configuration
        string MessageBusConnection { get; }
        string MessageBusChannel { get; }

        // Cache Service Configuration
        string CacheServiceConnection { get; }

        // Twitter Feed Configuration
        string TwitterFeedConsumerKey { get; }
        string TwitterFeedConsumerSecret { get; }
        string TwitterFeedAccessToken { get; }
        string TwitterFeedAccessTokenSecret { get; }
        IEnumerable<string> TwitterFeedUsers { get; }
    }
}