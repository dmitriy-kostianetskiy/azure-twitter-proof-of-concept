using System;
using AzureTwitter.MessageBus.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace AzureTwitter.RedisMessageBus
{
	public class RedisMessageBus : IMessageBus
	{
		private readonly ConnectionMultiplexer _connection;
		private readonly JsonSerializerSettings _serializerSettings;
		private readonly ISubscriber _subscriber;

		private readonly string _host;
		private readonly string _pipe;


		public RedisMessageBus(string host, string pipe)
		{
			_pipe = pipe;
			_host = host;

			_connection = ConnectionMultiplexer.Connect(_host);
			_subscriber = _connection.GetSubscriber();
			_serializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
		}
        
	    public void Send<T>(T message)
	    {
	        _subscriber.Publish(_pipe, JsonConvert.SerializeObject(message, _serializerSettings));
        }

	    public void Subscribe<T>(Action<T> onMessage)
	    {
	        _subscriber.Subscribe(_pipe, (channel, value) =>
	        {
	            var message = JsonConvert.DeserializeObject<T>(value, _serializerSettings);
	            onMessage(message);
	        });
        }
	}
}
