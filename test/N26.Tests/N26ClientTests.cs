using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace N26.Tests
{
    [TestFixture, Category("Integration Tests")]
    public class N26ClientTests
    {
        public N26Client N26Client { get; private set; }

        [OneTimeSetUp]
        public async Task Initialize()
        {
            var userName = TestContext.Parameters.Get("userName");
            var password = TestContext.Parameters.Get("password");
            Assume.That(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password), "Properties \"userName\" and/or \"password\" are missing.");
            N26Client = await N26Client.LoginAsync(userName, password);
            N26Client.Should().NotBeNull();
            N26Client.Token.Should().NotBeNull();
        }

        [Test]
        public async Task GetAccountsShouldNotBeNull()
        {
            var sut = await N26Client.GetAccountsAsync();
            sut.Should().NotBeNull();
        }

        [Test]
        public async Task GetAddressesShouldNotBeNullAndNotContainNulls()
        {
            var sut = await N26Client.GetAddressesAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        [Test]
        public async Task GetCardsShouldNotBeNullAndNotContainNulls()
        {
            var sut = await N26Client.GetCardsAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        [Test]
        public async Task GetMeShouldNotBeNull()
        {
            var sut = await N26Client.GetMeAsync();
            sut.Should().NotBeNull();
        }

        [Test]
        public async Task GetTransactionsShouldNotBeNullAndNotContainNulls()
        {
            var sut = await N26Client.GetTransactionsAsync();
            sut.Should().NotBeNull().And.NotContainNulls();
        }

        //[Test]
        //public void TestQueryable()
        //{
        //    var sut = N26.Transactions.Where(t => t.Amount > 100).ToList();
        //    sut.Should().NotBeNull().And.NotContainNulls();
        //}
    }
}
