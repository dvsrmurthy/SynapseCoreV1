namespace ClientHTTPConsuming.Utilities.Serializers
{
    public class UrlEncodedSerializer : XmlSerializer
    {
        public override string Serialize<TRequest>(TRequest request)
        {
            return request.ToString();
        }

        public override string ContentType
        {
            get
            {
                return "application/x-www-form-urlencoded";
            }
        }
    }
}
