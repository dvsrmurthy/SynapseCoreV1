using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserManagement
{
    public class ApproveIPWhiteListResponse
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string? firstname { get; set; }
        public string? middlename { get; set; }
        public string? lastname { get; set; }
        public string? username { get; set; }
        public string? server { get; set; }
        public string? ip { get; set; }
        public bool status { get; set; }
        public int currentstatus { get; set; }
        //public bool LDap { get; set; }
        public int Fstatus { get; set; }     
    }
}
