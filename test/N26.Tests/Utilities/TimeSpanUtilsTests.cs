using System;
using System.Collections.Generic;
using N26.Utilities;
using NUnit.Framework;

namespace N26.Tests.Utilities
{
    [TestFixture, Category("Utilities")]
    public class TimeSpanUtilsTests
    {
        private static IEnumerable<TimeSpan> GetTimeSpanValues()
        {
            yield return TimeSpan.Zero;
            yield return TimeSpan.FromDays(1D);
        }

        private static IEnumerable<long> GetN26TimeSpanValues()
        {
            yield return 0L;
            yield return 86400L;
        }

        [Test, Sequential]
        public void ToN26TimeSpanReturnsExpectedResult(
            [ValueSource(nameof(GetTimeSpanValues))] TimeSpan timeSpan,
            [ValueSource(nameof(GetN26TimeSpanValues))] long expected)
        {
            var actual = TimeSpanUtils.ToN26TimeSpan(timeSpan);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test, Sequential]
        public void FromN26TimeSpanReturnsExpectedResult(
            [ValueSource(nameof(GetN26TimeSpanValues))] long n26TimeSpan,
            [ValueSource(nameof(GetTimeSpanValues))] TimeSpan expected)
        {
            var actual = TimeSpanUtils.FromN26TimeSpan(n26TimeSpan);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
