using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using N26.Utilities;

namespace N26.Models
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class N26ModelAttribute : Attribute
    {
        [NotNull]
        internal static string GetRelativeUri<T>() => Get<T>().RelativeUri;

        [NotNull]
        internal static Type GetContainer<T>()
        {
            var type = typeof(T);
            var container = Get<T>().Container;
            return container?.MakeGenericType(type) ?? type;
        }

        private static N26ModelAttribute Get<T>() => typeof(T).GetRequiredCustomAttribute<N26ModelAttribute>();

        [NotNull]
        public string RelativeUri { get; }

        [CanBeNull]
        public Type Container { get; }

        public N26ModelAttribute([NotNull] string relativeUri)
        {
            Guard.IsNotNullOrEmpty(relativeUri, nameof(relativeUri));
            RelativeUri = relativeUri;
        }

        public N26ModelAttribute([NotNull] string relativeUri, [NotNull] Type container)
            : this(relativeUri)
        {
            Guard.IsNotNull(container, nameof(container));
            if (!container.IsGenericType)
            {
                throw new ArgumentException("Parameter must be a generic type.", nameof(container));
            }
            if (container.GenericTypeParameters().Length != 1)
            {
                throw new ArgumentException("Parameter must have a single generic type parameter.", nameof(container));
            }
            var type = container.MakeGenericType(typeof(object));
            Guard.IsAssignableTo<IEnumerable<object>>(type, nameof(container));
            Container = container;
        }
    }
}
