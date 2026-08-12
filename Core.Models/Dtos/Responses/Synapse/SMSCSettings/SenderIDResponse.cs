using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class SenderIDResponse
    {
        public int Id {get; set;}

        public int Type {get; set;}

        public string Code {get; set;}

        public string Description {get; set;}

        public bool Status {get; set;}

        public string DispatchSenderId {get; set;}

        public string DisplaySenderId {get; set;}
        
        public int SenderType { get; set; }

        public int ShortType { get; set; }

        public bool DndByPass { get; set; }

        public int CurrentStatus { get; set; }

        public int Fstatus { get; set; }

        public string RejectReason { get; set; }
        
    }
}
