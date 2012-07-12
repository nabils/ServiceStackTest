using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.WebHost.Endpoints;

[assembly: WebActivator.PreApplicationStartMethod(typeof(WebApplication1.App_Start.AppHost), "Start")]


/**
 * Entire ServiceStack Starter Template configured with a 'HelloRequest' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace WebApplication1.App_Start
{
	public class AppHost
		: AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("StarterTemplate ASP.NET Host", typeof(HelloService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
            // To hook in windsor as default container
            var windsorContainer = new WindsorContainer();
            Container.Adapter = new WindsorContainerAdapter(windsorContainer);

			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
		
			//Configure User Defined REST Paths
		    Routes
		        .Add<HelloRequest>("/hello")
		        .Add<HelloRequest>("/hello/{Name*}");

			//Uncomment to change the default ServiceStack configuration
			SetConfig(new EndpointHostConfig {
			    DebugMode = true //Show StackTraces when developing
                //EnableFeatures = Feature.Json | Feature.Metadata
			});

			//Register all your dependencies
            //Using an in-memory cache
		    windsorContainer.Register(Component.For<ICacheClient>().ImplementedBy<MemoryCacheClient>());

            this.RequestFilters.Add((request, response, requestDto) =>
                {
                    var x = 1;
                });

            this.ResponseFilters.Add((request, response, responseDto) =>
                {
                    var x = 1;
                });
		}

		public static void Start()
		{
			new AppHost().Init();
		}

	}
}
