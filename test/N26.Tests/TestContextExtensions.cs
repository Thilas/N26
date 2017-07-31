using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace N26.Tests
{
    public static class TestContextExtensions
    {
        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetPropertyValue(this TestContext testContext, string key)
            => GetPropertyValue(testContext, key, value => value?.ToString());

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="testContext">The test context.</param>
        /// <param name="key">The key.</param>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// testContext
        /// or
        /// key
        /// or
        /// selector
        /// </exception>
        public static T GetPropertyValue<T>(this TestContext testContext, string key, Func<object, T> selector)
        {
            if (testContext == null) throw new ArgumentNullException(nameof(testContext));
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (!testContext.Properties.TryGetValue(key, out var value)) value = null;
            return selector(value);
        }
    }
}
