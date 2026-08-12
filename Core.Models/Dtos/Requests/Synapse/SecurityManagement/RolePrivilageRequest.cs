namespace Core.Models.Dtos.Requests.Synapse.SecurityManagement
{
    public class RolePrivilageRequest
    {
        public string[] PrivilageIds { get; set; }

        public int RoleId { get; set; }

        public int CreatedBy { get; set; }

        public int CurrentStatus { get; set; }

        public string UserIp { get; set; }

        public int CustomerId { get; set; }
    }
}
