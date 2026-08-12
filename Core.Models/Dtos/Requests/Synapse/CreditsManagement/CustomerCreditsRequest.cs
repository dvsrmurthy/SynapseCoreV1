using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.CreditsManagement
{
   public class CustomerCreditsRequest
    {
        public int NID { get; set; }
        public int NCUSTID { get; set; }
        public int NCREATEDBY { get; set; }
        public EventStatus STATUS { get; set; }
        public string RequestPage { get; set; }
        public string UserIp { get; set; }
        public string SearchText { get; set; }
    }
    public class ShowGridCustomerCreditsOnRequest
    {
        public int NID { get; set; }
        public int CustomerId { get; set; }
        public int CustomerCreditId { get; set; }
        public string CustomerName { get; set; }
        public int AvailableCredits { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CreditType { get; set; }
        public int TransactionType { get; set; }
        public bool Status { get; set; }
        public int FStatus { get; set; }
        public int TopUpCredits { get; set; }
        public string Remarks { get; set; }
        public string RequestPage { get; set; }
        public CustomerTypes CustomerType { get; set; }
        public string StrSearch { get; set; }
        public int PreviousCredits { get; set; }
        public int GroupActionBy { get; set; }
        public SqlEventTypes EventType { get; set; }
        public string UserIp { get; set; }
    }
    public class SaveCustomerCreditsOnRequest
    {
        public int CustomerId { get; set; }
        public int TransactionType { get; set; }
        public int FStatus { get; set; }
        public SqlEventTypes EventType { get; set; }
        public bool Status { get; set; }
        public int GroupActionBy { get; set; }
        public string Remarks { get; set; }
        public int CustomerCreditId { get; set; }
        public string UserIp { get; set; }
        public string CreditType { get; set; }
        public int AvailableCredits { get; set; }
        //public DateTime ExpiryDate { get; set; }
     //   public DateTime CustExpiryDate { get; set; }
        public int CreatedBy { get; set; }
        //public int PreviousCredits { get; set; }
        public int TopUpCredits { get; set; }
        public int ThresholdLimit { get; set; }
        public int AutoResetCredits { get; set; }
    }

    //public class GetCustomersOnRequest
    //{
    //    public int CustomerId { get; set; }
    //    public int status { get; set; }
    //    public int CreatedBy { get; set; }
    //    public string RequestBy { get; set; }
    //}
}
