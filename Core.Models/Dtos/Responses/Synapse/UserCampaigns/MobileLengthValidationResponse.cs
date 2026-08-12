namespace Core.Models.Dtos.Responses.Synapse.UserCampaigns
{
    public class MobileLengthValidationResponse
    {
        public int SenderId { get; set; }

        public int MobileLength { get; set; }

        public int CountryCode { get; set; }

        public int TotalLength { get; set; }

        public string Name { get; set; }

        public string series { get; set; }
    }
}
