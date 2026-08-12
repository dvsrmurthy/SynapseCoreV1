using System;

namespace ClientHTTPConsuming.Utilities
{
    public interface IRestRequest
    {
        bool IsAuthenticated();
        TResponse Post<TRequest, TResponse>(TRequest request, Uri uri);
        string Post<TRequest>(TRequest request, Uri uri);
        TResponse Get<TResponse>(Uri uri);
    }
}
