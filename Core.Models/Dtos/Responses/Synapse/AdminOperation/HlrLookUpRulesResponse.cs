namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class HlrLookUpRulesResponse
    {
        public int Id { get; set; }

        public int CustomerId { get; set; } 

        public string CustomerName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int SenderId { get; set; }

        public bool LookUpEnableForCampaigns { get; set; }

        public bool LookUpEnableForAlerts { get; set; }

        public int HoldPeriodForAbsent { get; set; }

        public int LookUpReAttempt { get; set; }

        public bool SendSmsInRoaming { get; set; }

        public bool IsActive { get; set; }

        public int CurrentStatus { get; set; }

        public string CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public string RejectNote { get; set; }

        public string Code { get; set; }
    }

    public class UserLookup
    {
        public int Id { get; set; }

        public string UserName { get; set; }
    }

    public class SnderLookUp
    {
        public int Id { get; set; }

        public string Code { get; set; }
    }
}
