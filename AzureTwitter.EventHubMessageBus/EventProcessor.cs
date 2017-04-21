using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace AzureTwitter.EventHubMessageBus
{
    public class EventProcessor : IEventProcessor
    {
        private readonly Action<string> _onMessage;

        public EventProcessor(Action<string> onMessage)
        {
            _onMessage = onMessage;
        }

        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var eventData in messages)
            {
                var data = Encoding.UTF8.GetString(eventData.GetBytes());
                _onMessage(data);
            }

            return context.CheckpointAsync();
        }
    }
}
