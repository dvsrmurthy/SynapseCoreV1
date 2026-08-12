using Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class GetSMSCINTLDetailsRequest
    {
        public int nSid { get; set; }
        public int nCreatedby { get; set; }
        public int nStatus { get; set; }
        public string RequestPage { get; set; }
        public string UserIp { get; set; }
        //public int nVenderId { get; set; }
    }
    public class GetINTLVendorsRequest
    {
        public int NVENDOR { get; set; }
        public int NSTATUS { get; set; }
        public int NCREATEDBY { get; set; }
        public int NRETVAL { get; set; }
        public string RequestPage { get; set; }
    }

    public class GetRouteStagesRequest
    {
        public int NRETVAL { get; set; }
    }
    public class GetCountryCodesRequest
    {
        public int NID { get; set; }
        public int NSTATUS { get; set; }
        public int nCreatedBy { get; set; }
        public string RequestPage { get; set; }
        // public int nretval { get; set; }
    }
    public class GetOperatorsRequest
    {
        public int @createdby { get; set; }
        public string RequestPage { get; set; }

    }

    public class CheckerUpdateUserSMSCMasterRequest
    {
        public int SMSCId { get; set; }
        //public int SmppProtocolId { get; set; }
        //public int HttpProtocolId { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int UpdatedBy { get; set; }
        public string Rejectreason { get; set; }
        public int ReturnValue { get; set; }
    }
    public class UpdateSMSCINTLStatusRequest
    {
        public string strSMSCIds { get; set; }
        public int createdby { get; set; }
        public int nStatus { get; set; }
        public int nReturn { get; set; }
        public string chgstatus { get; set; }
        public string UserIp { get; set; }
    }
    public class SetSMSCINTLDetailsRequest
    {
        public int nId { get; set; }
        public string strSMSCName { get; set; }
        public string strSMSCDesc { get; set; }
        public int strConType { get; set; }
        public int VenderID { get; set; }
        public int VenderId { get; set; }
        public int Stage { get; set; }
        public int nOutSMSCId { get; set; }
        public int nUserid { get; set; }
        public int nReturn { get; set; }
        public string strSMSCIds { get; set; }
        public int nStatus { get; set; }
        public int nStatus_Bind { get; set; }
        // public int nReturn { get; set; }
        public string UserIp { get; set; }
    }
    public class SetConnectionsHTTPRequest
    {
        public int nId { get; set; }
        public int nsmscid { get; set; }
        public string strConnName { get; set; }
        public string strServerName { get; set; }
        public int nPort { get; set; }
        public int strUserId { get; set; }
        public string strPwd { get; set; }
        public string strURLText { get; set; }
        public string strURLUniCode { get; set; }
        public string strSuccessResp { get; set; }
        public string strErrResp { get; set; }
        public int nSMSLimit { get; set; }
        public int nUnit { get; set; }
        public int nUserid { get; set; }
        public int nUniqueid { get; set; }
        public int nReturn { get; set; }
        public string UserIp { get; set; }
    }
    public class SetConnectionsSMPPRequest
    {
        public int smpphttpid { get; set; }
        public string strsmpphttpid { get; set; }
        public int httpid { get; set; }
        public string strSMSCName { get; set; }
        public string strSMSCDesc { get; set; }
        public string strOprDesc { get; set; }
        public SmscConnectionType strConType { get; set; }
        public int ncontype { get; set; }
        public int VenderId { get; set; }
        public string strVenderId { get; set; }
        public int Stage { get; set; }
        public int nOutSMSCId { get; set; }
        public int nUserid { get; set; }
        public int nReturn { get; set; }
        public string strSMSCIds { get; set; }
        public int nStatus { get; set; }
        public int nStatus_Bind { get; set; }
        public int nId { get; set; }
        public string strnId { get; set; }
        public int nsmscid { get; set; }
        public string strConnName { get; set; }
        public string strServerName { get; set; }
        public int nPort { get; set; }
        public string strnPort { get; set; }
        public string strUserId { get; set; }
        public string strPwd { get; set; }
        public ProtocolModeType nMode { get; set; }
        public string strURLText { get; set; }
        public string strURLUniCode { get; set; }
        public string strSuccessResp { get; set; }
        public string strErrResp { get; set; }
        public int nSMSLimit { get; set; }
        public bool nAutoconnect { get; set; }
        public int nAlivePeriod { get; set; }
        public int nSvcTimeOut { get; set; }
        public string strnAlivePeriod { get; set; }
        public string strnSvcTimeOut { get; set; }
        public string strProBuild { get; set; }
        public string strProdVersion { get; set; }
        public string strExpirationDate { get; set; }
        public bool nLog { get; set; }
        public bool nDlrvPort { get; set; }
        public string strAddressRange { get; set; }
        public string strProtocolVersion { get; set; }
        public string strCharSet { get; set; }
        public string strdcs { get; set; }
        public string strThroughPut { get; set; }
        public string strSysType { get; set; }
        public string nDestton { get; set; }
        public string nDestNPI { get; set; }
        public string nSourcetone { get; set; }
        public string nSourceNPI { get; set; }
        public string strSourceAdd { get; set; }
        public int nUnit { get; set; }
        //public int nUserid { get; set; }
        public int nUniqueid { get; set; }
        // public int nReturn { get; set; }
        public string strProtocol { get; set; }
        public string strcntname { get; set; }
        public int countrycode { get; set; }
        public int[] operatorid { get; set; }
        public int Addedby { get; set; }
        public int currentstatus { get; set; }
        public string Systemid { get; set; }
        //public string Httpserver { get; set; }
      //  public int BoundOtherSource { get; set; }
        public string nBoundOtherSource { get; set; }
        public int Sessions { get; set; }
        public int Transciever { get; set; }
        public int Receiver { get; set; }
        public int Transmitter { get; set; }
        public string strSessions { get; set; }
        public string strTransciever { get; set; }
        public string strReceiver { get; set; }
        public string strTransmitter { get; set; }
        public string Instance { get; set; }
        public string UserIp { get; set; }
    }

    public class GetConnectionsRequest
    {
        public int nSmpphttpID { get; set; }
        //public int nHttpID { get; set; }
        public int nId { get; set; }
        public int ncontype { get; set; }
        public int nStatus { get; set; }
        public int nSid { get; set; }
        public int nsmscid { get; set; }
        public int nCreatedby { get; set; }
        public int nReturn { get; set; }
        public string RequestPage { get; set; }
        public string UserIp { get; set; }
    }

    public class GetIntlSMSCIdRequest
    {
        public int Nuniqueid { get; set; }
    }

}
