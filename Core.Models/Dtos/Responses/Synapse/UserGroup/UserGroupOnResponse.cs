using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserGroup
{
    
     public class ShowGridGroupsOnResponse
     {
         public int GroupId { get; set; }
         public string GroupName { get; set; }
         public string GroupDesc { get; set; }
         public int GroupCreatedBy { get; set; }
         public int ContactsCount { get; set; }
         public int TotalContactsCount { get; set; }
         public int GroupStatus { get; set; }
         public int CurrentStatus { get; set; }
         public string GroupDon { get; set; }
     }
     public class CustomerPreferencesOnResponse
     {
         public int Id { get; set; }
         public int CustomerId { get; set; }
         public string Name { get; set; }
         public string Value { get; set; }
         public int PreferenceId { get; set; }
         public string PreferenceValue { get; set; }
         public int CreatedBy { get; set; }
         public DateTime CreatedOn { get; set; }
     }
}
