using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserCampaigns
{
   public class GroupContactsMain
    {
       public List<GroupswithContacts> GroupswithContacts { get; set; }
    }

   public class GroupswithContacts 
   {
       public int GroupId { get; set; }
       public List<GroupContacts> GroupContacts { get; set; }
   
   }
   public class GroupContacts
   {
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string MobileNo { get; set; }
       public string Email { get; set; }
   }
}
