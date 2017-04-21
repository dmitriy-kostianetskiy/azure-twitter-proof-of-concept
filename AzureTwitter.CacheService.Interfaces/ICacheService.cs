using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTwitter.CacheService.Interfaces
{
    public interface ICacheService
    {
        void Set<T>(string key, T data);
        T Get<T>(string key);
        T GetSet<T>(string key, T data);
    }
}
