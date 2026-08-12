using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.StatusMonitor
{
    public class StatusCustUserResponse
    {
        public string Date { get; set; }
        public string Customer { get; set; }
        public int IncomingUserId { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public int ReceivedCount { get; set; }
        public int SentCount { get; set; }
        public string ModuleName { get; set; }
    }
    public class PromoSummaryResponse
    {
        public string Date { get; set; }
        public string Customer { get; set; }
        public int IncomingUserId { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public int ReceivedCount { get; set; }
        public int SentCount { get; set; }
        public string ModuleName { get; set; }
    }
    public class StatusMapSenderResponse
    {
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string DisplaySender { get; set; }
        public string DespatchType { get; set; }
        public string countryName { get; set; }
        public string DNSBypass { get; set; }
        public string DNDBypass { get; set; }
        public string smscid { get; set; }
        public string SMSCName { get; set; }
        public string status { get; set; }
        public string Operator { get; set; }
        public string Vendor { get; set; }
        public string RouteId { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
    public class StatusSMSCResponse
    {
        public int SMSCID { get; set; } = 0;
        public string SMSCName { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
        public string SMSCStatus { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string SystemId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ThroughPut { get; set; } = string.Empty;
        public string DTon { get; set; } = string.Empty;
        public string DNpi { get; set; } = string.Empty;
        public string STon { get; set; } = string.Empty;
        public string SNpi { get; set; } = string.Empty;
        public string Sessions { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;
        public string DCS { get; set; } = string.Empty;
        public string Transmitter { get; set; } = string.Empty;
        public string Transciever { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public string SystemType { get; set; } = string.Empty;
    }
    public class StatusCustomerResponse
    {
        public int CustomerId { get; set; } = 0;
        public string Customer { get; set; } = string.Empty;
        public string AccountManager { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreditType { get; set; } = string.Empty;
        public string AvailableCredits { get; set; } = string.Empty;
        public string CreatedOn { get; set; } = string.Empty;
        public string ParentCustomer { get; set; } = string.Empty;
    }

    public class StatusUserResponse
    {
        public int Userid { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool status { get; set; } = false;
        public string Customer { get; set; } = string.Empty;
        public string ParentCustomer { get; set; } = string.Empty;
        public bool http { get; set; } = false;
        public bool web { get; set; } = false;
        public bool smpp { get; set; } = false;
        public bool smtp { get; set; } = false;
        public string CreatedOn { get; set; } = string.Empty;        
    }

    public class CountryResponse
    {
        public List<CountryTableOne> CountryTableOnes { get; set; }
        public List<CountryTableTwo> CountryTableTwos { get; set; }
        public List<CountryTableThree> CountryTableThrees { get; set; }
    }
    public class CountryTableOne
    {
        public string IncomingUserId { get; set; }
        public string UserName { get; set; }
        public string InboundSender { get; set; }
        public string countrycode { get; set; }
        public string CountryName { get; set; }
        public string SMSCount { get; set; }
    }
    public class CountryTableTwo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public string countrycode { get; set; }
        public string CountryName { get; set; }        
        public string Delivrd { get; set; }
        public string Undeliv { get; set; }
        public string DeliveryPercent { get; set; }
    }
    public class CountryTableThree
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public string countrycode { get; set; }
        public string CountryName { get; set; }        
        public string DlvrdStatus { get; set; }
        public string DLRCount { get; set; }
    }
    public class OperatorResponse
    {
        public List<OperatorTableOne> OperatorTableOnes { get; set; }
        public List<OperatorTableTwo> OperatorTableTwos { get; set; }
        public List<OperatorTableThree> OperatorTableThrees { get; set; }
    }
    public class OperatorTableOne
    {
        public string IncomingUserId { get; set; }
        public string UserName { get; set; }
        public string InboundSender { get; set; }
        public string countrycode { get; set; }
        public string CountryName { get; set; }
        public string SMSCount { get; set; }
    }
    public class OperatorTableTwo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public string countrycode { get; set; }
        public string CountryName { get; set; }
        public string Series { get; set; }
        public string Delivrd { get; set; }
        public string Undeliv { get; set; }
        public string DeliveryPercent { get; set; }
    }
    public class OperatorTableThree
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; }
        public string countrycode { get; set; }
        public string CountryName { get; set; }
        public string Series { get; set; }
        public string DlvrdStatus { get; set; }
        public string DLRCount { get; set; }
    }
    public class StatusSrvrTransactionResponse
    {
        public string UserName { get; set; }
        public string Sender { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string ReceivedDate { get; set; }
        public string Credits { get; set; }
        public string DataCode { get; set; }
        public string CharCount { get; set; }
        public string ModuleName { get; set; }
        public string ClientPrId { get; set; }
        public int IncomingUserId { get; set; }
    }
}
