using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.EmailToSms
{
    public class EmailTemplateResponse
    {
        public int Id { get; set; }
        public string Template { get; set; }
        public string Name { get; set; }
        public string Customer { get;set;}
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string FromName { get; set; }
        public string EmailAlias { get; set; }
        public string EmailSubject { get; set; }
        public int EmailType { get; set; }
        public string EmailFormat { get; set; }
        public string EmailSignature { get; set; }
        public bool Status { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectReason { get; set; }
        public int CreatedBy { get; set; }
        public string Createdon { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string TextEditor { get; set; }
        public string MailBox { get; set; }
        public int IsMapped { get; set; }
    }
}
