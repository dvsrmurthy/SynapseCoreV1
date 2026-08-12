namespace ClientHTTPConsuming.Utilities
{
    public interface IWebClient
    {
        TResponse Post<TRequest, TResponse>(TRequest data);
        TResponse Post<TRequest, TResponse>(TRequest data, object parameters);
        string Post<TRequest>(TRequest data);
        string Post<TRequest>(TRequest data, object parameters);
        TResponse Get<TResponse>(object parameters);
    }
}
