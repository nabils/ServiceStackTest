using Castle.Windsor;
using ServiceStack.Configuration;

namespace WebApplication1.App_Start
{
    internal sealed class WindsorContainerAdapter : IContainerAdapter
    {
        private readonly IWindsorContainer _container;

        public WindsorContainerAdapter(IWindsorContainer container)
        {
            this._container = container;
        }

        public T TryResolve<T>()
        {
            // http://groups.google.com/group/servicestack/browse_thread/thread/cf210359e243b9d5
            return !this._container.Kernel.HasComponent(typeof(T)) ? default(T) : Resolve<T>();
        }

        public T Resolve<T>()
        {
            return this._container.Resolve<T>();
        }
    }
}