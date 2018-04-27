using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using JetBrains.Annotations;
using N26.Utilities;
using Newtonsoft.Json.Serialization;

namespace N26.Json
{
    internal sealed class AutofacContractResolver : DefaultContractResolver
    {
        private readonly ILifetimeScope _scope;

        public AutofacContractResolver([NotNull] ILifetimeScope scope)
        {
            Guard.IsNotNull(scope, nameof(scope));
            _scope = scope;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            // use Autofac to create types that have been registered with it
            return _scope.IsRegistered(objectType) ? CreateAutofacObjectContract(objectType) : base.CreateObjectContract(objectType);
        }

        private JsonObjectContract CreateAutofacObjectContract(Type objectType)
        {
            // attempt to create the contract from the resolved type
            if (_scope.ComponentRegistry.TryGetRegistration(new TypedService(objectType), out var registration))
            {
                objectType = (registration.Activator as ReflectionActivator)?.LimitType ?? objectType;
            }

            var contract = base.CreateObjectContract(objectType);
            contract.DefaultCreator = () => _scope.Resolve(objectType);
            return contract;
        }

        protected override JsonProperty CreatePropertyFromConstructorParameter(JsonProperty matchingMemberProperty, ParameterInfo parameterInfo)
        {
            var property = base.CreatePropertyFromConstructorParameter(matchingMemberProperty, parameterInfo);

            // use Autofac to create default values that have been registered with it
            if (property.DefaultValue == null && _scope.IsRegistered(property.PropertyType))
            {
                SetAutofacPropertyDefaultValue(property);
            }

            return property;
        }

        private void SetAutofacPropertyDefaultValue(JsonProperty property)
        {
            // attempt to set the default value from the resolved type only if it has a shared instance
            if (_scope.ComponentRegistry.TryGetRegistration(new TypedService(property.PropertyType), out var registration) &&
                registration.Sharing == InstanceSharing.Shared)
            {
                property.DefaultValue = _scope.Resolve(property.PropertyType);
            }
        }
    }
}
