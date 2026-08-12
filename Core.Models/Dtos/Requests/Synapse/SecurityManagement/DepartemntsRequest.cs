namespace Core.Models.Dtos.Requests.Synapse.SecurityManagement
{
    public class DepartemntsRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CustomerId { get; set; }

        public int CurrentStatus { get; set; }

        public int CreatedBy { get; set; }
    }
}
