using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Moq;
using N26.Connection;
using N26.Json;
using N26.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace N26.Tests
{
    [TestFixture, Category("Connection")]
    public class N26ConnectionTests
    {
        private static Uri ApiBaseUri { get; } = new Uri("uri://uri");

        private static IClient Client { get; } = Mock.Of<IClient>();

        private Func<T, TResult> ToFunc<T, TResult>(Func<T, TResult> func) => func;

        private Task<Token> GetTokenAsync(object response)
        {
            return GetResultAsync(response,
                httpResponse => Mock.Of<IHttpClient>(hc =>
                    hc.PostAsync(It.Is<Uri>(uri => ApiBaseUri.IsBaseOf(uri)), It.IsAny<IEnumerable<KeyValuePair<string, string>>>()) == Task.FromResult(httpResponse)
                ),
                async connection =>
                {
                    await connection.LoginAsync(ApiBaseUri, "userName", "password");
                    return connection.Token;
                });
        }

        private Task<TResult> GetResultAsync<T, TResult>(object response)
        {
            return GetResultAsync(response,
                httpResponse => Mock.Of<IHttpClient>(hc =>
                    hc.GetAsync(It.Is<Uri>(uri => ApiBaseUri.IsBaseOf(uri))) == Task.FromResult(httpResponse)
                ),
                async connection => await connection.GetAsync<T, TResult>(ApiBaseUri, null));
        }

        private Task<TResult> GetResultAsync<TResult>(
            object response,
            Func<string, IHttpClient> getHttpClient,
            Func<IConnection, Task<TResult>> getResultAsync)
        {
            var serializerSettingsFactory = Bootstrapper.Resolve<ISerializerSettingsFactory>();
            var httpResponse = JsonConvert.SerializeObject(response, serializerSettingsFactory.Create());
            var httpClient = getHttpClient(httpResponse);
            using (var scope = Bootstrapper.BeginClientLifetimeScope(builder =>
            {
                builder.RegisterInstance(Client).As<IClient>();
                builder.RegisterInstance(httpClient).As<IHttpClient>();

            }))
            {
                var connection = scope.Resolve<IConnection>();
                return getResultAsync(connection);
            }
        }

        [Test]
        public async Task GetTokenReturnsExpectedResult()
        {
            // arrange
            var expected = new Token(Guid.NewGuid(), TokenType.Bearer,
                Guid.NewGuid(), TimeSpan.FromMinutes(30),
                TokenScope.Read | TokenScope.Write | TokenScope.Trust);
            var toJson = ToFunc((Token t) => new
            {
                access_token = t.AccessToken,
                token_type = t.TokenType.ToString().ToLowerInvariant(),
                refresh_token = t.RefreshToken,
                expires_in = t.ExpiresIn,
                scope = t.Scope.ToString().Replace(", ", " ").ToLowerInvariant()
            });
            // act
            var sut = await GetTokenAsync(toJson(expected));
            // assert
            Assert.That(sut, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetAccountsReturnsExpectedResult()
        {
            // arrange
            var expected = new Accounts(Client, 1.2M, 2.3M, 3.4M, "Iban", "Bic", "Bank Name", true, Guid.NewGuid());
            var toJson = ToFunc((Accounts a) => new
            {
                id = a.Id,
                availableBalance = a.AvailableBalance,
                usableBalance = a.UsableBalance,
                bankBalance = a.BankBalance,
                iban = a.Iban,
                bic = a.Bic,
                bankName = a.BankName,
                seized = a.Seized
            });
            // act
            var sut = await GetResultAsync<Accounts, Accounts>(toJson(expected));
            // assert
            Assert.That(sut, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetAddressesReturnsExpectedResult()
        {
            // arrange
            var addressTypes = Enum.GetValues(typeof(AddressType)).Cast<AddressType>();
            var expected = new Collection<Address>(
                new CollectionPaging(addressTypes.Count()),
                addressTypes.Select(addressType => new Address(
                    Client, $"Address Line 1 {addressType}", $"Street Name {addressType}",
                    $"House Number Block {addressType}", $"Zip Code {addressType}", $"City Name {addressType}",
                    $"Country Name {addressType}", addressType, Guid.NewGuid(), Guid.NewGuid())).ToImmutableList());
            var toJson = ToFunc((Address a) => new
            {
                addressLine1 = a.AddressLine1,
                streetName = a.StreetName,
                houseNumberBlock = a.HouseNumberBlock,
                zipCode = a.ZipCode,
                cityName = a.CityName,
                countryName = a.CountryName,
                type = a.Type.ToString().ToUpperInvariant(),
                userId = a.UserId,
                id = a.Id
            });
            // act
            var sut = await GetResultAsync<Address, IEnumerable<Address>>(new
            {
                paging = new
                {
                    totalResults = expected.Paging.TotalResults
                },
                data = expected.Select(toJson).ToArray()
            });
            // assert
            Assert.That(sut, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetCardsReturnsExpectedResult()
        {
            // arrange
            var expected = new[] {
                new Card(Client, Guid.NewGuid(), null, "Pan", "Masked Pan", DateTime.UtcNow.Date.AddYears(1),
                CardType.MasterCard, CardStatus.Active, null, CardProductType.Black,
                DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddYears(-1), "User Name On Card",
                null, null, null, null, null, null, null, null, true, true, "Design", null, true)
            };
            var toJson = ToFunc((Card c) => new
            {
                id = c.Id,
                publicToken = c.PublicToken,
                pan = c.Pan,
                maskedPan = c.MaskedPan,
                expirationDate = c.ExpirationDate,
                cardType = c.CardType.ToString().ToUpperInvariant(),
                status = ((InternalCardStatus)c.Status).ToString(),
                cardProduct = ((InternalCardProduct?)c.CardProduct)?.ToString(),
                cardProductType = c.CardProductType.ToString().ToUpperInvariant(),
                pinDefined = c.PinDefined,
                cardActivated = c.CardActivated,
                usernameOnCard = c.UserNameOnCard,
                exceetExpressCardDelivery = c.ExceetExpressCardDelivery,
                membership = c.Membership,
                exceetActualDeliveryDate = c.ExceetActualDeliveryDate,
                exceetExpressCardDeliveryEmailSent = c.ExceetExpressCardDeliveryEmailSent,
                exceetCardStatus = c.ExceetCardStatus,
                exceetExpectedDeliveryDate = c.ExceetExpectedDeliveryDate,
                exceetExpressCardDeliveryTrackingId = c.ExceetExpressCardDeliveryTrackingId,
                cardSettingsId = c.CardSettingsId,
                applePayEligible = c.ApplePayEligible,
                googlePayEligible = c.GooglePayEligible,
                design = c.Design,
                orderId = c.OrderId,
                mptsCard = c.MptsCard
            });
            // act
            var sut = await GetResultAsync<Card, IEnumerable<Card>>(expected.Select(toJson).ToArray());
            // assert
            Assert.That(sut, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetMeReturnsExpectedResult()
        {
            // arrange
            var expected = new Me(Client, Guid.NewGuid(), "Email", "First Name", "Last Name",
                "KYC First Name", "KYC Last Name", "Title", Gender.Male, DateTime.UtcNow.Date,
                true, "Nationality", "Mobile Phone Number", Guid.NewGuid(), true);
            var toJson = ToFunc((Me m) => new
            {
                id = m.Id,
                email = m.Email,
                firstName = m.FirstName,
                lastName = m.LastName,
                kycFirstName = m.KycFirstName,
                kycLastName = m.KycLastName,
                title = m.Title,
                gender = m.Gender.ToString().ToUpperInvariant(),
                birthDate = m.BirthDate,
                signupCompleted = m.SignupCompleted,
                nationality = m.Nationality,
                mobilePhoneNumber = m.MobilePhoneNumber,
                shadowUserId = m.ShadowUserId,
                transferWiseTermsAccepted = m.TransferWiseTermsAccepted
            });
            // act
            var sut = await GetResultAsync<Me, Me>(toJson(expected));
            // assert
            Assert.That(sut, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetTransactionsReturnsExpectedResult()
        {
            // arrange
            var expected = new[]
            {
                new Transaction(Client, Guid.NewGuid(), Guid.NewGuid(), TransactionType.MasterCardPaymentPT,
                    1.2M, Currency.EUR, 2.3M, Currency.EUR, 3.4M, "Merchant City", DateTime.UtcNow,
                    1, 2, "Merchant Name", true,
                    "Partner Bic", "Partner Bcn", true, "Partner Bank Name", "Partner Name",
                    Guid.NewGuid(), "Partner Iban", "Partner Account Ban",
                    "Category", Guid.NewGuid(), "Reference Text",
                    DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2), true,
                    TransactionNature.Normal, "Reference To Original Operation",
                    Guid.NewGuid(), DateTime.UtcNow.AddHours(3), 3,
                    Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(4))
            };
            var toJson = ToFunc((Transaction t) => new
            {
                id = t.Id,
                userId = t.UserId,
                type = ((InternalTransactionType)t.Type).ToString(),
                amount = t.Amount,
                currencyCode = t.CurrencyCode.ToString(),
                originalAmount = t.OriginalAmount,
                originalCurrency = t.OriginalCurrency?.ToString(),
                exchangeRate = t.ExchangeRate,
                merchantCity = t.MerchantCity,
                visibleTS = t.VisibleTS,
                mcc = t.Mcc,
                mccGroup = t.MccGroup,
                merchantName = t.MerchantName,
                recurring = t.Recurring,
                partnerBic = t.PartnerBic,
                partnerBcn = t.PartnerBcn,
                partnerAccountIsSepa = t.PartnerAccountIsSepa,
                partnerBankName = t.PartnerBankName,
                partnerName = t.PartnerName,
                accountId = t.AccountId,
                partnerIban = t.PartnerIban,
                partnerAccountBan = t.PartnerAccountBan,
                category = t.Category,
                cardId = t.CardId,
                referenceText = t.ReferenceText,
                userAccepted = t.UserAccepted,
                userCertified = t.UserCertified,
                pending = t.Pending,
                transactionNature = t.TransactionNature.ToString().ToUpperInvariant(),
                referenceToOriginalOperation = t.ReferenceToOriginalOperation,
                smartContactId = t.SmartContactId,
                createdTS = t.CreatedTS,
                merchantCountry = t.MerchantCountry,
                smartLinkId = t.SmartLinkId,
                linkId = t.LinkId,
                confirmed = t.Confirmed
            });
            // act
            var sut = await GetResultAsync<Transaction, IEnumerable<Transaction>>(expected.Select(toJson).ToArray());
            // assert
            Assert.That(sut, Is.EqualTo(expected));
        }
    }
}
