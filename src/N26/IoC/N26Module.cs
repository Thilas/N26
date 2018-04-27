using Autofac;
using N26.Connection;
using N26.IoC;
using N26.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace N26
{
    internal sealed class N26Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var iocConstructorFinder = new IoCConstructorFinder();

            builder.RegisterType<N26Client>()
                .As<IClient>()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(iocConstructorFinder);

            #region Connection

            builder.RegisterType<N26Connection>()
                .As<IConnection>()
                .InstancePerLifetimeScope();
            builder.RegisterType<N26HttpClient>()
                .As<IHttpClient>();

            #endregion

            #region Json

            builder.RegisterType<N26SerializerSettingsFactory>()
                .As<ISerializerSettingsFactory>();
            builder.RegisterType<AutofacContractResolver>()
                .As<IContractResolver>();
            builder.RegisterType<N26DateTimeConverter>()
                .As<JsonConverter>();
            builder.RegisterType<N26TimeSpanConverter>()
                .As<JsonConverter>();
            builder.RegisterType<StringFlagsEnumConverter>()
                .As<JsonConverter>();

            #endregion
        }
    }
}
