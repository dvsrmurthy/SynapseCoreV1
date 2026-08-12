using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMPP
{
    public class GetSmppRequest
    {
        public int nSMPPSendetID { get; set; }
        public int nuserid { get; set; }
        public string? strSender { get; set; }
        public int nStatus { get; set; }
       // public int currentstatus { get; set; }

    }

    public class SetSmppRequest
    {
        public int nUseId { get; set; }
        public string? UserName { get; set; }
        public string? strGWSender { get; set; }
        public string? strOutBoundSender { get; set; }
        public string? strShCode { get; set; }
        public string? strRemarks { get; set; }
        public int nSMPPSEnderstatus { get; set; }
        public int NSMPPID { get; set; }
        public string? Stage { get; set; }
        public int MBCID { get; set; }
        public int ModuleID { get; set; }    
        public string? command { get; set; }
        public int nID { get; set; }
       
    }
    public class SmppIdReq
    {
        public int UserId { get; set; }
    }

   

}
