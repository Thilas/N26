using System;
using System.Collections.Generic;
using FluentAssertions;
using N26.Models;
using NUnit.Framework;

namespace N26.Tests
{
    [TestFixture, Category("Models")]
    public class N26ModelTests
    {
        private static IEnumerable<object[]> GetEqualityTestCases()
        {
            var n26Client = new N26Client();
            var model1 = new N26Model(n26Client, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
            var model2 = new N26Model(n26Client, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2));
            yield return new object[] { null, null, true };
            yield return new object[] { model1, null, false };
            yield return new object[] { null, model1, false };
            yield return new object[] { model1, model1, true };
            yield return new object[] { model1, model2, false };
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void StaticEqualsShouldReturnExpectedResult(N26Model left, N26Model right, bool expected)
        {
            var sut = N26Model.Equals(left, right);
            sut.Should().Be(expected);
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void EqualsShouldReturnExpectedResult(N26Model left, N26Model right, bool expected)
        {
            if (left != null)
            {
                var sut = left.Equals(right);
                sut.Should().Be(expected);
            }
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void EqualityOperatorShouldReturnExpectedResult(N26Model left, N26Model right, bool expected)
        {
            var sut = left == right;
            sut.Should().Be(expected);
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void InequalityOperatorShouldReturnExpectedResult(N26Model left, N26Model right, bool expected)
        {
            var sut = left != right;
            sut.Should().Be(!expected);
        }

        public class N26Client : IN26Client
        {
        }

        public class N26Model : N26Model<N26Model>
        {
            public N26Model(IN26Client n26Client, Guid id)
                : base(n26Client, id)
            {
            }
        }
    }
}
