using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMPP
{
    public class GetSmppResponse
    {
        public int AutoId { get; set; }
        public string? UserName { get; set; }
        public int UserId { get; set; }
        public int SmscId { get; set; }
        public string? GatwaySender { get; set; }        
        public bool Status { get; set; }
        public string? SmscSender { get; set; }
        public string? Remarks { get; set; }
        public int Fstatus { get; set; }
        public int CurrentStatus { get; set; }
    }

    //public class SetSmppResponse
    //{
    //    public int UserId { get; set; }
    //    public string? GatwaySender { get; set; }
    //    public int SmscId { get; set; }
    //    public bool Status { get; set; }
    //    public string? SmscSender { get; set; }
    //    public int Stage { get; set; }
    //    public int MbcId { get; set; }
    //    public int ModuleId { get; set; }
    //    public int SenderId { get; set; }
    //    public string? Remarks { get; set; }
    //}

    public class SmppIdRes
    {
        public string? SmppIdDetails { get; set; }
    }
    
}
