using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.EmailToSms
{
    public class EmailToSmsResponse
    {
        public string? ID { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string? Mailbox { get; set; }
        public string? FromEmail { get; set; }
      //  public string? ServerDetails { get; set; }
      //  public string? Port { get; set; }
      //  public string? Encryption { get; set; }
      //  public string? EmailUserName { get; set; }
     //   public string? Password { get; set; }
        public int SenderId { get; set; }
        public string? Code { get; set; }
        //public int TemplateId { get; set; }
        public string? Formats { get; set; }
        public string? StartTag { get; set; }
        public string? EndTag { get; set; }
        public string? SenderTag { get; set; }
        public string? MessageTag { get; set; }
        public int Interval { get; set; }
        public string? WhiteListEmailIds { get; set; }
        public bool Status { get; set; }
        public int Template { get; set; }
        public string? CustomerName { get; set; }
        public string? UserName { get; set; }
        public string? TemplateName { get; set; }
        public int CurrentStatus { get; set; }
        public string? MailBoxName { get; set; }
        public string? Domain { get; set; }
        public string? RMailbox { get; set; }
        public string? ReplyMailBox { get; set; }
        public string? ReplyMailTemplate { get; set; }
        public string? RMailTemplate { get; set; }
    }
}
