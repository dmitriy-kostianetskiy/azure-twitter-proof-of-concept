using Autofac;
using AzureTwitter.Configuration;
using AzureTwitter.Configuration.Dependencies;
using AzureTwitter.TwitterFeedHandler.Interfaces;
using AzureTwitter.TwitterFeedHandler.Providers;
using AzureTwitter.MessageBus.Interfaces;

namespace AzureTwitter.TwitterFeedHandler.Dependencies
{
    public class TwitterFeedHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Modules registration
            builder.RegisterModule<ConfigurationModule>();

            // Types registration
            builder.RegisterType<TweetsProvider>().As<ITweetsProvider>();
            builder.Register(c => new RedisMessageBus.RedisMessageBus(
                                c.Resolve<ISettingsManager>().MessageBusConnection, 
                                c.Resolve<ISettingsManager>().MessageBusChannel))
                .As<IMessageBus>();

        }
    }
}
