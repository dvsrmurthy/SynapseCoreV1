using System;

namespace ClientHTTPConsuming.Utilities
{
    public class RestRequestAdapter : IWebClient
    {
        private readonly IUriBuilder _uriBuilder;
        private readonly IRestRequest _restRequest;

        public RestRequestAdapter(IUriBuilder uriBuilder, IRestRequest restRequest)
        {
            if (uriBuilder == null) throw new ArgumentNullException("uriBuilder");
            if (restRequest == null) throw new ArgumentNullException("restRequest");
            _uriBuilder = uriBuilder;
            _restRequest = restRequest;
        }

        public TResponse Post<TRequest, TResponse>(TRequest data)
        {
            return Post<TRequest, TResponse>(data, null);
        }

        public TResponse Post<TRequest, TResponse>(TRequest data, object parameters)
        {
            var uri = (parameters == null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);
            return _restRequest.Post<TRequest, TResponse>(data, uri);
        }

        public TResponse Get<TResponse>(object parameters)
        {
            var uri = (parameters == null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);
            return _restRequest.Get<TResponse>(uri);
        }        
        public string Post<TRequest>(TRequest data)
        {
            return Post(data, null);
        }

        public string Post<TRequest>(TRequest data, object parameters)
        {
            var uri = (parameters == null) ? _uriBuilder.GetUri() : _uriBuilder.GetUriFor(parameters);
            return _restRequest.Post(data, uri);
        }
    }
}
