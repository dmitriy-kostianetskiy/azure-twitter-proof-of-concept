using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace AzureTwitter.CommunicationUtils.Dependencies
{
    public class CommunicationUtilsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IDataSerializer>().As<DataSerializer>();
        }
    }
}
