using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
   
        //public class GetCustomersDetailsResponse
        //{
        //    public int Id { get; set; }
        //    public string? Name { get; set; }
        //    public int CustomerType { get; set; }
        //    public string? Email { get; set; }
        //    public string? Mobile { get; set; }
        //    // public DateTime ExpiryDate { get; set; }
        //}
        //public class GetUsersDetailsResponse
        //{
        //    public int Userid { get; set; }
        //    public string? FirstName { get; set; }
        //    public string? MiddleName { get; set; }
        //    public string? LastName { get; set; }
        //    public string? UserName { get; set; }
        //    public string? Password { get; set; }
        //    public string? Mail { get; set; }
        //    public string? MobileNo { get; set; }
        //    public int CustomerId { get; set; }
        //    public string? CustomerName { get; set; }
        //    public int divisionid { get; set; }
        //    public string? DivisionName { get; set; }
        //    public int RoleId { get; set; }
        //    public string? RoleName { get; set; }
        //    public int Level { get; set; }
        //    public bool Http { get; set; }
        //    public bool Smtp { get; set; }
        //    public bool Web { get; set; }
        //    public bool Smpp { get; set; }
        //    public int SessionsCount { get; set; }
        //    public bool Status { get; set; }
        //    public string? NAME { get; set; }
        //    public string? DecryptPassword { get; set; }
        //    public string? Customer { get; set; }
        //    public int Fstatus { get; set; }
        //    public bool ldap { get; set; }
        //    public string? RejectNote { get; set; }

        //}

        public class GetGBLSenderResponse
        {
            public int Id { get; set; }
            public string? Code { get; set; }
        }
    public class GETINTLUSERSTATDETAILSResponse
    {
        public int CUSTOMERID { get; set; }
        public int USERID { get; set; }
        public string? COUNTRY { get; set; }
        public string? COUNTRYNAME { get; set; }
        public int LEN_COUNTRY { get; set; }
        public int MOBILELEN { get; set; }
        public int TOTMOBILELEN { get; set; }
        public int OPERATORID { get; set; }
        public string? OperatorName { get; set; }
        public string? SERIES { get; set; }
        public int SMSCID { get; set; }
        public string? SMSC { get; set; }
        public int PROTOCOLID { get; set; }
        public int STAGE { get; set; }
        public bool DEFROUTE { get; set; }
        public string? SENDER { get; set; }
        public int SENDERID { get; set; }
        public int ROUTEID { get; set; }
        public string? DISPLAYSID { get; set; }
        public int VENDORID { get; set; }
        public string? VendorName { get; set; }
    }
    }
