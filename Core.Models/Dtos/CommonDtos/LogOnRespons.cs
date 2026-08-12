using System;
using Core.Models.Enums;

namespace Core.Models.Dtos.CommonDtos
{
    public class LogOnRespons
    {        
        /// <summary>
        /// 
        /// </summary>
        /// 
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CustomerId { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DivisionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Http { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Web { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Smpp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SessionsCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UpdatedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DivisionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MobilityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MobilityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DIVMBC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AcmId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Datasource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MBCONSTRING { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ProductLogoReplacedWithCLogo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FooterNotes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExpiryDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CustomerType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CustomerStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool LDap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HashKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ActionStatus ActionResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PreProcessorInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsFirstLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReturnValue { get; set; }

        public int UserHistoryIdentity { get; set; }

        public int? uIsFirstLogin { get; set; }
        public bool IsDefaultCustomer { get; set; }
        public int AvailableCredits { get; set; }
        public int CreditType { get; set; }
        public string GetIPAddress { get; set; }

        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public string AccountType { get; set; }

        public int FreezeTimeMinutes { get; set; }

        public bool IsTwoFactor { get; set; }

        public int NoOfAttempts { get; set; }
        public int RemainAttempts{get;set;}
        public int OTPTime { get; set; }
        public int IPWhiteListCount { get; set; }
        public int OtpCount { get; set; }
    }
}
