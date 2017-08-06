using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using N26.Models;

namespace N26.Tests
{
    [TestClass, TestCategory("Models")]
    public class N26ModelTests
    {
        //private IEnumerable<N26Model> Get()
        //{
        //    var n26Client = new N26Client();
        //    yield return null;
        //    yield return new N26Model(n26Client, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
        //    yield return new N26Model(n26Client, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2));
        //}

        [DataTestMethod]
        [DataRow(null, null, true)]
        public void N26ModelEqualsShouldReturnExpectedResult(N26Model left, N26Model right, bool expected)
        {
            var sut = N26Model.Equals(left, right);
            sut.Should().Be(expected);
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
