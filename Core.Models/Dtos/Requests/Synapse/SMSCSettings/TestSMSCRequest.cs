using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class TestSMSCRequest
    {
        public int nID { get; set; }
        public int nAddedBy { get; set; }
        public int nStatus { get; set; }
        public int nTestSMSC { get; set; }
        public string? RequestedPage { get; set; }
        public string? UserIp { get; set; }
    }


    public class AddSMSRequest
    {
        public int QSMSID { get; set; }
        public int nSenderID { get; set; }

        public string? nLangId { get; set; }

        public string? strMsg { get; set; }

        public int nCharCount { get; set; }

        public int nCreditsUsed { get; set; }

        public int nDLR { get; set; }

        public int nAddedBy { get; set; }

        public int TotalCreditsReq { get; set; }

        public int CustomerID { get; set; }

        public string? Sender { get; set; }

        public string? Module { get; set; }

        public string? Stage { get; set; }

        public int Status { get; set; }

        public string? strMobiles { get; set; }

        //public string? strFIELD4 { get; set; }

      //  public string? strFIELD5 { get; set; }

        public int nTestSMSC { get; set; }

        public int nReturn { get; set; }

        public int nId { get; set; }

        public string? command { get; set; }

        public string? RejectNote { get; set; }

      
            public int Currentstatus { get; set; }
            public int updatedby { get; set; }
            public int EventType { get; set; }
            public string? UserName { get; set; }
            public int userId { get; set; }
            public string? UserIp { get; set; }
      
    }

    public class CheckTestSMSC
    {
        public int QuicksmsId { get; set; }

        public int STATUS { get; set; }
        public string? RejectNote { get; set; }


        public int Currentstatus { get; set; }
        public int updatedby { get; set; }
    }

}
