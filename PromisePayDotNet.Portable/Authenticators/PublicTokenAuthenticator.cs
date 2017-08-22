using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.Authenticators;

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

		private static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}

        public bool CanPreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            return true;
        }

        public bool CanPreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            return false;
        }

        public bool CanHandleChallenge(IHttpClient client, IHttpRequestMessage request, ICredentials credentials, IHttpResponseMessage response)
        {
            return false;
        }

        public Task PreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            request.AddHeader("Authorization", "Basic " + Base64Encode($"{Email}:{PublicToken}"));
            return Task.FromResult(0);
        }

        public Task PreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public Task HandleChallenge(IHttpClient client, IHttpRequestMessage request, ICredentials credentials, IHttpResponseMessage response)
        {
            throw new NotImplementedException();
        }
    }
}
