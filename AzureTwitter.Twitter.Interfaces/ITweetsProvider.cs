using System.Threading.Tasks;
using AzureTwitter.Models;

namespace AzureTwitter.Twitter.Interfaces
{
	public interface ITweetsProvider
	{
		Task<TweetModel> GetLatestAsync(string userName);
	}
}