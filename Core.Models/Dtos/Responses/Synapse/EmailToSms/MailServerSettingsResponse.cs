using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.EmailToSms
{
    public class MailServerSettingsResponse
    {
        public string? Id{ get; set; }
       
        public string? Mailbox { get; set; }
        public string? Server { get; set; }
        public string? Port { get; set; }
        public string? Encryption { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool Status { get; set; }
        public int CurrentStatus { get; set; }
        public int Interval { get; set; }
        public int ServerType { get; set; }
        public int CreatedBy { get; set; }
        public string? ReplyMailBox { get; set; }
        public string? ReplyMailBoxName { get; set; }
        public string? ReplyMailTemplate { get; set; }
    }
}
