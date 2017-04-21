using Autofac;
using AzureTwitter.Configuration;
using AzureTwitter.Configuration.Dependencies;
using AzureTwitter.MessageBus.Interfaces;


namespace AzureTwitter.ApiService.Dependencies
{
    internal class ApiServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ConfigurationModule>();
            builder.Register(c => new RedisMessageBus.RedisMessageBus(
                    c.Resolve<ISettingsManager>().MessageBusConnection,
                    c.Resolve<ISettingsManager>().MessageBusChannel))
                .As<IMessageBus>();
        }
    }
}
