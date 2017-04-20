using System.Collections.Generic;
using AzureTwitter.Models;

namespace AzureTwitter.Storage.Interfaces.Repositories
{
    public interface ITweetsRepository
    {
        IEnumerable<TweetModel> Get();
        TweetModel Get(string id);
    }
}
