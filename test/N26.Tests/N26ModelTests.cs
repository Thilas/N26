using System;
using System.Collections.Generic;
using Moq;
using N26.Models;
using NUnit.Framework;

namespace N26.Tests
{
    [TestFixture, Category("Models")]
    public class N26ModelTests
    {
        private static IEnumerable<object[]> GetEqualityTestCases()
        {
            var client = Mock.Of<IClient>();
            var model1 = new TestModel(client, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
            var model2 = new TestModel(client, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2));
            yield return new object[] { null, null, true };
            yield return new object[] { model1, null, false };
            yield return new object[] { null, model1, false };
            yield return new object[] { model1, model1, true };
            yield return new object[] { model1, model2, false };
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void StaticEqualsReturnsExpectedResult(TestModel left, TestModel right, bool result)
        {
            if (result)
            {
                Assert.That(left, Is.EqualTo(right));
            }
            else
            {
                Assert.That(left, Is.Not.EqualTo(right));
            }
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void EqualsReturnsExpectedResult(TestModel left, TestModel right, bool result)
        {
            if (left != null)
            {
                if (result)
                {
                    Assert.That(left, Is.EqualTo(right));
                }
                else
                {
                    Assert.That(left, Is.Not.EqualTo(right));
                }
            }
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void EqualityOperatorReturnsExpectedResult(TestModel left, TestModel right, bool result)
        {
            if (result)
            {
                Assert.That(left, Is.EqualTo(right));
            }
            else
            {
                Assert.That(left, Is.Not.EqualTo(right));
            }
        }

        [Test, TestCaseSource(nameof(GetEqualityTestCases))]
        public void InequalityOperatorReturnsExpectedResult(TestModel left, TestModel right, bool result)
        {
            if (result)
            {
                Assert.That(left, Is.EqualTo(right));
            }
            else
            {
                Assert.That(left, Is.Not.EqualTo(right));
            }
        }

        public class TestModel : N26Model<TestModel>
        {
            public TestModel(IClient client, Guid id)
                : base(client, id)
            {
            }
        }
    }
}
