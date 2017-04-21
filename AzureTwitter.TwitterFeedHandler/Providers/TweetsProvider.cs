using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AzureTwitter.Models;
using AzureTwitter.TwitterFeedHandler.Interfaces;
using LinqToTwitter;

namespace AzureTwitter.TwitterFeedHandler.Providers
{
	public class TweetsProvider : ITweetsProvider
	{
		private readonly TwitterContext _context;

		public TweetsProvider(IServiceSettings settings)
		{
			var auth = new SingleUserAuthorizer
			{
				CredentialStore = new SingleUserInMemoryCredentialStore
				{
					ConsumerKey = settings.ConsumerKey,
					ConsumerSecret = settings.ConsumerSecret,
					AccessToken = settings.AccessToken,
					AccessTokenSecret = settings.AccessTokenSecret
				}
			};

			_context = new TwitterContext(auth);
		}

		public async Task<TweetModel> GetLatestAsync(string userName)
		{
			var query = _context.Status
				.Where(x => x.Type == StatusType.User && x.ScreenName == userName && x.Count == 1);

			var tweet = await query.SingleOrDefaultAsync();
			if (tweet == null)
			{
				return null;
			}

			return new TweetModel
			{
				Id = tweet.ID.ToString(CultureInfo.InvariantCulture),
				User = tweet.User.Name,
				Content = tweet.Text,
				CreatedAt = tweet.CreatedAt
			};
		}
	}
}

