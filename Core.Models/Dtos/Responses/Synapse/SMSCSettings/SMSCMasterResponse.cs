using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class GetSMSCINTLDetailsResponse
    {
        public int Id { get; set; }
        //public int SMPPID { get; set; }
        public int HttpSmppID { get; set; }
        public string? SMSCName { get; set; }
        public string? SMSCDesc { get; set; }
        public int ConnectionType { get; set; }
        public string? VendorName { get; set; }
        public int VendorId { get; set; }
        public int StageNumber { get; set; }
        public bool SMSCStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UserIntiatedStatus { get; set; }
        public bool BoundStatus { get; set; }
        public int CurrentStatus { get; set; }
    }
    public class GetINTLVendorsResponse
    {
        public int Id { get; set; }
        public string? VendorName { get; set; }
        public bool Status { get; set; }
        public string? CreatedBy { get; set; }

    }
    public class GetCountryCodesResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CountryCode { get; set; }
        public int Status { get; set; }
    }
    public class GetOperatorsResponse
    {

        public int OPRID { get; set; }
        public int OPRCNTRY { get; set; }
        public int CNTID { get; set; }
        public string? CNTNAME { get; set; }
        public string? OPRNAME { get; set; }
        public string? OPRDESC { get; set; }
        public bool OPRSTS { get; set; }
        //public DateTime OPRADON { get; set; }
        public int CurrentStatus { get; set; }

    }
    public class GetRouteStagesResponse
    {
        public int Id { get; set; }
        public string? Stages { get; set; }
    }
    public class GetInstanceResponse
    {
        public int Id { get; set; }
        public string? SMSC_ESMEGroupInstance { get; set; }
    }
    public class GetIntlSMSCIdResponse
    {
        public int Nuniqueid { get; set; }
    }
    public class GetConnectionsResponse
    {
        public int Id { get; set; }
        public int SmscId { get; set; }
        public string? HostDetails { get; set; }
        public int Port { get; set; }
        public string? SystemId { get; set; }
        public string? Password { get; set; }
        public int BindMode { get; set; }
        public bool AutoConnectStatus { get; set; }
        public int AlivePeriod { get; set; }
        public int ServerTimeOut { get; set; }
        public bool LogStatus { get; set; }
        public bool DeliveryRptStatus { get; set; }
        public string? ProtocolVersion { get; set; }
        public int CharSet { get; set; }
        public int DCS { get; set; }
        public int ThroughPut { get; set; }
        public string? SystemType { get; set; }
        public string? ExpirationDate { get; set; }
        public int DestTon { get; set; }
        public int DestNpi { get; set; }
        public int SourceTon { get; set; }
        public int SourceNpi { get; set; }
        public string? SourceAddress { get; set; }
        public string? SMSCName { get; set; }
        public string? SMSCDesc { get; set; }
        public int ConnectionType { get; set; }
        public string? VendorName { get; set; }
        public int VendorId { get; set; }
        public int StageNumber { get; set; }
        public bool SMSCStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UserIntiatedStatus { get; set; }
        public bool BoundStatus { get; set; }
        public int HTPSID { get; set; }
        public string? HTPSNAME { get; set; }
        public string? HTPSVRNAME { get; set; }
        public int HTPPORT { get; set; }
        public int HTPUSER { get; set; }
        public string? HTPPWD { get; set; }
        public string? HTPURL { get; set; }
        public string? HTPUNICDE { get; set; }
        public string? HTPSUCRESP { get; set; }
        public string? HTPERRSP { get; set; }
        public int HTPSMSLT { get; set; }
        public int HTPUNIT { get; set; }
        public int HTPST { get; set; }
        public int CreateBy { get; set; }
        public int CurrentStatus { get; set; }
        public bool BoundOtherSource { get; set; }
        public int Sessions { get; set; }
        public int Transciever { get; set; }
        public int Receiver { get; set; }
        public int Transmitter { get; set; }
        public string? Instance { get; set; }
    }

    public class GetSMSCDetailsEdit
    {
        public int Id { get; set; }
        public string? SMSCName { get; set; }
        public string? SMSCDesc { get; set; }
        public int ConnectionType { get; set; }
        public string? VendorName { get; set; }
        public int VendorId { get; set; }
        public int StageNumber { get; set; }
        public bool SMSCStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UserIntiatedStatus { get; set; }
        public bool BoundStatus { get; set; }
        public int CurrentStatus { get; set; }
        public bool BoundOtherSource { get; set; }
    }

    public class GetSMSCSmppDetailsEdit
    {
        public int Id { get; set; }
        public int SmscId { get; set; }
        public int CurrentStatus { get; set; }
        public string? HostDetails { get; set; }
        public int Port { get; set; }
        public string? SystemId { get; set; }
        public string? Password { get; set; }
        public int BindMode { get; set; }
        public bool AutoConnectStatus { get; set; }
        public int AlivePeriod { get; set; }
        public int ServerTimeOut { get; set; }
        public bool LogStatus { get; set; }
        public bool DeliveryRptStatus { get; set; }
        public string? ProtocolVersion { get; set; }
        public int CharSet { get; set; }
        public int DCS { get; set; }
        public int ThroughPut { get; set; }
        public string? SystemType { get; set; }
        public int DestTon { get; set; }
        public int DestNpi { get; set; }
        public int SourceTon { get; set; }
        public int SourceNpi { get; set; }
        public string? SourceAddress { get; set; }
        public int CreatedBy { get; set; }
        public string? ExpirationDate { get; set; }
        public int nBoundOtherSource { get; set; }
        public int Sessions { get; set; }
        public int Transciever { get; set; }
        public int Receiver { get; set; }
        public int Transmitter { get; set; }
        public string? Instance { get; set; }
    }
    public class GetSMSCHttpDetailsEdit
    {
        public int Id { get; set; }
        public int HTPSID { get; set; }
        public string? HTPSNAME { get; set; }
        public string? HTPSVRNAME { get; set; }
        public int HTPPORT { get; set; }
        public string? HTPUSER { get; set; }
        public string? HTPPWD { get; set; }
        public string? HTPURL { get; set; }
        public string? HTPUNICDE { get; set; }
        public string? HTPSUCRESP { get; set; }
        public string? HTPERRSP { get; set; }
        public int HTPSMSLT { get; set; }
        public int HTPUNIT { get; set; }
        public int HTPST { get; set; }
        public int CurrentStatus { get; set; }
        public int CreatedBy { get; set; }
    }
}