using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace N26.Tests
{
    [TestClass, TestCategory("Integration Tests")]
    public class N26Testsa
    {
        public static N26 N26 { get; private set; }

        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var userName = testContext.GetPropertyValue("userName");
            var password = testContext.GetPropertyValue("password");
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                Assert.Inconclusive("Properties \"userName\" and/or \"password\" are missing.");
            }
            N26 = await N26.LoginAsync(userName, password);
            N26.Should().NotBeNull();
            N26.Token.Should().NotBeNull();
        }

        [TestMethod]
        public async Task TestAccounts()
        {
            var sut = await N26.GetAccountsAsync();
            sut.Should().NotBeNull();
        }

        [TestMethod]
        public async Task TestAddresses()
        {
            var sut = await N26.GetAddressesAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        [TestMethod]
        public async Task TestCards()
        {
            var sut = await N26.GetCardsAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        [TestMethod]
        public async Task TestMe()
        {
            var sut = await N26.GetMeAsync();
            sut.Should().NotBeNull();
        }

        [TestMethod]
        public async Task TestTransactions()
        {
            var sut = await N26.GetTransactionsAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        [TestMethod]
        public void TestQueryable()
        {
            var sut = N26.Transactions.Where(t => t.Amount > 100).ToList();
            sut.Should().NotBeNull().And.NotContainNulls();
        }
    }
}
