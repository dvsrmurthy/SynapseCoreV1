namespace Core.Models.Dtos.Responses.Synapse.UserManagement
{
    public class LdapUseDetailsResponse
    {
        public string? EmailAddress { get; set; }
        public string? GivenName { get; set; }
        public string? MiddleName { get; set; }
        public string? Surname { get; set; }
        public string? EmployeeId { get; set; }
        public string? PhoneNo { get; set; }
        public bool IsValidUser { get; set; }
    }
}
