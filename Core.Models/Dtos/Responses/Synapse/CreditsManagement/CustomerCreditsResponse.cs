using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.CreditsManagement
{
    public class CustomerCreditsResponse
    {
        public int CustomerId { get; set; }
        public int CustomerCreditId { get; set; }
        public string Name { get; set; }
        public int AvailableCredits { get; set; }
        // public string CustExpDate { get; set; }
        //public string CustCreditExDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreditType { get; set; }
        public int TransactionType { get; set; }
        public int TransactionCredits { get; set; }
        public int Status { get; set; }
        public int FStatus { get; set; }
        public int Totalcredits { get; set; }
        public string Date { get; set; }
        public int ThresholdLimit { get; set; }
        public int AutoResetCredits { get; set; }
        public string Remarks { get; set; }
        //public string RejectionReason { get; set; }        
    }
    public class CustomerCreditsResponse_1
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int AvailableCredits { get; set; }
        //public string CustExpDate { get; set; }
        public string CreditType { get; set; }
        public int ThresholdLimit { get; set; }
        public int AutoResetCredits { get; set; }
        public int ParentId { get; set; }
    }
    public class CustomerDetails
    {
        public int CustomerId { get; set; }
        public int TransactionType { get; set; }
        public int FStatus { get; set; }
        public int CustomerCreditId { get; set; }
        public string Name { get; set; }
        public int AvailableCredits { get; set; }
        //public string ExpDate { get; set; }
        // public string ExDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreditType { get; set; }
        
        public string Remarks { get; set; }
    }

    public class GetCustomersOnResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreditType { get; set; }
        //public string custtype { get; set; }
        //public bool status { get; set; }
        //public int CreatedBy { get; set; }
    }
}
