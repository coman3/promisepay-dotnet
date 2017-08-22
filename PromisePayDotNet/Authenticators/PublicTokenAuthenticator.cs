using System;
using RestSharp;
using RestSharp.Authenticators;

namespace PromisePayDotNet.Authenticators
{
    public class PublicTokenAuthenticator : IAuthenticator
    {
        public string Email { get; set; }
        public string PublicToken { get; set; }
        public PublicTokenAuthenticator(string email, string publicToken)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrWhiteSpace(publicToken)) throw new ArgumentNullException(nameof(publicToken));
            Email = email;
            PublicToken = publicToken;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("Authorization", Base64Encode($"{Email}:{PublicToken}"));
        }

		private static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
    }
}
