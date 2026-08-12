using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class UserRouteOnResponse
    {
        public int ROUTEID { get; set; }
        public string RouteName { get; set; }
        public string CountryName { get; set; }
        public int Countrycode { get; set; }
        public int SeriesId { get; set; }
        public string OperatoreName { get; set; }
        public int VendorId { get; set; }
        public int SmscId { get; set; }
        public int StageNumber { get; set; }
        public string Series { get; set; }
        public string VendorName { get; set; }
        public string Smscname { get; set; }
        public bool DefaultStatus { get; set; }
        public bool Status { get; set; }
        public int CurrentStatus { get; set; }
    }
    public class CheckDefaultRouteOnResponse
    {
        public int RM_INT_ID { get; set; }
        public string RM_INT_ROUTENAME { get; set; }
        public int RM_INT_COUNTRYID { get; set; }
        public int RM_VAR_COUNTRYCODE { get; set; }
        public string RM_VAR_COUNTRY { get; set; }
        public int RM_INT_SERIESID { get; set; }
        public int RM_INT_SMSCID { get; set; }
    }   
    public class BindSeriesUserRouteOnResponse
    {
        public int OperatorId { get; set; }
        public string Series { get; set; }
        public string Name { get; set; }
        public string CreatedOn { get; set; }
        public string CountryName { get; set; }
        public int CountryCode { get; set; }
    }
    public class GetVendorsUserRouteOnResponse
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GetSMSCUserRouteOnResponse
    {
        public int Id { get; set; }
        public string SMSCName { get; set; }
        public string SMSCDesc { get; set; }
        public int ConnectionType { get; set; }
        public bool SMSCStatus { get; set; }
    }
    public class GetCountriesUserRouteOnResponse
    {
        public int CountryCode { get; set; }
        //public string CCD_VAR_CODE { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public int Value { get; set; }
    }
    public class GetOperatorsUserRouteOnResponse
    {
        public int Id { get; set; }
        public int Countrycode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }
    public class GetCountryOperatorsUserRouteOnResponse
    {
        public int ccd_int_id { get; set; }
        public string CCD_VAR_CODE { get; set; }
        public string CCD_VAR_COUNTRY { get; set; }
        public int opr_int_id { get; set; }
        public int CCD_SINT_STATUS { get; set; }
        public int CCD_DTM_ADDEDON { get; set; }
        public string CCD_SINT_ADDEDBY { get; set; }
        public int CCD_DTM_UPDATEDON { get; set; }
        public string CCD_SINT_UPDATEDBY { get; set; }
        public int Value { get; set; }
    }
    public class CheckDefaultUserRouteOnResponse
    {
        public int Id { get; set; }
        public string RouteName { get; set; }
        public int Countrycode { get; set; }
        public int SeriesId { get; set; }
        public int SMSCId { get; set; }
    }
}









