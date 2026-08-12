using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.StatusMonitor
{
    public class CustUserSearch
    {
        public int loggedUserId { get; set; } = 0;
        public int UserId { get; set; }
        public string? user { get; set; }
        public string? SearchDate { get; set; }
        public string? UserIp { get; set; } = string.Empty;  
        public string? sender { get; set; }
        public string? customer { get; set; }
    }
    public class PromoSummarySearch
    {
        public int loggedUserId { get; set; } = 0;
        public int UserId { get; set; }
        public string? user { get; set; }
        public string? SearchDate { get; set; }
        public string? UserIp { get; set; } = string.Empty;
        public string? sender { get; set; }  
        public string? customer { get; set; }
    }
    public class MapSenderSearch
    {        
        public int userId { get; set; }
        public string? UserIp { get; set; } = string.Empty;
        public bool status { get; set; }   
        public string? searchStr { get; set; }
        public bool DNSBypass { get; set; }
        public bool DNDBypass { get; set;}
    }
    public class SMSCSearch
    {        
        public int userId { get; set; }
        public string? UserIp { get; set; } = string.Empty;
        public bool status { get; set; }
        public string? searchStr { get; set; }
    }
    public class CustomerSearch
    {        
        public int userId { get; set; }
        public string? UserIp { get; set; } = string.Empty;
        public bool status { get; set; }
        public string? searchStr { get; set; }
    }
    public class UserSearch
    {        
        public int UserId { get; set; }
        public string? UserIp { get; set; } = string.Empty;
        public bool status { get; set; }
        public string? searchStr { get; set; }
    }
    public class DLRPercentageSearch
    {
        public string? fromDate { get; set; }
        public string? toDate { get; set; }  
        public string? sender { get; set; }        
        public int DLRCountryOperator { get; set; }
    }
    public class ServerTransactionSearch
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? UserId { get; set; }
        public string? Sender { get; set; }
        public string? MobileNo { get; set; }
        public string? UserIp { get; set; }
    }

}
