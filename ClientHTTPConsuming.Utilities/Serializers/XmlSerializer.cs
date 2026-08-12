using System.IO;

namespace ClientHTTPConsuming.Utilities.Serializers
{
    public class XmlSerializer : ISerializer
    {
        public virtual string Serialize<TRequest>(TRequest request)
        {
            var stream = new MemoryStream();
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TRequest));
            serializer.Serialize(stream, request);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public virtual TResponse Desearialize<TResponse>(Stream stream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TResponse));
            var response = serializer.Deserialize(stream);
            return (TResponse)response;
        }

        public virtual string ContentType
        {
            get { return "application/xml"; }
        }
    }
}
