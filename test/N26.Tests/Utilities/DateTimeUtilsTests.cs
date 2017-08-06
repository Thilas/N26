using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using N26.Utilities;

namespace N26.Tests.Utilities
{
    [TestClass, TestCategory("Utilities")]
    public class DateTimeUtilsTests
    {
        public static readonly DateTime TestDateTime = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly long TestN26DateTime = 946684800000L;

        [TestMethod]
        public void N26ReferenceDateTimeToN26DateTimeShouldReturnZero()
        {
            var expected = 0L;
            var actual = DateTimeUtils.ToN26DateTime(DateTimeUtils.N26ReferenceDateTime);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToN26DateTimeShouldReturnExpectedResult()
        {
            var expected = TestN26DateTime;
            var actual = DateTimeUtils.ToN26DateTime(TestDateTime);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ZeroFromN26DateTimeShouldReturnN26ReferenceDateTime()
        {
            var expected = DateTimeUtils.N26ReferenceDateTime;
            var actual = DateTimeUtils.FromN26DateTime(0L);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FromN26DateTimeShouldReturnExpectedResult()
        {
            var expected = TestDateTime;
            var actual = DateTimeUtils.FromN26DateTime(TestN26DateTime);
            actual.Should().Be(expected);
        }
    }
}
