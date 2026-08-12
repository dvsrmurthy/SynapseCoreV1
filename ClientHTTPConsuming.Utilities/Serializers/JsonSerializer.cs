using System.IO;
using System.Runtime.Serialization.Json;

namespace ClientHTTPConsuming.Utilities.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<TRequest>(TRequest request)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(typeof(TRequest));
            serializer.WriteObject(stream, request);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public TResponse Desearialize<TResponse>(Stream stream)
        {
            var ser = new DataContractJsonSerializer(typeof(TResponse));
            var result = ser.ReadObject(stream);
            return (TResponse)result;
        }

        public string ContentType
        {
            get { return "application/json; charset=utf-8"; }
        }
    }
}
