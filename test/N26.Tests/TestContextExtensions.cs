using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using N26.Helpers;

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
        public static T GetPropertyValue<T>(this TestContext testContext, string key, Func<object, T> selector)
        {
            Guard.IsNotNull(testContext, nameof(testContext));
            Guard.IsNotNullOrEmpty(key, nameof(key));
            Guard.IsNotNull(selector, nameof(selector));
            if (!testContext.Properties.TryGetValue(key, out var value)) value = null;
            return selector(value);
        }
    }
}
