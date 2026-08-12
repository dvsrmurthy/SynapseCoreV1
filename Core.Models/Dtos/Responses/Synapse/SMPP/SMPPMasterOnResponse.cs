using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMPP
{
    #region SMPPMASTER
    public class SMPPMasterOnResponse
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public int AutoId { get; set; }
        public int UserId { get; set; }
        public int CustomerID { get; set; }
        public string? AddRange { get; set; }
        public int AddRangeNPI { get; set; }
        public int AddRangeTON { get; set; }
        public int EnquiryInterval { get; set; }
        public int Port { get; set; }
        public string? SystemType { get; set; }
        public int Version { get; set; }
        public int Status { get; set; }
        public int Mode { get; set; }
        public string? IPaddress { get; set; }
        public int Session { get; set; }
        public int TON { get; set; }
        public int Charset { get; set; }
        public int DCS { get; set; }
        public int NPI { get; set; }
        public int Throughput { get; set; }
        public string? SenderId { get; set; }
        public int StaticDynamic { get; set; }
        public string? Shortcode { get; set; }
        public string? ShortcodeOut { get; set; }
        public int SmppStage { get; set; }

        public int CurrentStatus { get; set; }


        public int SessionDistrb { get; set; }
        public int Transmeter { get; set; }
        public int Transiver { get; set; }
        public int Receiver { get; set; }
        public string? Instance { get; set; }
        public int AssembleLongMessages { get; set; }
    }
    public class GetInstanceResponseSMPP
    {
        public int Id { get; set; }
        public string? SMSC_ESMEGroupInstance { get; set; }
    }
    public class GetUsersSMPPMasterOnResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public bool Smpp { get; set; }
        public bool Status { get; set; }
    }
    public class GetCustomerSMPPMasterOnResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    #endregion
    #region SMPPIPALLOCATION

    public class GetSmppMasterIPAllocationOnResponse
    {
        public int AutoId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? IPaddress { get; set; }
        public int Session { get; set; }
        public int Status { get; set; }
    }
    public class GetSmppIPAllocationOnResponse
    {
        public int AutoID { get; set; }
        public int Row { get; set; }
        public int CustomerID { get; set; }
        public int UserId { get; set; }
        public string? VALUE { get; set; }
        public int LEN { get; set; }
        public int SMPPID { get; set; }
        public bool Status { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectReason { get; set; }
    }

    #endregion
}
