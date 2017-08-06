using System;
using System.Diagnostics;
using System.Reflection;
using JetBrains.Annotations;

namespace N26.Utilities
{
    /// <summary>
    /// Common guards for argument validation.
    /// </summary>
    [DebuggerStepThrough]
    internal static class Guard
    {
        /// <summary>
        /// Ensures <typeparamref name="TFrom" /> is assignable to <typeparamref name="TTo" />.
        /// </summary>
        /// <typeparam name="TTo"></typeparam>
        /// <typeparam name="TFrom"></typeparam>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Parameter must be assignable to <typeparamref name="TTo" />.</exception>
        public static void IsAssignableTo<TTo, TFrom>([InvokerParameterName] string paramName)
            => IsAssignableTo<TTo>(typeof(TFrom), paramName);

        /// <summary>
        /// Ensures <paramref name="type" /> is assignable to <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Parameter must be assignable to <typeparamref name="T" />.</exception>
        public static void IsAssignableTo<T>(Type type, [InvokerParameterName] string paramName)
        {
            if (!typeof(T).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Parameter must be assignable to {typeof(T)}.", paramName);
            }
        }

        /// <summary>
        /// Ensures <typeparamref name="T" /> is assignable from <paramref name="value" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Parameter must be assignable from <typeparamref name="T" />.</exception>
        public static void IsAssignableTo<T>(object value, [InvokerParameterName] string paramName)
        {
            if (!(value is T))
            {
                throw new ArgumentException($"Parameter must be assignable to {typeof(T)}.", paramName);
            }
        }

        /// <summary>
        /// Ensures <paramref name="value" /> is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Parameter cannot be null.</exception>
        public static void IsNotNull<T>(T value, [InvokerParameterName] string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, "Parameter cannot be null.");
            }
        }

        /// <summary>
        /// Ensures <paramref name="value" /> is not null or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Parameter cannot be empty.</exception>
        /// <exception cref="ArgumentNullException">Parameter cannot be null.</exception>
        public static void IsNotNullOrEmpty(string value, [InvokerParameterName] string paramName)
        {
            IsNotNull(value, paramName);
            if (value.Length == 0)
            {
                throw new ArgumentException("Parameter cannot be empty.", paramName);
            }
        }
    }
}
