using System.Web.Http;
using System.Web.Http.Cors;
using Autofac.Integration.WebApi;
using AzureTwitter.ApiService.Dependencies;
using AzureTwitter.ApiService.Hubs;
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
			var config = new HttpConfiguration();
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

			var container = ContainerProvider.GetContainer(hubConfiguration);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;


			appBuilder.UseCors(CorsOptions.AllowAll);
			appBuilder.UseWebApi(config);
			appBuilder.RunSignalR(hubConfiguration);

			var messageBus = new RedisMessageBus.RedisMessageBus("localhost", "feed");
			messageBus.Subscribe<TweetModel>(x =>
			{
				var conenction = hubConfiguration.Resolver.Resolve<IConnectionManager>();
				conenction.GetHubContext<TweetHub>().Clients.All.newTweet(x);
			});

			//messageBus.Send(new TweetModel());
		}
	}
}
