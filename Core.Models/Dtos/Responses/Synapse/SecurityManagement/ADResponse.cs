namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class ADResponse
    {
        public bool IsValidLdapUser { get; set; }

        public string? Message { get; set; }
    }
}
