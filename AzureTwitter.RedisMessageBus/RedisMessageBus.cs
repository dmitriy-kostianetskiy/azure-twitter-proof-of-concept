using AzureTwitter.RedisMessageBus.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AzureTwitter.RedisMessageBus
{
	public class RedisMessageBus : IRedisMessageBus
	{
		private readonly ConnectionMultiplexer _connection;
		private readonly ISubscriber _subscriber;

		private readonly string _host;
		private readonly string _pipe;


		public RedisMessageBus(string host, string pipe)
		{
			_pipe = pipe;
			_host = host;

			_connection = ConnectionMultiplexer.Connect(_host);
			_subscriber = _connection.GetSubscriber();
		}

		public void Send(object message)
		{
			_subscriber.Publish(_pipe, JsonConvert.SerializeObject(message));
		}
	}
}
