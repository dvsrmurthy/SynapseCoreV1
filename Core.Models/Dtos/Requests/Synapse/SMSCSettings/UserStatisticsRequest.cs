using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
   
        //public class GetCustomersForUserStatisticsRequest
        //{
        //    public string? STRName { get; set; }
        //    public int NSEARCH { get; set; }           
        //    public int nCustType { get; set; }
        //    public int nCreatedby { get; set; }
        //    public string? requestedby { get; set; }
        //    public int NRETVAL { get; set; }
        //}
        //public class GetUsersDetailsRequest
        //{
        //    public int NUSERID { get; set; }
        //    public int NSTATUS { get; set; }
        //    public int NCUSTID { get; set; }
        //    public int NCREATEDBY { get; set; }
        //    public string? requestedby { get; set; }
        //}
        public class GetGBLSenderDetailsRequest
        {
            public int nUserId { get; set; }
            public string? requestedby { get; set; }
            public int NRETVAL { get; set; }
        }
        public class GETINTLUSERSTATDETAILSREquest
        {
            public int NSENDERID { get; set; }
            public int nUserId { get; set; }
            public string? requestedby { get; set; }
            public int NRETVAL { get; set; }
        }
    }
