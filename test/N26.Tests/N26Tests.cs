using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace N26.Tests
{
    [TestClass]
    public class N26Tests
    {
        public static N26 N26 { get; private set; }

        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var userName = testContext.GetPropertyValue("userName");
            var password = testContext.GetPropertyValue("password");
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
            N26 = await N26.LoginAsync(userName, password);
            N26.Should().NotBeNull();
            N26.Token.Should().NotBeNull();
            N26.Token.AccessToken.Should().NotBeEmpty();
            N26.Token.RefreshToken.Should().NotBeEmpty();
            N26.Token.ExpiresIn.Should().BeGreaterThan(TimeSpan.Zero);
        }

        [TestMethod]
        public async Task TestAccounts()
        {
            var sut = await N26.GetAccountsAsync();
            sut.Should().NotBeNull();
            sut.Iban.Should().NotBeNullOrEmpty();
            sut.Bic.Should().NotBeNullOrEmpty();
            sut.BankName.Should().NotBeNullOrEmpty();
            sut.Id.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task TestAddresses()
        {
            var sut = await N26.GetAddressesAsync();
            sut.Should().NotBeNull();
            foreach (var item in sut)
            {
                item.StreetName.Should().NotBeNullOrEmpty();
                item.HouseNumberBlock.Should().NotBeNullOrEmpty();
                item.ZipCode.Should().NotBeNullOrEmpty();
                item.CityName.Should().NotBeNullOrEmpty();
                item.CountryName.Should().NotBeNullOrEmpty();
                item.Id.Should().NotBeEmpty();
            }
        }

        [TestMethod]
        public async Task TestCards()
        {
            var sut = await N26.GetCardsAsync();
            sut.Should().NotBeNull();
            foreach (var item in sut)
            {
                item.Id.Should().NotBeEmpty();
                item.MaskedPan.Should().NotBeNullOrEmpty();
                item.UserNameOnCard.Should().NotBeNullOrEmpty();
            }
        }

        [TestMethod]
        public async Task TestMe()
        {
            var sut = await N26.GetMeAsync();
            sut.Should().NotBeNull();
            sut.Id.Should().NotBeEmpty();
            sut.Email.Should().NotBeNullOrEmpty();
            sut.FirstName.Should().NotBeNullOrEmpty();
            sut.LastName.Should().NotBeNullOrEmpty();
            sut.KycFirstName.Should().NotBeNullOrEmpty();
            sut.KycLastName.Should().NotBeNullOrEmpty();
            sut.Nationality.Should().NotBeNullOrEmpty();
            sut.MobilePhoneNumber.Should().NotBeNullOrEmpty();
            sut.ShadowUserId.Should().NotBeEmpty();
            sut.IdNowToken.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task TestTransactions()
        {
            var sut = await N26.GetTransactionsAsync();
            sut.Should().NotBeNull();
            sut.Should().NotBeEmpty();
            foreach (var item in sut)
            {
                item.Id.Should().NotBeEmpty();
                item.UserId.Should().NotBeEmpty();
                item.CurrencyCode.Should().NotBeNullOrEmpty();
                item.AccountId.Should().NotBeEmpty();
                item.Category.Should().NotBeNullOrEmpty();
                item.SmartLinkId.Should().NotBeEmpty();
                item.LinkId.Should().NotBeEmpty();
            }
        }

        [TestMethod]
        public void TestQueryable()
        {
            var transactionsQuery = N26.Transactions;
            var transactionsResult = transactionsQuery.Where(t => t.Amount > 100).ToList();
        }
    }
}
