﻿using Newtonsoft.Json;
using RestSharp.Portable;
using System.Collections.Generic;
using System.Net;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class PayPalAccountRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                         PromisePayDotNet.Dynamic.Interfaces.IPayPalAccountRepository
    {
        public PayPalAccountRepository(IRestClient client)
            : base(client)
        {
        }

        public IDictionary<string, object> GetPayPalAccountById(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var request = new RestRequest("/paypal_accounts/{id}", Method.GET);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public IDictionary<string, object> CreatePayPalAccount(IDictionary<string, object> paypalAccount)
        {
            var request = new RestRequest("/paypal_accounts", Method.POST);
            request.AddParameter("user_id", (string)paypalAccount["user_id"]);
            var paypal = (IDictionary<string, object>)(paypalAccount["paypal"]);

            foreach (var key in paypal.Keys) {
                request.AddParameter(key, paypal[key]);
            }

            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }

        public bool DeletePayPalAccount(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var request = new RestRequest("/paypal_accounts/{id}", Method.DELETE);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(Client, request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;
        }

        public IDictionary<string, object> GetUserForPayPalAccount(string paypalAccountId)
        {
            AssertIdNotNull(paypalAccountId);
            var request = new RestRequest("/paypal_accounts/{id}/users", Method.GET);
            request.AddUrlSegment("id", paypalAccountId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
        }
    }
}
