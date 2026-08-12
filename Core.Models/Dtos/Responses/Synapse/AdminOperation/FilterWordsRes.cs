using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class FilterWordsRes
    {
        public int Id { get; set; }
        public string Word { get; set; }
       // public string ReplaceWord { get; set; }
        public int Status { get; set; }
        public string CreatedOn { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectNote { get; set; }
    }

    public class CheckerFilterWordsResponse
    {

        public string CurrentStatus { get; set; }
        public string RejectNote { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }

    }
}
