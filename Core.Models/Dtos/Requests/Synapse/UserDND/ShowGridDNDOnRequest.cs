using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserDND
{
    public class ShowGridDNDOnRequest
    {
        public int CUSTOMERID{get;set;}
        public int USERID{get;set;}
        public int DNDID{get;set;}
        public string MOBILENO{get;set;}
        public string NAME{get;set;}
        public int STATUS{get;set;}
        public int SEARCH{get;set;}
        public string SearchText { get; set; }
    }
}
