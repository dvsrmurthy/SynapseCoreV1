//using System.IO;
//using System.Text;
//using System.Text.Json;

//namespace ClientHTTPConsuming.Utilities.Serializers
//{
//    public class JsonSerializer : ISerializer
//    {
//        public string? Serialize<TRequest>(TRequest request)
//        {
//            var stream = new MemoryStream();
//            var serializer = new DataContractJsonSerializer(typeof(TRequest));
//            serializer.WriteObject(stream, request);
//            stream.Position = 0;
//            var reader = new StreamReader(stream);
//            return reader.ReadToEnd();
//        }

//        public TResponse Desearialize<TResponse>(Stream stream)
//        {
//            //var ser = new DataContractJsonSerializer(typeof(TResponse));
//            //var result = ser.ReadObject(stream);
//            //return (TResponse)result;
//            if (stream == null)
//                throw new ArgumentNullException(nameof(stream));

//            using var reader = new StreamReader(
//                stream,
//                Encoding.UTF8);

//            string json = reader.ReadToEnd();

//            if (string.IsNullOrWhiteSpace(json))
//                throw new InvalidOperationException(
//                    "The server returned an empty response.");

//            var options = new JsonSerializerOptions
//            {
//                PropertyNameCaseInsensitive = true
//            };

//            var result = JsonSerializer.Deserialize<TResponse>(
//                json,
//                options);

//            if (result == null)
//                throw new InvalidOperationException(
//                    $"Unable to deserialize response to {typeof(TResponse).Name}. " +
//                    $"JSON: {json}");

//            return result;
//        }

//        public string? ContentType
//        {
//            get { return "application/json; charset=utf-8"; }
//        }
//    }
//}
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientHTTPConsuming.Utilities.Serializers
{
    public class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerOptions JsonOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,

                // Keep enum handling compatible when APIs return
                // enum values as strings.
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

        /// <summary>
        /// Serializes an object to JSON.
        /// </summary>
        public string? Serialize<TRequest>(TRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return System.Text.Json.JsonSerializer.Serialize(
                request,
                JsonOptions);
        }

        /// <summary>
        /// Deserializes JSON response into the requested type.
        /// </summary>
        public TResponse Desearialize<TResponse>(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            // Make sure we are reading from the beginning.
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

            using var reader = new StreamReader(
                stream,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                leaveOpen: false);

            string json = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidOperationException(
                    $"The server returned an empty response " +
                    $"while deserializing {typeof(TResponse).Name}.");
            }

            try
            {
                var result =
                    System.Text.Json.JsonSerializer.Deserialize<TResponse>(
                        json,
                        JsonOptions);

                if (result == null)
                {
                    throw new InvalidOperationException(
                        $"Unable to deserialize response to " +
                        $"{typeof(TResponse).Name}." +
                        Environment.NewLine +
                        $"Response JSON: {json}");
                }

                return result;
            }
            catch (System.Text.Json.JsonException ex)
            {
                throw new InvalidOperationException(
                    $"JSON deserialization failed for " +
                    $"{typeof(TResponse).Name}." +
                    Environment.NewLine +
                    $"Response JSON: {json}" +
                    Environment.NewLine +
                    $"Error: {ex.Message}",
                    ex);
            }
        }

        /// <summary>
        /// Content type used for HTTP requests.
        /// </summary>
        public string? ContentType
        {
            get
            {
                return "application/json; charset=utf-8";
            }
        }
    }
}