using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientHTTPConsuming.Utilities
{
    public interface IRestClientFactory
    {
        RestRequestAdapter GetJsonRestRequest(IUriBuilder uriBuilder);
        RestRequestAdapter GetXmlRestRequest(IUriBuilder uriBuilder);
        RestRequestAdapter GetUrlEncodedRestRequest(IUriBuilder uriBuilder);
    }
}
