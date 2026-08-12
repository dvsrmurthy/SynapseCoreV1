using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AlertsManager
{
    public class ProfilesCreationOnRequest
    {
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string Dbtype { get; set; }
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Port { get; set; }
        public string DefaultDb { get; set; }
        public string Connectionstring { get; set; }
        public int UserId { get; set; }
        public int CurrentStatus { get; set; }
        public int NReturn { get; set; }
        public string UserIp { get; set; }
        public int ConnectType { get; set; }
    }
    public class TestConnectionOnRequest
    {
        public string strservername { get; set; }
       // public string strconnection { get; set; }
        public string strusername { get; set; }
        public string strpassword { get; set; }
        public string strport { get; set; }
        public string strdbname { get;set; }
        public string strtype { get; set; }
        public string struserType { get; set; }
        public int ConnectType { get; set; }
    }
    public class GetProfilesOnRequest
    {
        public int NId { get; set; }
        public int NStatus { get; set; }
        public int NCreatedBy { get; set; }
        public string RequestedBy { get; set; }
        public int NRetrval { get; set; }
        public int customer { get; set; }
        public string UserIp { get; set; }
    }  
    public class GetEditProfileOnRequest
    {
        public int NId { get; set; }
        public int NStatus { get; set; }
        public int NRetrval { get; set; }
    }
    public class UpdateProfileStatusOnRequest
    {
        public int NId { get; set; }
        public int NStatus { get; set; }
        public int NRetrval { get; set; }
        public int NUpdatedBy { get; set; }
        public string UserIp { get; set; }
    }
    public class ApproveRejectProfileCreation
    {
        public int ProfileId { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int UpdatedBy { get; set; }
        public string Rejectreason { get; set; }
        public int ReturnValue { get; set; }
    }
}
