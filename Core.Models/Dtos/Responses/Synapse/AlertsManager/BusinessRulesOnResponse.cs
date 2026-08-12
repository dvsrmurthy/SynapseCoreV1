using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AlertsManager
{
    public class GetCustomerbusinessOnResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetBusinessProfilesOnResponse
    {
        public int Id { get; set; }

        public string Profile { get; set; }
        public string DBtype { get; set; }
        public int CurrentStatus { get; set; }
        public string ConnectionString { get; set; }
    }
    public class GetBusinessRulesOnResponse
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public int CustomerId { get; set; }
        public string CountryID { get; set; }
        public string CustomerName { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }
        public int CmFlag { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string DbQuery { get; set; }
        public string SrcTable { get; set; }
        public string UniqueIdCol { get; set; }
        public string StatusCol { get; set; }
        public int Status { get; set; }
        public int Profileid { get; set; }
        public string Profile { get; set; }
        public string RejectNote { get; set; }
        public int CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public string ServerValue { get; set; }
        public int OnlineOrOffline { get; set; }
        public string FilePath { get; set; }
        public string Delimiter { get; set; }
        public int BankName { get; set; }
        public string BDBankName { get; set; }
        public string UpdateValue { get; set; }
        public string RefIdCol { get; set; }
    }
    public class TestStatementViewOnResponse
    {
        public string ResultTable { get; set; }
        public string Result { get; set; }
        public string ResultError { get; set; }
    }


}
