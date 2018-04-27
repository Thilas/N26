using System;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using N26.Connection;

namespace N26
{
    internal static class Bootstrapper
    {
        private static readonly Lazy<IContainer> _container = new Lazy<IContainer>(() =>
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Bootstrapper).Assembly);
            return builder.Build();
        });

        [NotNull]
        public static TService Resolve<TService>()
        {
            return _container.Value.Resolve<TService>();
        }

        [NotNull]
        public static async Task<IClient> LoginAsync([NotNull] Uri apiBaseUri, [NotNull] string userName, [NotNull] string password)
        {
            var scope = BeginClientLifetimeScope();
            var connection = scope.Resolve<IConnection>();
            await connection.LoginAsync(apiBaseUri, userName, password);
            var result = scope.Resolve<IClient>();
            return result;
        }

        [NotNull]
        public static ILifetimeScope BeginClientLifetimeScope([CanBeNull] Action<ContainerBuilder> configurationAction = null)
        {
            var result = configurationAction == null ?
                _container.Value.BeginLifetimeScope(typeof(IClient)) :
                _container.Value.BeginLifetimeScope(typeof(IClient), configurationAction);
            return result;
        }
    }
}
