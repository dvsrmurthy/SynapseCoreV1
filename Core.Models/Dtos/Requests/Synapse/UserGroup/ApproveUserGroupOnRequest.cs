using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserGroup
{
    public class ApproveUserGroupOnRequest
    {
        public int GroupId { get; set; }
        public int CurrentStatus { get; set; }
        public int GroupUpdatedBy { get; set; }
        public string? RejectionReason { get; set; }
    }
}
