using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AlertsManager
{
    public class GetTemplateDetailsRequest
    {
        public int Tempid { get; set; }
        public int Status { get; set; }
        public int CreatedByUser { get; set; }
        public int Return { get; set; }
        public string RequestPage { get; set; }
        public int customer { get; set; }
        public string UserIp { get; set; }
    }
    public class SetTemplatesRequest
    {
        public string NTMPID { get; set; }
        public string NUSERID { get; set; }
        public string NCUSTOMERID { get; set; }
        public string STRTMPNAME { get; set; }
        public string STRTMPDESC { get; set; }
        public string NTYPE { get; set; }
        public string NLANG { get; set; }
        public string STRCOLS { get; set; }
        public string STRMSG { get; set; }
        public int NCREATEDBY { get; set; }
        public string NCREATEDBYUSER { get; set; }
        public int NSTATUS { get; set; }
        public int NUpdatedBY { get; set; }
        public string NUpdatedBYUSER { get; set; }
        //public int NROLETYPE { get; set; }
        public int NRETURN { get; set; }
        public int CurrentStatus { get; set; }
        public string SMSType { get; set; }
        public string EmailType { get; set; }
        public string TextEditor { get; set; }
        public string UserIp { get; set; }
    }
    public class ChangeTemplateStatusRequest
    {
        public int NID { get; set; }
        public int NSTATUS { get; set; }
        public int NUPDATEDBY { get; set; }
        public string NUPDATEDUSER { get; set; }
        public int NRETURN { get; set; }
        public string UserIp { get; set; }
    }
    public class ApproveRejectTemplateCreation
    {
        public int TempId { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int UpdatedBy { get; set; }
        public string Rejectreason { get; set; }
        public int ReturnValue { get; set; }
    }
    public class CheckMessageTemplatesRequest
    {
        public int NRETURN { get; set; }
    }
    #region UserMapping
    public class GetTemplateUserMapDetailsRequest
    {
        public int Tempid { get; set; }
        public int Status { get; set; }
        public int CreatedByUser { get; set; }
        public int Return { get; set; }
        public string RequestPage { get; set; }
    }
    public class ChangeTemplateUserMapStatusRequest
    {
        public int NID { get; set; }
        public int NSTATUS { get; set; }
        public int NUPDATEDBY { get; set; }
        public string NUPDATEDUSER { get; set; }
        public int NRETURN { get; set; }
    }
    public class GetCustomersDetailsRequest
    {
        public string STRName { get; set; }
        public int NSEARCH { get; set; }
        public int NId { get; set; }
        public int nCustType { get; set; }
        public int nCreatedby { get; set; }
        public string requestedby { get; set; }
        public int NRETVAL { get; set; }
    }
    public class GetUsersDetailsRequest
    {
        public int NUSERID { get; set; }
        public int NSTATUS { get; set; }
        public int NCUSTID { get; set; }
        public int NCREATEDBY { get; set; }
        public string requestedby { get; set; }
        public int NRETVAL { get; set; }
    }
    public class SetTemplateUserMappingRequest
    {
        public int NTMPID { get; set; }
        public string STRTEMPID { get; set; }
        public int NCUSTID { get; set; }
        public int STRUSERID { get; set; }
        public int NCREATEDBY { get; set; }
        public string NCREATEDBYUSER { get; set; }
        public int NSTATUS { get; set; }
        public int CurrentStatus { get; set; }
        public int NRETURN { get; set; }
        public int NUpdatedBY { get; set; }
        public string NUpdatedBYUSER { get; set; }
    }
    #endregion
}
