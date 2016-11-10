using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using N26.Models;

namespace N26
{
    public class N26
    {
        string bearer = "YW5kcm9pZDpzZWNyZXQ=";
        string api = "https://api.tech26.de";
        string N26token = string.Empty;

        public async void Login(string username, string password)
        {
            var v = new { username = username, password = password, grant_type = "password", Authorization = "Basic " + bearer };

            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(v));

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

        //            public Me me()
        //            {
        //                var client = new RestClient(api);
        //                var request = new RestRequest("/api/me");
        //                request.AddHeader("Authorization", "bearer " + N26token);

        //                IRestResponse<Me> response = client.Get<Me>(request);
        //                return response.Data;
        //            }

        //            public Accounts GetAccounts()
        //            {
        //                var client = new RestClient(api);
        //                var request = new RestRequest("/api/accounts");
        //                request.AddHeader("Authorization", "bearer " + N26token);

        //                IRestResponse<Accounts> response = client.Get<Accounts>(request);
        //                return response.Data;
        //            }

        //            public Transactions GetTransactions()
        //            {
        //                var client = new RestClient(api);
        //                var request = new RestRequest("/api/transactions?sort=visibleTS&dir=DESC&limit=50");
        //                request.AddHeader("Authorization", "bearer " + N26token);
        //                IRestResponse<Transactions> response = client.Get<Transactions>(request);
        //                return response.Data;
        //            }

        //            public Cards GetCards()
        //            {
        //                var client = new RestClient(api);
        //                var request = new RestRequest("/api/cards");
        //                request.AddHeader("Authorization", "bearer " + N26token);

        //                IRestResponse<Cards> response = client.Get<Cards>(request);
        //                return response.Data;

        //            }

        //            public Addresses GetAddresses()
        //            {
        //                var client = new RestClient(api);
        //                var request = new RestRequest("/api/addresses");
        //                request.AddHeader("Authorization", "bearer " + N26token);
        //                IRestResponse<Addresses> response = client.Get<Addresses>(request);
        //                return response.Data;
        //            }

        //        }
    }
}
