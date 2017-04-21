using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using AzureTwitter.ApiService.Dependencies;
using AzureTwitter.ApiService.Hubs;
using AzureTwitter.MessageBus.Interfaces;
using AzureTwitter.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace AzureTwitter.ApiService
{
	public static class Startup
	{
		// This code configures Web API. The Startup class is specified as a type
		// parameter in the WebApp.Start method.
		public static void ConfigureApp(IAppBuilder appBuilder)
		{
		    var hubConfiguration = GetSignalRHubConfig();
		    var container = ContainerManager.BuildContainer(hubConfiguration);
		    var config = GetWebApiConfig(container);
            
			appBuilder.UseCors(CorsOptions.AllowAll);
			appBuilder.UseWebApi(config);
			appBuilder.RunSignalR(hubConfiguration);

		    var messageBus = container.Resolve<IMessageBus>();
			messageBus.Subscribe<TweetModel>(x =>
			{
				var connection = hubConfiguration.Resolver.Resolve<IConnectionManager>();
				connection.GetHubContext<TweetHub>().Clients.All.newTweet(x);
			});
		}

	    private static HttpConfiguration GetWebApiConfig(IContainer container)
	    {
	        var config = new HttpConfiguration();

	        config.Routes.MapHttpRoute(
	            name: "DefaultApi",
	            routeTemplate: "api/{controller}/{id}",
	            defaults: new { id = RouteParameter.Optional }
	        );

	        config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
	        config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
	        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
	        config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

	        return config;
	    }

	    private static HubConfiguration GetSignalRHubConfig()
	    {
	        var hubConfiguration = new HubConfiguration
	        {
	            EnableJSONP = true,
	            EnableDetailedErrors = true,
	            EnableJavaScriptProxies = true
	        };

	        var settings = new JsonSerializerSettings
	        {
	            ContractResolver = new SignalRContractResolver()
	        };

	        var serializer = JsonSerializer.Create(settings);

	        hubConfiguration.Resolver.Register(typeof(JsonSerializer), () => serializer);

	        return hubConfiguration;
	    }
	}
}
