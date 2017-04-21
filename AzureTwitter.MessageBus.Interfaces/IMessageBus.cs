using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTwitter.MessageBus.Interfaces
{
    public interface IMessageBus
    {
        void Send<T>(T message);
        void Subscribe<T>(Action<T> onMessage);
    }
}
