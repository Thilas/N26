using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using JetBrains.Annotations;

namespace N26.Utilities
{
    [DebuggerStepThrough]
    internal static class ReflectionExtensions
    {
        //[NotNull]
        //public static Assembly Assembly([NotNull] this Type type)
        //{
        //    Guard.IsNotNull(type, nameof(type));
        //    var result = type.GetTypeInfo().Assembly;
        //    return result;
        //}

        [NotNull, ItemNotNull]
        public static IEnumerable<ConstructorInfo> DeclaredConstructors([NotNull] this Type type)
        {
            Guard.IsNotNull(type, nameof(type));
            var result = type.GetTypeInfo().DeclaredConstructors;
            return result;
        }

        //[CanBeNull]
        //public static T GetCustomAttribute<T>([NotNull] this Type type)
        //    where T : Attribute
        //{
        //    Guard.IsNotNull(type, nameof(type));
        //    var result = type.GetTypeInfo().GetCustomAttribute<T>();
        //    return result;
        //}

        [NotNull]
        public static T GetRequiredCustomAttribute<T>([NotNull] this Type type)
            where T : Attribute
        {
            Guard.IsNotNull(type, nameof(type));
            var result = type.GetCustomAttribute<T>();
            return result ?? throw new InvalidOperationException($"Type \"{type.Name}\" does not have a \"{typeof(T).Name}\" attribute.");
        }

        //public static bool IsGenericType([NotNull] this Type type)
        //{
        //    Guard.IsNotNull(type, nameof(type));
        //    var result = type.GetTypeInfo().IsGenericType;
        //    return result;
        //}

        [NotNull]
        public static Type[] GenericTypeParameters([NotNull] this Type type)
        {
            Guard.IsNotNull(type, nameof(type));
            var result = type.GetTypeInfo().GenericTypeParameters;
            return result;
        }

        public static bool IsNullableType([NotNull] this Type type)
        {
            Guard.IsNotNull(type, nameof(type));
            return !type.IsValueType || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
