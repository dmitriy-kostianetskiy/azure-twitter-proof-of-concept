using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AzureTwitter.Models;
using AzureTwitter.Twitter.Interfaces;
using LinqToTwitter;

namespace AzureTwitter.Twitter
{
	public class TweetsProvider : ITweetsProvider
	{
		private readonly TwitterContext _context;

		public TweetsProvider()
		{
			var auth = new SingleUserAuthorizer
			{
				CredentialStore = new SingleUserInMemoryCredentialStore
				{
					ConsumerKey = ConfigurationManager.AppSettings["consumerKey"],
					ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"],
					AccessToken = ConfigurationManager.AppSettings["accessToken"],
					AccessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]
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
				Content = tweet.Text
			};
		}
	}
}

