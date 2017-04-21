using System;
using System.Collections.Generic;
using System.Configuration;
using AzureTwitter.TwitterFeedHandler.Interfaces;

namespace AzureTwitter.TwitterFeedHandler.Settings
{
	public class ServiceSettings : IServiceSettings
	{

		public ServiceSettings()
		{
			Users = ConfigurationManager.AppSettings["users"].Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);

			ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
			ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
			AccessToken = ConfigurationManager.AppSettings["accessToken"];
			AccessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"];

			RedisHost = ConfigurationManager.AppSettings["redisHost"];
			RedisPipeName = ConfigurationManager.AppSettings["redisPipeName"];
		}

		public string AccessTokenSecret { get; }
		public string AccessToken { get; }
		public string ConsumerSecret { get; }
		public string ConsumerKey { get; }

		public IEnumerable<string> Users { get; }

		public string RedisHost { get; }
		public string RedisPipeName { get; }
	}
}
