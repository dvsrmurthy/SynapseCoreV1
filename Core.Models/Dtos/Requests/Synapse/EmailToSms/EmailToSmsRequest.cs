using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.EmailToSms
{
    public class EmailToSmsRequest
    {
        public string ID { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string Mailbox { get; set; }
      //  public string ServerDetails { get; set; }
      //  public string Port { get; set; }
       // public string Encryption { get; set; }
      //  public string EmailUserName { get; set; }
      //  public string Password { get; set; }
        public string SenderId { get; set; }
        public string StartTag { get; set; }
        public string EndTag { get; set; }
        public string Interval { get; set; }
        public string WhiteListEmailIds { get; set; }
        public string TemplateId { get; set; }
        public string FromEmail { get; set; }
        public string Formats { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int Currentstatus { get; set; }
        public string requestedby { get; set; }
        public string command { get; set; }
        public string SenderTag { get; set; }
        public string MessageTag { get; set; }
        public int Template { get; set; }
        public string Domain { get; set; }
        public string UserIp { get; set; }
        public string Originator { get; set; }
        public string ReplyMailTemplate { get; set; }
    }
}
