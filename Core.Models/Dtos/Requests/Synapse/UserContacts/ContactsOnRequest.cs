using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserContacts
{
    public class GetGroupsContactsOnRequest
    {
        public int GROUPID { get; set; }
        public int CREATEDBY { get; set; }
        public int STATUS { get; set; }
        public string? REQUESTEDBY { get; set; }
        public string? UserIp { get; set; }
    }
    public class ShowGridContactsOnRequest
    {
        public int USERGROUPID { get; set; }
        public Int64 CREATEDBY { get; set; }
       public int STATUS{get;set;}
       public string? STRSEARCH {get;set;}
       public string? GroupID { get; set; }
       public string? UserIp { get; set; }
       public string? SearchText { get; set; }
    }
    public class InsertContactsOnRequest
    {
        public int CONTACTID { get; set; }
        public string? FIRSTNAME{get;set;}
        public string? LASTNAME{get;set;}
        public string? EMAIL{get;set;}
        public string? MOBILE{get;set;}
        public string? GROUPIDS{get;set;}
        public int CREATEDBY{get;set;}
        public int CURRENTSTATUS { get; set; }
        public string? UserIp { get; set; }
        public bool GroupUpdate { get; set; }   
    }
    public class UpdateContactsOnRequest
    {
        public int USERGROUPID{get;set;}
        public string? FIRSTNAME{get;set;}
        public string? LASTNAME{get;set;}
        public string? EMAIL{get;set;}
        public string? MOBILE{get;set;}
        public string? GROUPIDS{get;set;}
        public int UPDATEDBY{get;set;}
        public string? UserIp { get; set; }
    }
    public class ChangeStatusContactsOnRequest
    {
        public int CONTGRPID{get;set;}
        public int STATUS{get;set;}
        public int UPDATEDBY{get;set;}
        public int CURRENTSTATUS { get; set; }
        public string? UserIp { get; set; }
    }
    public class DeleteContactsOnRequest
    {
        public string? CONTACTIDS { get; set; }
        public string?  GROUPIDS { get; set; }
    }
    public class ImportContactsCSVOnRequest
    {
        public string? FILEPATH { get; set; }
        public string? GROUPIDS { get; set; }
        public int CREATEDBY { get; set; }
        public int FILETYPE { get; set; }
        public int CURRENTSTATUS { get; set; }
        public List<ImportContact> ContactsList { get; set; }
    }
     public class ImportContact
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }      
    }
    public class ExportContactsOnRequest
    {
        public int CONTGRPID { get; set; }
        public int CREATEDBY { get; set; }
        public int STATUS { get; set; }
        //public string? STRSEARCH { get; set; }
        //public string? FILEEXTENSION { get; set; }
    }
}
