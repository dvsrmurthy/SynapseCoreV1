using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AlertsManager
{
     public  class ProfileCreationOnResponse
    {
    }
    public class DefaultDBsOnResponse
    {
        public string DBName { get; set; }
    }
    public class GetProfileOnResponse
    {
      public int Id{ get ; set ;}
      public string Profile	{ get ; set ; }
      public string DBtype { get; set; }
      public string ServerName { get; set; }
      public string UserName { get; set; }
      public string Password { get; set; }
      public string Port { get; set; }
      public string DBname { get; set; }
      public string ConnectionString { get; set; }
      public string Status { get; set; }
      public string CreatedBy { get; set; }
      public string UserType { get; set; }
      //public DateTime CreatedOn { get; set; }
      public int currentstatus { get; set; }
      public string rejectreason { get; set; }
      public string Name { get; set; }
      public int cnt { get; set; }
      public Cstatus Fstatus { get; set; }
      public bool IsCheckerRequired { get; set; }
      public int ConnectivityType { get; set; }
    }
    public class GetEditProfilesOnResonse
    {
        public int Id { get; set; }
        public string Profile { get; set; }
        public int DBtype { get; set; }
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string DBname { get; set; }
        public int UserType { get; set; }
      
    }
}
