using System.Linq;
using System.Threading.Tasks;
using N26.Models;
using NUnit.Framework;

namespace N26.Tests
{
    [TestFixture, Category("Integration Tests")]
    public class IntegrationTests
    {
        public IClient Client { get; private set; }

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var userName = TestContext.Parameters.Get("userName");
            var password = TestContext.Parameters.Get("password");
            Assume.That(userName, Is.Not.Null.And.Not.Empty, "Property \"userName\" is missing.");
            Assume.That(password, Is.Not.Null.And.Not.Empty, "Property \"password\" is missing.");
            Client = await N26Client.LoginAsync(userName, password);
            Assert.That(Client, Is.Not.Null.And.TypeOf<N26Client>());
            Assert.That(Client.Token, Is.Not.Null.And.TypeOf<Token>());
        }

        [Test]
        public async Task GetAccountsReturnsANotNullAccountsInstance()
        {
            var sut = await Client.GetAccountsAsync();
            Assert.That(sut, Is.Not.Null.And.TypeOf<Accounts>());
        }

        [Test]
        public async Task GetAddressesReturnsANotNullIEnumerableInstanceOfNotNullAddressInstances()
        {
            var sut = await Client.GetAddressesAsync();
            Assert.That(sut, Is.Not.Null.And.All.Not.Null.And.All.TypeOf<Address>());
        }

        [Test]
        public async Task GetCardsShouldNotBeNullAndNotContainNulls()
        {
            var sut = await Client.GetCardsAsync();
            Assert.That(sut, Is.Not.Null.And.All.Not.Null.And.All.TypeOf<Card>());
        }

        [Test]
        public async Task GetMeShouldNotBeNull()
        {
            var sut = await Client.GetMeAsync();
            Assert.That(sut, Is.Not.Null.And.TypeOf<Me>());
        }

        [Test]
        public async Task GetTransactionsShouldNotBeNullAndNotContainNulls()
        {
            var sut = await Client.GetTransactionsAsync();
            Assert.That(sut, Is.Not.Null.And.All.Not.Null.And.All.TypeOf<Transaction>());
        }

        [Test]
        public void TestQueryable()
        {
            //var sut = N26Client.Transactions.Where(t => t.Amount > 100).ToList();
            var sut = Client.Transactions.ToList();
            Assert.That(sut, Is.Not.Null.And.All.Not.Null.And.All.TypeOf<Transaction>());
        }
    }
}
