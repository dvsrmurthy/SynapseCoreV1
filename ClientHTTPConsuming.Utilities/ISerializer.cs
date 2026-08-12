using System.IO;

namespace ClientHTTPConsuming.Utilities
{
    public interface ISerializer
    {
        string Serialize<TRequest>(TRequest request);
        TResponse Desearialize<TResponse>(Stream stream);
        string ContentType { get; }
    }
}
