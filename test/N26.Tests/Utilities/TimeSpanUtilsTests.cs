using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using N26.Utilities;

namespace N26.Tests.Utilities
{
    [TestClass, TestCategory("Utilities")]
    public class TimeSpanUtilsTests
    {
        public static readonly TimeSpan TestTimeSpan = TimeSpan.FromDays(1D);
        public static readonly long TestN26TimeSpan = 86400L;

        [TestMethod]
        public void ZeroToN26TimeSpanShouldReturnZero()
        {
            var expected = 0L;
            var actual = TimeSpanUtils.ToN26TimeSpan(TimeSpan.Zero);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToN26TimeSpanShouldReturnExpectedResult()
        {
            var expected = TestN26TimeSpan;
            var actual = TimeSpanUtils.ToN26TimeSpan(TestTimeSpan);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ZeroFromN26TimeSpanShouldReturnZero()
        {
            var expected = TimeSpan.Zero;
            var actual = TimeSpanUtils.FromN26TimeSpan(0L);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FromN26TimeSpanShouldReturnExpectedResult()
        {
            var expected = TestTimeSpan;
            var actual = TimeSpanUtils.FromN26TimeSpan(TestN26TimeSpan);
            actual.Should().Be(expected);
        }
    }
}
