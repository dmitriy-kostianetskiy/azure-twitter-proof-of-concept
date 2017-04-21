using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTwitter.Configuration
{
    public class SettingsManager : ISettingsManager
    {
        public string MessageBusConnection { get; private set; }
        public string MessageBusChannel { get; private set; }
        public string CacheServiceConnection { get; private set; }
        public string TwitterFeedConsumerKey { get; private set; }
        public string TwitterFeedConsumerSecret { get; private set; }
        public string TwitterFeedAccessToken { get; private set; }
        public string TwitterFeedAccessTokenSecret { get; private set; }
        public IEnumerable<string> TwitterFeedUsers { get; private set; }

        public SettingsManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            TwitterFeedUsers = ConfigurationManager.AppSettings["TwitterFeedUsers"]?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            TwitterFeedConsumerKey = ConfigurationManager.AppSettings["TwitterFeedConsumerKey"];
            TwitterFeedConsumerSecret = ConfigurationManager.AppSettings["TwitterFeedConsumerSecret"];
            TwitterFeedAccessToken = ConfigurationManager.AppSettings["TwitterFeedAccessToken"];
            TwitterFeedAccessTokenSecret = ConfigurationManager.AppSettings["TwitterFeedAccessTokenSecret"];

            MessageBusConnection = ConfigurationManager.AppSettings["MessageBusConnection"];
            MessageBusChannel = ConfigurationManager.AppSettings["MessageBusChannel"];

            CacheServiceConnection = ConfigurationManager.AppSettings["CacheServiceConnection"];
        }
    }
}
