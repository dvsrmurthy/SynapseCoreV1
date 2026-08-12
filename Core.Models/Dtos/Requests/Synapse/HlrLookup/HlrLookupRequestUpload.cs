namespace Core.Models.Dtos.Requests.Synapse.HlrLookup
{
    public class HlrLookupRequestUpload
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public string? UserIp { get; set; }
    }

    public class SaveHlrLookupRequestUpload
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int CurrentStatus { get; set; }
        public int ReturnValue { get; set; }
        public string? Email { get; set; }
        public string? UserIp { get; set; }
    }

    public class CountryDetailsHlr
    {
        public string? Strsearch { get; set; }
        public int Nsearch { get; set; }
        public int NCreatedBy { get; set; }
        public string? RequestPage { get; set; }
    }

    public class StatusHlrUpdatedOnRequest
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int UpdatedBy { get; set; }
        public int CurrentStatus { get; set; }
        public int Return { get; set; }
        public string? UserIp { get; set; }
    }
}
