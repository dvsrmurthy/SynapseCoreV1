namespace Core.Models.Dtos.Requests.Synapse.Customers
{
    public class CustomerCreationRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pin { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public string? Logo { get; set; }
        public string? FooterNotes { get; set; }
        public int CumtomerType { get; set; }
        public int CreateToCustomerCount { get; set; }
        public string? ExpiryDate { get; set; }
        public int ParentId { get; set; }
        public bool Status { get; set; }
        public int AcmId { get; set; }
        public string? Remarks { get; set; }
        public int CreatedBy { get; set; }
        public int CurrentStatus { get; set; }
        public string? UserIp { get; set; }
        public string? TechnicalEmail { get; set; }
        public string? TechnicalPhone { get; set; }
        public string? BusinessEmail { get; set; }
        public string? BusinessPhone { get; set; }

    }
}
