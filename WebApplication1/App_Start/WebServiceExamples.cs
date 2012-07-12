using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace WebApplication1
{
	public class HelloRequest
	{
		public string Name { get; set; }
	}

	public class HelloResponse : IHasResponseStatus
	{
		public string Result { get; set; }
		public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
	}

    public class HelloService : ServiceBase<HelloRequest>
	{
        private readonly ICacheClient _cacheClient;

        public HelloService(ICacheClient cacheClient)
        {
            _cacheClient = cacheClient;
        }

        protected override object Run(HelloRequest request)
		{
            return base.RequestContext.ToOptimizedResultUsingCache(
                _cacheClient, "urn:helloworld", () => new HelloResponse { Result = "HelloRequest, " + request.Name });

			return new HelloResponse { Result = "HelloRequest, " + request.Name };
		}
	}
}
