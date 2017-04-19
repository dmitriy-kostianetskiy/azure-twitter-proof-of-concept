namespace AzureTwitter.RedisMessageBus.Interfaces
{
	public interface IRedisMessageBus
	{
		void Send(object message);
	}
}