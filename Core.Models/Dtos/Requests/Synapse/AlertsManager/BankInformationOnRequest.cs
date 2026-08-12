using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AlertsManager
{
    public class BankInformationOnRequest
    {
        public int NID { get; set; }
        public string? BankCode { get; set; }
        public string? BankName { get; set; }
        public string? ShortDescription { get; set; }
        public int Status { get; set; }
        public string? RejectNote { get; set; }
        public int CurrentStatus { get; set; }
        public int NCREATEDBY { get; set; }
        public int NUpdatedBY { get; set; }
        public int NRETURN { get; set; }
    }

    public class GetBankInformationDetailsRequest
    {
        public int bankid { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int NUPDATEDBY { get; set; }
        public string? NUPDATEDUSER { get; set; }
        public int Return { get; set; }
        public string? RequestPage { get; set; }
    }

    public class ApproveRejectBankInformationDetailsCreation
    {
        public int bankid { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int UpdatedBy { get; set; }
        public string? Rejectreason { get; set; }
        public int ReturnValue { get; set; }
    }
}
