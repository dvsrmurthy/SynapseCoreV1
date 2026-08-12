namespace Core.Models.Dtos.Requests.Synapse.UserManagement
{
    public class ApproveUserRequest
    {
        public int UserId { get; set; }

        public int CurrentStatus { get; set; }

        public bool Status { get; set; }

        public int UpdatedBy { get; set; }

        public string RejectionReason { get; set; }
    }
}
