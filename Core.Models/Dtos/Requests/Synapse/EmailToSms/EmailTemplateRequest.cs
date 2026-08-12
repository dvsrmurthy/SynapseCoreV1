using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.EmailToSms
{
    public class EmailTemplateRequest
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public string requestedby { get; set; }
        public int Createdby { get; set; }

        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public string Template { get; set; }

        public string Name { get; set; }
        public string FromAliasName { get; set; }
        public string FromName { get; set; }
        public string EmailAlias { get; set; }
        public string EmailSubject { get; set; }
        public string EmailFormat { get; set; }
        public int EmailType { get; set; }
        public int Currentstatus { get; set; }
        public string command { get; set; }
        public string EmailSignature { get; set; }
        public string TextEditor { get; set; }
        public string UserIp { get; set; }
    }
}
