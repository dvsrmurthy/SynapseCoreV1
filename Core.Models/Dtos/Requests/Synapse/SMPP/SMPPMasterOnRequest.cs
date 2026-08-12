using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMPP
{
    #region SMPPMASTER
    public class SMPPMasterOnRequest
    {
        public int SMPPUSERID { get; set; }
        public int STATUS { get; set; }
        public int AUTOID { get; set; }
        public string? UserIp { get; set; }
        public int UserId { get; set; }
    }
    public class GetUsersSMPPMasterOnRequest
    {
        public int CUSTOMERID { get; set; }
        public int SEARCHVALUE { get; set; }
        public string? SEARCHTEXT { get; set; }
    }
    public class GetCustomerSMPPMasterOnRequest
    {
        public int CUSTOMERID { get; set; }
        public int STATUS { get; set; }
        public int CUSTTYPE { get; set; }
        public int CREATEDBY { get; set; }
        public string? REQUESTEDBY { get; set; }

        public string? SEARCHTEXT { get; set; }
    }
    public class InsertSMPPMasterOnRequest
    {
        public int AutoID { get; set; }
        public int CustomerID { get; set; }
        public int SMPPUSERID { get; set; }
        public string? ADDRANGE { get; set; }
        public int ADDRANGENPI { get; set; }
        public int ADDRANGETON { get; set; }
        public int ENQUIRY { get; set; }
        public int PORT { get; set; }
        public string? SYSTEMTYPE { get; set; }
        public int VERSION { get; set; }
        public int STATUS { get; set; }
        public int MODE { get; set; }
        public string? IPADDRESS { get; set; }
        public int SESSION { get; set; }
        public int SOURCETON { get; set; }
        public int CHARSET { get; set; }
        public int DCS { get; set; }
        public int SOURCENPI { get; set; }
        public int THROUGHPUT { get; set; }
        public int CurrentStatus { get; set; }

        public int SessionDistrb { get; set; }
        public int Transmeter { get; set; }
        public int Transiver { get; set; }
        public int Receiver { get; set; }
        public string? Instance { get; set; }
        public int AssembleLongMessages { get; set; }
        public string? UserIp { get; set; }

    }
    public class ChangeStatusSMPPMasterOnRequest
    {
        public int AUTOID { get; set; }
        public int STATUS { get; set; }
        public int UPDATEDBY { get; set; }
        public int CURRENTSTATUS { get; set; }
        public string? UserIp { get; set; }
    }
    public class CheckerUpdateSMPPMasterOnRequest
    {
        public string? AUTOID { get; set; }
        public string? RejectReason { get; set; }
        public int UPDATEDBY { get; set; }
        public int CURRENTSTATUS { get; set; }
    }
    #endregion
    #region SMPPIPALLOCATION

    public class GetSmppMasterIPAllocationOnRequest
    {
        public int AUTOID { get; set; }
        public int USERID { get; set; }
        public int CUSTID { get; set; }
        public int STATUS { get; set; }
    }
    public class GetSmppIPAllocationOnRequest
    {
        public int SMPPID { get; set; }
        public int CUSTID { get; set; }
        public int USERID { get; set; }
        public int SEARCHVAL { get; set; }
        public string? UserIp { get; set; }
        //public bool status { get; set; }
    }
    public class SetSmppIPAllocationOnRequest
    {
        public int AutoID { get; set; }
        public int CustomerID { get; set; }
        public int USERID { get; set; }
        public string? IPAddress { get; set; }
        public int NoOfSession { get; set; }
        public int SMPPID { get; set; }
        public int Status { get; set; }
        public int CurrentStatus { get; set; }
        public List<SetSmppIPAllocationOnRequest> SMPPIPS { get; set; }
        public string? RejectReason { get; set; }
        public int InsertCheckerFlag { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsCheckerRequired { get; set; }
        public string? UserIp { get; set; }
    }
    public class CheckerUpdateSMPPIPOnRequest
    {
        public int AUTOID { get; set; }
        public string? RejectReason { get; set; }
        public int UPDATEDBY { get; set; }
        public int CURRENTSTATUS { get; set; }
    }

    #endregion
}
