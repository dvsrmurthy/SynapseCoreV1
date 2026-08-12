using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

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

        public TResponse Post<TRequest, TResponse>(TRequest request, Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            //webRequest.ProtocolVersion = HttpVersion.Version10;
            //webRequest.ServicePoint.Expect100Continue = false;
            var webexetime = System.Configuration.ConfigurationManager.AppSettings["WebReqExeTime"] == null ? "300000" : System.Configuration.ConfigurationManager.AppSettings["WebReqExeTime"];
            webRequest.Timeout = Convert.ToInt32(webexetime);
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", System.Configuration.ConfigurationManager.AppSettings["accsToken"]);
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

        public string Post<TRequest>(TRequest request, Uri uri)
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
            catch (Exception ex)
            {
                throw new Exception(String.Format(
                            "Server error URL : {0} --- Exception {1}",
                            webRequest.Address.AbsolutePath, ex.Message));
            }
        }
    }
}
