using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserContacts
{
    public class ContactsOnResponse
    {
    }
    public class GetGroupsContactsOnResponse
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupStatus { get; set; }
        public int CurrentStatus { get; set; }
        public int ContactsCount { get; set; }  
    
    }
    public class ShowGridContactsOnResponse
    {
        public Int64 ContGrpID { get; set; }
        public Int64 ContID { get; set; }
        public int GroupID { get; set; }
        public string GrpName { get; set; }
        public string Mobileno { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public int CurrentStatus { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ImportContactsCSVOnResponse
    {
        public string MOBILENO { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string EMAIL { get; set; }
    }
    public class ExportContactsOnResponse
    {
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string GroupName { get; set; }
    }

}
