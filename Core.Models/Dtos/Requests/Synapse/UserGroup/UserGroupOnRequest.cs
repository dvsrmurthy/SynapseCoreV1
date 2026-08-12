using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserGroup
{
   
        public class ShowGridGroupsOnRequests
        {
            public int ID { get; set; }
            public int CREATEDBY { get; set; }
            public EventStatus STATUS { get; set; }
            public string RequestPage { get; set; }
            public Cstatus CurrentStatus { get; set; }
            public int GroupUpdatedBy { get; set; }
            public string UserIp { get; set; }
            public int CustomerId { get; set; }   //property added on 11-03-2017
            public string SearchText { get; set; }
        }

        public class SaveGroupsOnRequests
        {
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public string Description { get; set; }
            public int GroupActionBy { get; set; }
            //public int GroupUpdatedBy { get; set; }
            public bool Status { get; set; }
            public int CurrentStatus { get; set; }
            public SqlEventTypes EventType { get; set; }
            public string UserIp { get; set; }
        }


        public class ChangeStatusOnRequests
        {
            public int GroupId { get; set; }
            public int Status { get; set; }
            public int GroupUpdatedBy { get; set; }
            public string UserIp { get; set; }
        }

    public class CustomerPreferencesOnRequests
    {
        public int CustomerId { get; set; }
    }
    public class GroupUsersCountOnRequests
    {
        public int CustomerId { get; set; }
        public int NType { get; set; }
        public int NCount { get; set; }
    }
}
