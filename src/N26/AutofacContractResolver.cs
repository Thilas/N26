using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using N26.Utilities;
using Newtonsoft.Json.Serialization;

namespace N26
{
    internal class AutofacContractResolver : DefaultContractResolver
    {
        private readonly IContainer _container;

        public AutofacContractResolver(IContainer container)
        {
            Guard.IsNotNull(container, nameof(container));
            _container = container;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            // use Autofac to create types that have been registered with it
            return _container.IsRegistered(objectType) ? CreateAutofacObjectContract(objectType) : base.CreateObjectContract(objectType);
        }

        private JsonObjectContract CreateAutofacObjectContract(Type objectType)
        {
            // attempt to create the contract from the resolved type
            if (_container.ComponentRegistry.TryGetRegistration(new TypedService(objectType), out var registration))
            {
                objectType = (registration.Activator as ReflectionActivator)?.LimitType ?? objectType;
            }

            var contract = base.CreateObjectContract(objectType);
            contract.DefaultCreator = () => _container.Resolve(objectType);
            return contract;
        }

        protected override JsonProperty CreatePropertyFromConstructorParameter(JsonProperty matchingMemberProperty, ParameterInfo parameterInfo)
        {
            var property = base.CreatePropertyFromConstructorParameter(matchingMemberProperty, parameterInfo);

            // use Autofac to create default values that have been registered with it
            if (property.DefaultValue == null && _container.IsRegistered(property.PropertyType))
            {
                SetAutofacPropertyDefaultValue(property);
            }

            return property;
        }

        private void SetAutofacPropertyDefaultValue(JsonProperty property)
        {
            // attempt to set the default value from the resolved type only if it has a shared instance
            if (_container.ComponentRegistry.TryGetRegistration(new TypedService(property.PropertyType), out var registration) &&
                registration.Sharing == InstanceSharing.Shared)
            {
                property.DefaultValue = _container.Resolve(property.PropertyType);
            }
        }
    }
}
