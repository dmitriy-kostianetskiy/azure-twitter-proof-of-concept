using System.Threading.Tasks;
using AzureTwitter.Models;

namespace AzureTwitter.TwitterFeedHandler.Interfaces
{
	public interface ITweetsProvider
	{
		Task<TweetModel> GetLatestAsync(string userName);
	}
}