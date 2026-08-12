using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AlertsManager
{
    public class BankInformationOnResponse
    {
        public int Id { get; set; }
        public string? BankCode { get; set; }
        public string? BankName { get; set; }
        public string? ShortDescription { get; set; }
        public int Status { get; set; }
        public string? RejectNote { get; set; }
        public int CurrentStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBY { get; set; }
        public int NRETURN { get; set; }
    }

    public class GetBankInformationResponseforedit
    {
        public int Id { get; set; }
        public string? BankCode { get; set; }
        public string? BankName { get; set; }
        public string? ShortDescription { get; set; }
        public int Status { get; set; }
        public string? RejectNote { get; set; }
        public int CurrentStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBY { get; set; }
        public int NRETURN { get; set; }
    }
}
