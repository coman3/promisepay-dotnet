using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PromisePayDotNet.Dynamic.Implementations
{
    public class AddressRepository : PromisePayDotNet.Implementations.AbstractRepository,
                                     PromisePayDotNet.Dynamic.Interfaces.IAddressRepository
    {
        public AddressRepository(IRestClient client)
            : base(client)
        {
        }

        public IDictionary<string,object> GetAddressById(string addressId)
        {
            AssertIdNotNull(addressId);
            var request = new RestRequest("/addresses/{id}", Method.GET);
            request.AddUrlSegment("id", addressId);
            var response = SendRequest(Client, request);
            return JsonConvert.DeserializeObject<IDictionary<string,object>>(response.Content);
        }
    }
}
