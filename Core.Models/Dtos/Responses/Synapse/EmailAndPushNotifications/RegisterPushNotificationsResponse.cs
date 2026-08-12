using DocumentFormat.OpenXml.Wordprocessing;

namespace Core.Models.Dtos.Responses.Synapse.EmailAndPushNotifications
{
    public class RegisterPushNotificationsResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? SecretKey { get; set; }

        public string? SecretKeyLabel { get; set; }

        public int PhoneType { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public int Status { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectReason { get; set; }
    }

    public class AppRegistrationResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Secretkey { get; set; }

        public string? SecretKeyLabel { get; set; }

        public int PhoneType { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public int Status { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectReason { get; set; }
    }
}
