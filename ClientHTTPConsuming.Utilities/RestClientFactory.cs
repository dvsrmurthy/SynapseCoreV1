using ClientHTTPConsuming.Utilities.Serializers;

namespace ClientHTTPConsuming.Utilities
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestRequestAdapter GetJsonRestRequest(IUriBuilder uriBuilder)
        {
            var jsonRequest = new RestRequest(new JsonSerializer());
            return new RestRequestAdapter(uriBuilder, jsonRequest);
        }

        public RestRequestAdapter GetXmlRestRequest(IUriBuilder uriBuilder)
        {
            var xmlRequest = new RestRequest(new XmlSerializer());
            return new RestRequestAdapter(uriBuilder, xmlRequest);
        }

        public RestRequestAdapter GetUrlEncodedRestRequest(IUriBuilder uriBuilder)
        {
            var xmlRequest = new RestRequest(new UrlEncodedSerializer());
            return new RestRequestAdapter(uriBuilder, xmlRequest);
        }
    }
}
