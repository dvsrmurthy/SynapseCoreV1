using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserDND
{
    public class ShowGridDNDOnResponse
    {
        public int DND_INT_ID{get;set;}
        public int DND_INT_CUSTOMERID{get;set;}
        public int DND_INT_USERID{get;set;}
        public int DND_INT_SENDERID{get;set;}
        public int DND_INT_SHORTCODEID{get;set;}
        public string? DND_VAR_MOBILE{get;set;}
        public string? DND_VAR_NAME{get;set;}
        public int DND_SINT_STATUS{get;set;}
        public string? DND_DTM_ADDEDON{get;set;}
        public string? SENDERNAME{get;set;}
    }
}
