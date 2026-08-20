using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ClientHTTPConsuming.Utilities
{
    public class RestRequest : IRestRequest
    {
        private readonly ISerializer _serializer;
        public RestRequest(ISerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            _serializer = serializer;            
        }

        public bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }
        public string? GetConfiguration(string param)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Sets look-up folder to application directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            return configuration[param].ToString(); 
        }   

        public TResponse Post<TRequest, TResponse>(TRequest request, Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.CreateHttp(uri);           
            var webexetime = GetConfiguration("WebReqExeTime") == null ? "300000" : GetConfiguration("WebReqExeTime");
            webRequest.Timeout = Convert.ToInt32(webexetime);
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", GetConfiguration("accsToken"));
            webRequest.ContentType = _serializer.ContentType;
            webRequest.KeepAlive = true;

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(_serializer.Serialize(request));

            webRequest.ContentLength = bytes.Length;

            using (var requestStream = webRequest.GetRequestStream())
            {
                // Send the data.
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                return GetHttpWebResponse<TResponse>(webRequest);
            }
        }

        public string? Post<TRequest>(TRequest request, Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "POST";
            webRequest.ContentType = _serializer.ContentType;
            webRequest.KeepAlive = true;

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(_serializer.Serialize(request));

            webRequest.ContentLength = bytes.Length;

            using (var requestStream = webRequest.GetRequestStream())
            {
                // Send the data.
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                using (var response = webRequest.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));

                    var stream = response.GetResponseStream();
                    if (stream == null)
                        throw new Exception("Could not read the response stream");

                    var reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
        }

        public TResponse Get<TResponse>(Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            webRequest.ContentType = _serializer.ContentType;
            return GetHttpWebResponse<TResponse>(webRequest);
        }

        private TResponse GetHttpWebResponse<TResponse>(HttpWebRequest webRequest)
        {
            try
            {
                using (var response = webRequest.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));

                    return _serializer.Desearialize<TResponse>(response.GetResponseStream());
                }
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        string errorBody = reader.ReadToEnd();
                        // Include errorBody in your exception message to see what went wrong!
                        throw new Exception($"Remote server returned (400) Bad Request. Details: {errorBody}", ex);
                    }
                }
                throw;
            }
        }
    }
}
