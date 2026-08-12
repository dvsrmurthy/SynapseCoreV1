using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class DNDListRequest
    {
        public int nCustomerId { get; set; }
        public int nUserId { get; set; }
        public int nDNDId { get; set; }
        public string? strMobileNo { get; set; }
        public string? strName { get; set; }
        public bool nStatus { get; set; }
        public int bSearch { get; set; }
        public string? requestedby { get; set; }
        public string? UserIp { get; set; }
        public string? SearchText { get; set; }
    }
    public class InsertDNDList
    {
        public int nId { get; set; }
        public string? strMobileNo { get; set; }
        public string? strName { get; set; }
        public int nUserId { get; set; }
        public int nCustomerId { get; set; }
        public string? command { get; set; }
        public int Currentstatus { get; set; }
        public int Updatedby { get; set; }
        public int CreatedBy { get; set; }
        public int Custname { get; set; }
        public string? UserIp { get; set; }
        //public int custId { get; set; }
    }

    public class ImportDNDRequest
    {
        public int Id { get; set; }
        public string? FILEPATH { get; set; }
        public int Createdby { get; set; }
        public int FILETYPE { get; set; }
        public int CurrentStatus { get; set; }
        public List<ImportDND> DNDList { get; set; }
    }
    
    public class ImportDND
    {
        public string? Name { get; set; }
        public string? MobileNo { get; set; }      
    }


    public class ExportDNDReq
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }

    //For Checker
    public class StatusUpdateDNDList
    {
        public string? id { get; set; }
        public int UpdatedBy { get; set; }
        public int Currentstatus { get; set; }
        public int EventType { get; set; }
        public string? RejectNote { get; set; }
        public int ReturnValue { get; set; }
        public bool status { get; set; }
        public int Functionalstatus { get; set; }
        public string? inputparam { get; set; }
        public string? MobileNo { get; set; }
    }
}
