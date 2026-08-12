using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
   public class UserRouteOnRequest
    {
        public int ROUTEID{get;set;}
        public int CREATEDBY{get;set;}
        public int ISDEFAULT{get;set;}
        public int COUNTRYID{get;set;}
        public string REQUESTEDBY { get; set; }
        public string UserIp { get; set; }
    }
   public class CheckDefaultRouteOnRequest
   {
       public int ROUTEID{get;set;}
       public string ROUTEIDS { get; set; }
       public int COUNTRYCODE{get;set;}
       public int STATUS{get;set;}
       public int SERIESID{get;set;}
       public int DEFAULTSTATUS{get;set;}
       public int USERID{get;set;}
       public int VENDORID{get;set;}
       public int SMSCID{get;set;}
       public int CurrentStatus { get; set; }
       public string chgstatus { get; set; }
       public string UserIp { get; set; }
   }
   public class ChangeStatusUserRouteOnRequest
   {
       public string ROUTEIDS{get;set;}
       public int STATUS{get;set;}
   }
   public class ShowDetailsUserRouteOnRequest
   {
       public int COUNTRYID{get;set;} 
       public int CREATEDBY{get;set;}
       public int DEFAULTSTATUS{get;set;}
   }
   public class BindSeriesUserRouteOnRequest
    {
       public string COUNTRYIDS{get;set;}
       public string OPERATORIDS{get;set;}
       public int CHECKVALUE{get;set;}
    }
   public class GetVendorsUserRouteOnRequest
   {
       public int VENDORID{get;set;}
       public int STATUS{get;set;}
       public int CREATEDBY{get;set;}
       public string REQUESTEDBY { get; set; }
   }
   public class GetSMSCUserRouteOnRequest
   {
       public int VENDORID { get; set; }
   }
   public class GetRoutesExistUserRouteOnRequest
   {
       public string STRCOUNTRYCODE { get; set; }
       public int STRSERIES { get; set; }
       public int VENDORID { get; set; }
       public int SMSCID { get; set; }
       public int DEFAULT { get; set; }
       public int ROUTEMASTERID { get; set; }
   }
   public class InsertRouteUserRouteOnRequest_1
   {
       public string ROUTENAME { get; set; }
       public string OLDROUTENAME { get; set; }
       public int COUNTRYCODE { get; set; }
      // public string COUNTRYNAME { get; set; }
       public int SERIESID { get; set; }
       public int VENDORID { get; set; }
       public int SMSCID { get; set; }
       public int DEFAULT { get; set; }
       public int ADDEDBY { get; set; }
       public int STATUS { get; set; }
       public int ROUTEID { get; set; }
   }
   public class InsertRouteUserRouteOnRequest
   {
       public string ROUTENAME { get; set; }
       public string OLDROUTENAME { get; set; }
       public string COUNTRYCODE { get; set; }
       // public string COUNTRYNAME { get; set; }
       public string SERIESID { get; set; }
       public int VENDORID { get; set; }
       public string SMSCID { get; set; }
       public int DEFAULT { get; set; }
       public int ADDEDBY { get; set; }
       public int STATUS { get; set; }
       public int ROUTEID { get; set; }
       public int CurrentStatus { get; set; }
       public string UserIp { get; set; }
   }
   public class CheckerUpdateUserRouteOnRequest
   { 
       public string ROUTEID{get;set;}
       public int CURRENTSTATUS{get;set;}
       public string REJECTREASON{get;set;}
       public int UPDATEDBY {get;set;}
   }
}






