using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class SenderConfigurationResponse
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int SenderId { get; set; }
        public string? Code { get; set; }
        public int Status { get; set; }

        public bool SMSChannel { get; set; }
        public bool EmailChannel { get; set; }
        public int CardBin { get; set; }
        public string? CardBinNo { get; set; }
        public int Module { get; set; }
        //public int BankId { get; set; }
        //public string? BankName { get; set; }

        public string? RejectNote { get; set; }
        public int CurrentStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBY { get; set; }
        public int NRETURN { get; set; }

    }

    public class GetSenderConfigurationResponseforedit
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int SenderId { get; set; }
        public bool SMSChannel { get; set; }
        public bool EmailChannel { get; set; }
        public string? Code { get; set; }
        public int Status { get; set; }
        public int CurrentStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBY { get; set; }
        public int NRETURN { get; set; }
        public int[] CardBin { get; set; }
        public int[] Module { get; set; }
        public int BankId { get; set; }
        // public int CardBinNo { get; set; }
    }

    public class SenderbyUserIdRes
    {
        public int Id { get; set; }
        public string? Code { get; set; }
    }
}
