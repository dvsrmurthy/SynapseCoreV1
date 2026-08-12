using System;
using System.Linq;

namespace ClientHTTPConsuming.Utilities
{
    public class UriBuilder : IUriBuilder
    {
        private readonly string _uriTemplate;

        public UriBuilder(string template)
        {
            if (template == null) throw new ArgumentNullException("template");
            _uriTemplate = template;
        }

        public Uri GetUri()
        {
            return new Uri(_uriTemplate);
        }

        public Uri GetUriFor(object parameters)
        {
            var type = parameters.GetType();
            var properties = type.GetProperties();
            var url = properties
                .Aggregate(_uriTemplate,
                           (current, property) =>
                            current.Replace("{" + property.Name + "}", property.GetValue(parameters, null).ToString()));
            return new Uri(url);
        }
    }
}
