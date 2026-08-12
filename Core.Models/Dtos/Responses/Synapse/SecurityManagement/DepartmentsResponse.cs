using Core.Models.Enums;

namespace Core.Models.Dtos.Responses.Synapse.SecurityManagement
{
    public class DepartmentsResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public Cstatus FStatus { get; set; }
        public string? RejectNote { get; set; }
    }
}
