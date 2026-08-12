using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.EmailToSms
{
    public class MailServerSettingsRequest
    {
        public string Id { get; set; }
        public string Mailbox { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string Encryption { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int Currentstatus { get; set; }
        public string requestedby { get; set; }
        public string command { get; set; }
        public string Interval { get; set; }
        public int ServerType { get; set; }
        public int UserId { get; set; }
        public string UserIp { get; set; }
        public string ReplyMailBox { get; set; }
        public string ReplyMailTemplate { get; set; }
    }
}
