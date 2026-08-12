using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.MailBox
{
    public class MailBoxConfigurationResponse
    {
        public int Id { get; set; }
        public string? Host { get; set; }
        public string? MailBox { get; set; }
        public string? Password { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public int Frequency { get; set; }
        public int MailType { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
        public int CurrentStatus { get; set; }
       
    }
    public class CheckerMailBoxConfigurationResponce
    {
        public string? CurrentStatus { get; set; }
        public string? RejectReason { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
    }
}
