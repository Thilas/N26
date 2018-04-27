using System;
using System.Collections.Generic;
using N26.Utilities;
using NUnit.Framework;

namespace N26.Tests.Utilities
{
    [TestFixture, Category("Utilities")]
    public class DateTimeUtilsTests
    {
        private static IEnumerable<DateTime> GetDateTimeValues()
        {
            yield return DateTimeUtils.N26ReferenceDateTime;
            yield return new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        private static IEnumerable<long> GetN26DateTimeValues()
        {
            yield return 0L;
            yield return 946684800000L;
        }

        [Test, Sequential]
        public void ToN26DateTimeReturnsExpectedResult(
            [ValueSource(nameof(GetDateTimeValues))] DateTime dateTime,
            [ValueSource(nameof(GetN26DateTimeValues))] long expected)
        {
            var actual = DateTimeUtils.ToN26DateTime(dateTime);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test, Sequential]
        public void FromN26DateTimeReturnsExpectedResult(
            [ValueSource(nameof(GetN26DateTimeValues))] long n26DateTime,
            [ValueSource(nameof(GetDateTimeValues))] DateTime expected)
        {
            var actual = DateTimeUtils.FromN26DateTime(n26DateTime);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
