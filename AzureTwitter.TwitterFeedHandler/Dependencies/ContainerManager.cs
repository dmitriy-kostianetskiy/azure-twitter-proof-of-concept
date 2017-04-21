using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AzureTwitter.TwitterFeedHandler.Dependencies
{
    internal static class ContainerManager
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TwitterFeedHandlerModule>();

            return builder.Build();
        }
    }
}
