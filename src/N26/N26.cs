using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using N26.Models;
using System.Net.Http.Headers;
using System.Text;

namespace N26
{
    public class N26
    {
        string bearer = "YW5kcm9pZDpzZWNyZXQ=";
        string api = "https://api.tech26.de";
        string N26token = string.Empty;

        public async Task Login(string username, string password)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", bearer);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });
            var result = await client.PostAsync("https://api.tech26.de/oauth/token", content);
            string str = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Token>(str);
            N26token = response.access_token;
        }

        public void transaction(string pin, string bic, string amount, string iban, string name, string referenceText)
        {
            //                //    var bearer = localStorage.getItem('N26token');
            //                //    var pin = $('#pin').val() || $('#pin').data('value') || $('#pin').text();
            //                //    var iban = $('#iban').val() || $('#iban').data('value') || $('#iban').text();
            //                //    var bic = $('#bic').val() || $('#bic').data('value') || $('#bic').text();
            //                //    var amount = $('#amount').val() || $('#amount').data('value') || $('#amount').text();
            //                //    var name = $('#name').val() || $('#name').data('value') || $('#name').text();
            //                //    var reference = $('#reference').val() || $('#reference').data('value') || $('#reference').text();

            //                //    amount = parseFloat(amount).toFixed(2);

            //                //$.ajax({
            //                //    type: 'POST',
            //                //    url: api + '/api/transactions',
            //                //    dataType: 'json',
            //                //    data: JSON.stringify({
            //                //            'pin': pin,
            //                //        'transaction': {
            //                //                "partnerBic": bic,
            //                //            "amount": amount,
            //                //            "type": "DT",
            //                //            "partnerIban": iban,
            //                //            "partnerName": name,
            //                //            "referenceText": reference
            //                //        }
            //                //        }),
            //                //    beforeSend: function(xhr) {
            //                //            xhr.setRequestHeader('Authorization', 'bearer ' + bearer);
            //                //            xhr.setRequestHeader('Accept', 'application/json');
            //                //            xhr.setRequestHeader('Content-Type', 'application/json');
            //                //        },
            //                //    success: function(data) {
            //                //            console.log(data);
            //                //        },
            //                //    error: function(data) {
            //                //            console.log(data);
            //                //        }
            //                //    });
        }

        public async Task<Me> Me()
        {
            return await Get<Me>("https://api.tech26.de/api/me");
        }

        public async Task<Accounts> GetAccounts()
        {
            return await Get<Accounts>("https://api.tech26.de/api/accounts");
        }

        public async Task<Transactions> GetTransactions()
        {
            return await Get<Transactions>("https://api.tech26.de/api/transactions");
        }

        public async Task<Cards> GetCards()
        {
            return await Get<Cards>("https://api.tech26.de/api/cards");
        }

        public async Task<Addresses> GetAddresses()
        {
            return await Get<Addresses>("https://api.tech26.de/api/addresses");
        }

        public async Task<T> Get<T>(string requestUri)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", N26token);

            var result = await client.GetAsync(requestUri);
            string str = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<T>(str);
            return response;
        }
    }
}
