namespace Core.Models.Dtos.Requests.Synapse.EmailAndPushNotifications
{
    public class RegisterPushNotificationRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? SecretKey { get; set; }

        public string? SecretKeyLabel { get; set; }

        public int PhoneType { get; set; }

        public int CustomerId { get; set; }

        public int UserId { get; set; }

        public int Status { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectReason { get; set; }

        public int CreatedBy { get; set; }

        public string? UserIp { get; set; }

        public int IsStatusUpdate { get; set; }
    }
}
