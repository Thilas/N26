using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace N26.Tests
{
    [TestClass, TestCategory("Integration Tests")]
    public class N26ClientTests
    {
        public static N26Client N26Client { get; private set; }

        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var userName = testContext.GetPropertyValue("userName");
            var password = testContext.GetPropertyValue("password");
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                Assert.Inconclusive("Properties \"userName\" and/or \"password\" are missing.");
            }
            N26Client = await N26Client.LoginAsync(userName, password);
            N26Client.Should().NotBeNull();
            N26Client.Token.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetAccountsShouldNotBeNull()
        {
            var sut = await N26Client.GetAccountsAsync();
            sut.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetAddressesShouldNotBeNullAndNotContainNulls()
        {
            var sut = await N26Client.GetAddressesAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        [TestMethod]
        public async Task GetCardsShouldNotBeNullAndNotContainNulls()
        {
            var sut = await N26Client.GetCardsAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        [TestMethod]
        public async Task GetMeShouldNotBeNull()
        {
            var sut = await N26Client.GetMeAsync();
            sut.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetTransactionsShouldNotBeNullAndNotContainNulls()
        {
            var sut = await N26Client.GetTransactionsAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        //[TestMethod]
        //public void TestQueryable()
        //{
        //    var sut = N26.Transactions.Where(t => t.Amount > 100).ToList();
        //    sut.Should().NotBeNull().And.NotContainNulls();
        //}
    }
}
