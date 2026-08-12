namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class AccountManagersResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Job { get; set; }

        public string JoinDate { get; set; }

        public string JobLeft { get; set; }

        public bool Status { get; set; }

        public int CurrentStatus { get; set; }

        public string RejectNote { get; set; }

        public string Remarks { get; set; }

        public int AddedBy { get; set; }

        public string Password { get; set; }

        public int CustomerId { get; set; }

        public string HashKey { get; set; }

        public bool Web { get; set; }
        public string UserIp { get; set; }
    }
}
