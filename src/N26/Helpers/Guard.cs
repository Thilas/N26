using System;
using System.Diagnostics;
using System.Reflection;

namespace N26.Helpers
{
    /// <summary>
    /// Common guards for argument validation.
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// Ensures <typeparamref name="T" /> is assignable from <paramref name="type" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Parameter must be assignable from <typeparamref name="T" />.</exception>
        public static void IsAssignableTo<T>(Type type, string paramName)
        {
            if (!typeof(T).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Parameter must be assignable from {typeof(T)}.");
            }
        }

        /// <summary>
        /// Ensures <typeparamref name="T" /> is assignable from <paramref name="value" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentException">Parameter must be assignable from <typeparamref name="T" />.</exception>
        public static void IsAssignableTo<T>(object value, string paramName)
        {
            if (!(value is T))
            {
                throw new ArgumentException($"Parameter must be assignable from {typeof(T)}.");
            }
        }

        /// <summary>
        /// Ensures <paramref name="value" /> is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Parameter cannot be null.</exception>
        public static void IsNotNull<T>(T value, string paramName)
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
        public static void IsNotNullNorEmpty(string value, string paramName)
        {
            IsNotNull(value, paramName);
            if (value.Length == 0)
            {
                throw new ArgumentException("Parameter cannot be empty.", paramName);
            }
        }

        /// <summary>
        /// Ensures <paramref name="value" /> is valid according to the <paramref name="validate" /> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="validate">The validate function.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="ArgumentException"><paramref name="message" /></exception>
        public static void IsValid<T>(T value, string paramName, Func<T, bool> validate, string message)
        {
            if (!validate(value))
            {
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// Ensures <paramref name="value" /> is valid according to the <paramref name="validate" /> function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="validate">The validate function.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentException"><paramref name="message" /></exception>
        public static void IsValid<T>(T value, string paramName, Func<T, bool> validate, string message, params object[] args)
        {
            if (!validate(value))
            {
                throw new ArgumentException(string.Format(message, args), paramName);
            }
        }
    }
}
