using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzureTwitter.Configuration;
using AzureTwitter.Models;
using AzureTwitter.TwitterFeedHandler.Interfaces;
using LinqToTwitter;

namespace AzureTwitter.TwitterFeedHandler.Providers
{
	public class TweetsProvider : ITweetsProvider
	{
		private readonly TwitterContext _context;

		public TweetsProvider(ISettingsManager settings)
		{
			var auth = new SingleUserAuthorizer
			{
				CredentialStore = new SingleUserInMemoryCredentialStore
				{
					ConsumerKey = settings.TwitterFeedConsumerKey,
					ConsumerSecret = settings.TwitterFeedConsumerSecret,
					AccessToken = settings.TwitterFeedAccessToken,
					AccessTokenSecret = settings.TwitterFeedAccessTokenSecret
				}
			};

			_context = new TwitterContext(auth);
		}

		public Task Subscribe(Action<TweetModel> handler, CancellationToken cancellationToken)
		{
			var query = _context.Streaming.Where(x => x.Type == StreamingType.User)
				.StartAsync(async stream =>
				{
					await Task.Run(() =>
					{
						if (cancellationToken.IsCancellationRequested)
						{
							stream.CloseStream();
							return;
						}

						if (stream.EntityType != StreamEntityType.Status)
							return;

						var tweet = stream.Entity as Status;
						if (tweet != null)
						{
							var tweetModel = new TweetModel
							{
								Id = tweet.ID.ToString(CultureInfo.InvariantCulture),
								User = tweet.User.Name,
								Content = tweet.Text,
								Created = tweet.CreatedAt
							};

							handler(tweetModel);
						}

					}, cancellationToken);
				});

			return query;
		}
	}
}

