using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AlertsManager
{
    public class GetCustomerBusinessRulesOnRequest
    {
        public int CUSTOMERID { get; set; }
        public int STATUS { get; set; }
        public int CUSTTYPE { get; set; }
        public int CREATEDBY { get; set; }
        public string? REQUESTEDBY { get; set; }

        public string? SEARCHTEXT { get; set; }
    }
    public class GetBusinessRulesOnRequest
    {
        public int CreatedBy { get; set; }
        public int Id { get; set; }
        public int Status { get; set; }
        public string? RequestedBy { get; set; }
        public int customerid { get; set; }
        public string? UserIp { get; set; }
    }
    public class GetBusinessProfilesOnRequest
    {
        public string? SerachProfile { get; set; }
        public string? ProfileName { get; set; }
        public int CreatedBy { get; set; }
        public int Return { get; set; }

    }
    public class InsertOrUpdateBusinessOnRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int CMFlag { get; set; }
        public string? BusinessRule { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string?  DbQuery {get; set;}
        public string? SelectTable { get; set; }
        public string? UniqueIDcolumn { get; set; }
        public string? Statuscolumn { get; set; }
        public string? RefIdCol { get; set; }
        public int Status { get; set; }
        public string? CountryId { get; set; }
        public int ProfileId { get; set; }
        public int CurrentStatus { get; set; }
        public int CreatedBy { get; set; }
        public string? ServerValue { get; set; }
        public int OnlineOrOffline { get; set; }
        public string? FilePath { get; set; }
        public string? Delimiter { get; set; }
        public int StatusUpdate { get; set; }
        public int Return { get; set; }
        public int BankId { get; set; }
        public string? UpdateValue { get; set; }
        public string? UserIp { get; set; }
    }

    public class StatusUpdatedOnRequest
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int UpdatedBy { get; set; }
        public int Return { get; set; }
        public int CurrentStatus { get; set; }
        public string? UserIp { get; set; }
    }
    public class TestStatementOnRequest
    {
        public string? Connection { get; set; }
        public string? Statement { get; set; }
        public int Dbtype { get; set; }
    }
    public class ApproveBusinessRuleOnRequest
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string? RejectNote { get; set; }
        public int UpdatedBy { get; set; }
        public int Return { get; set; }
    }
}
