using System.Collections.Generic;
using System.Linq;
using AzureTwitter.Models;
using AzureTwitter.Storage.Interfaces.Repositories;

namespace AzureTwitter.Storage.Repositories
{
    public class TweetsRepository: ITweetsRepository
    {
		private readonly TweetModel[] _tweets = {

				new TweetModel
				{
					Id = "1",
					Content = "Test",
					User = "Putin"
				},
				new TweetModel
				{
					Id = "2",
					Content = "Test 1",
					User = "Trump"
				},
		};

		public IEnumerable<TweetModel> Get()
		{
			return _tweets;
		}

		public TweetModel Get(string id)
		{
			return _tweets.FirstOrDefault(x => x.Id == id);
		}
    }
}
