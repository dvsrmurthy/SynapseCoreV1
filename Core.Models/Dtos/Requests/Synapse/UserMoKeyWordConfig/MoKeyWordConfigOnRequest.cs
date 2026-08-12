using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserMoKeyWordConfig
{
    public class MoKeyWordConfigOnRequest
    {
        public int UserID { get; set; }
        public int CustomerID { get; set; }
        public string? SearchValue { get; set; }
        public int Status { get; set; }
        public int KeyID { get; set; }
        public string? UserIp { get; set; }
    }
    public class InsertMoKeyWordConfigOnRequest
    {
        public string? kid { get; set; }
        public int KeyID { get; set; }
        public string? KeyName{get;set;}
        public string? KeyDescription{get;set;}
        public string? ValidFrom{get;set;}
        public string? ValidTo{get;set;}
        public string? Language{get;set;}
        public int UserID{get;set;}
        public int CustomerID{get;set;}
        public int Status { get; set; }
        public string? requestedby { get; set; }
        public int Createdby { get; set; }
        public int Currentstatus { get; set; }
        public string? command { get; set; }
        public int UpdatedBy { get; set; }
        public string? UserIp { get; set; }
    }
    public class ChangeStatusMoKeyWordConfigOnRequest
    {
        public int KeyID { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }
        public string? UserIp { get; set; }
    }
}
