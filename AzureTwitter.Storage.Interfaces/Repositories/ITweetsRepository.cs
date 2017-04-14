using System.Collections.Generic;
using AzureTwitter.Storage.Models;

namespace AzureTwitter.Storage.Interfaces.Repositories
{
    public interface ITweetsRepository
    {
        IEnumerable<TweetModel> Get();
        TweetModel Get(string id);
    }
}
