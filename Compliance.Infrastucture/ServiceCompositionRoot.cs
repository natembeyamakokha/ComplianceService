using Autofac;

namespace Compliance.Infrastructure
{
    public static class ServiceCompositionRoot
    {
        private static IContainer _container;
        
        public static void Set(IContainer container)
        {
            _container = container;
        }

        public static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }
    }
}