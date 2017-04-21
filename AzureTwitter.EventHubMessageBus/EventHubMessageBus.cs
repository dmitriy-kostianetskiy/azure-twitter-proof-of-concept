using System;
using AzureTwitter.MessageBus.Interfaces;

namespace AzureTwitter.EventHubMessageBus
{
    public class EventHubMessageBus : IMessageBus
    {
        public EventHubMessageBus()
        {
            
        }

        public void Send<T>(T message)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Action<T> onMessage)
        {
            throw new NotImplementedException();
        }
    }
}
