using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserMoCampaignConfiguration
{
    #region MoCampaign
    public class ShowMoCampaignOnRequest
    {
        public int NID { get; set; }
        public string STRSEARCH { get; set; }
        public int CUSTID { get; set; }
        public int USERID { get; set; }
        public int STATUS { get; set; }        
    }
   public class SaveMoCamapignOnRequest
   {
       public int NID { get; set; }
       public string CAMPNAME { get; set; }
       public string DESCRIPTION { get; set; }
       public int SHORTCODE { get; set; }
       public string KEYWORD { get; set; }
       public string SMSCID { get; set; }
       public string STRXML { get; set; }
       public int USERID { get; set; }
       public int CUSTID { get; set; }      
   }
   public class GetMoCampaignNamesOnRequest
   {
       public int NID { get; set; }       
       public int CUSTID { get; set; }
       public int USERID { get; set; }
       public int STATUS { get; set; } 
   }
   public class ChangeStatusMoCampaignsOnRequest
   {
       public int NID { get; set; }
       public int STATUS { get; set; }
       public int UPDATEDDBY { get; set; }
   }
   public class MoCompaginsForXMLOnRequest
   {

       public int nMOCINTCAMPID { get; set; }
       public string strMOCVARCAMPNAME { get; set; }
       public string strMOCVARDESC { get; set; }
       public int nMOCVARSHORTCODE { get; set; }
       public int nMOCVARKEYWORD { get; set; }
       public int nMOCVARSCHEDULE { get; set; }
       public int nMOCINTSTATUS { get; set; }
       public int nMOCINTUSERID { get; set; }
       public string strMOCDETCREATEDDATE { get; set; }
       public int nMOCINTSHORTCODE { get; set; }
       public int nRetVal { get; set; }
       public string strStartime { get; set; }
       public string strEndTime { get; set; }
       public int nInterval { get; set; }
       public string strDate { get; set; }
       public string strEndOn { get; set; }
       public string strAlertOn { get; set; }
       public int nWeek { get; set; }
       public string strWeekdays { get; set; }
       public string strDays { get; set; }
       public string strMonths { get; set; }
       public int nEndOn { get; set; }
       public string strEndDate { get; set; }
       public string strStartDate { get; set; }
       public string strSchedulexml { get; set; }
       public int nCUSTOMERID { get; set; }
       public int nCreatedBy { get; set; }
       public string nMOCINTSYSTEMID { get; set; }
       public string strIds { get; set; }
       public string strSearch { get; set; }
   }
    #endregion
    #region MoReply
    public class ShowMoReplyOnRequest
    {
       public int NID { get; set; }      
       public int USERID { get; set; }
        public int CUSTID { get; set; }       
    }
    public class SaveMoReplyOnRequest
    {
        public int NID { get; set; }
        public int CAMPID { get; set; }
        public int NREPLYTYPE { get; set; }
        public string STRREPLYTYPE { get; set; }
        public string LANGUAGE { get; set; }
        public string REPLYTEXT { get; set; }
        public int CREATEDBY { get; set;}
        public int UPDATEDBY { get; set; }
    }
    #endregion
    #region MoForward
    public class ShowMoForwardOnRequest
    {
        public int NID { get; set; }
        public string STRSEARCH { get; set; }       
        public int USERID { get; set; }
        public int CUSTID { get; set; }
        public int RETVAL { get; set; }
    }
    public class SaveMoForwardOnRequest
    {
        public int NID { get; set; }
        public int CAMPID { get; set; }        
        public int FORWARDTYPE { get; set; }
        public string STRURL { get; set; }
        public int USERID { get; set; }
        public int MBCID { get; set; }
        public int MODULEID { get; set; }
        public string STRRETRYTYPE { get; set; }
        public string STRRETRYATTEMPTS { get; set; }
        public string STRRETRYINTERVAL { get; set; }
        public int NCREATEDBY { get; set; }        
    }
    public class DeleteMoForwardOnRequest
    {
        public int CAMPID { get; set; }
    }
    #endregion
    #region MoSmppForward
    public class ShowMoSmppForwardOnRequest
    {
        public int MSFDID { get; set; }
        public string STRSEARCH { get; set; }
        public int CUSTID { get; set; }
        public int USERID { get; set; }
        public int STATUS { get; set; }
        public int RETVAL { get; set; }      
    }
    public class SaveMoSmppForwardOnRequest
    {
        public int MSFDID { get; set; }
        public int SHORTCODE { get; set; }
        public int USERID { get; set; }
        public int CUSTOMERID { get; set; }
        public int REQUIREDSMPPFWD { get; set; }
        public int STATUS { get; set; }
        public int CREATEDBY { get; set; }
        public int UPDATEDBY { get; set; }

    }
    public class ChangeStatusMoSmppOnRequest
    {
        public string STRMSFDID { get; set; }
        public int STATUS { get; set; }
        public int UPDATEDBY { get; set; }
    }
    public class GetMoCampForward
    {
        public int NID { get; set; }
        public int CUSTID { get; set; }
        public int USERID { get; set; }
    }
    #endregion 
    #region Load Methods
    public class LoadSenderIDsOnRequest
    {
        public int USERID { get; set; }
        public int CUSTID { get; set; }
        public int STATUS { get; set; }
    }

    public class LoadSMSCSOnRequest
    {
        public int USERID { get; set; }
        public int CUSTID { get; set; }
        public string SHORTCODE { get; set; }
    }

    public class LoadKeywordsOnRequest
    {
        public int USERID { get; set; }
        public int CUSTID { get; set; }
        public int STATUS { get; set; }
    }
    #endregion

}



