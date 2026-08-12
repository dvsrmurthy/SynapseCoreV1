using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserMoKeyWordConfig
{
    public class MoKeyWordConfigOnResponse
    {
        public int KeyID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Language { get; set; }
        public int UserID { get; set; }
        public bool Status { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectReason { get; set; }
        public int CreatedBy { get; set; }
        public string? Createdon { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }

        //public int KYW_INT_ID { get; set; }
        //public int KYD_SINT_CREATEDBY { get; set; }        
        //public string? KYD_VAR_NAME { get; set; }
        //public string? KYD_VAR_DESC { get; set; }
        //public string? KYD_DTM_VALID_FROM { get; set; }
        //public string? KYD_DTM_VALID_TO { get; set; }
        //public int KYD_INT_LANG { get; set; }
        //public string? LNG_VAR_NAME { get; set; }
        //public int KYD_INT_STATUS { get; set; }
    }
    public class EditMoKeyWordConfigOnResponse
    {
        //public int KYW_INT_ID { get; set; }
        //public string? KYD_VAR_NAME { get; set; }
        //public string? KYD_VAR_DESC { get; set; }
        //public string? KYD_DTM_VALID_FROM { get; set; }
        //public string? KYD_DTM_VALID_TO { get; set; }
        //public int KYD_INT_LANG { get; set; }
        //public int KYD_INT_STATUS { get; set; }
    }
}
