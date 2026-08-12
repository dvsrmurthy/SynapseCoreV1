using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SecurityManagement
{
    public class GetPasswordPreferenceRequest
    {
        //public string propertyname { get; set; }
        public string custid { get; set; }
        public string UserIp { get; set; }
    }

    public class PasswordPreferenceRequest
    {
        public int Id { get; set; }
        public int customerid { get; set; }
        public int nPwdLen { get; set; }
        public int nAlpas { get; set; }
        public int nDigits { get; set; }
        public int nExpiry { get; set; }
        public int nAttempts { get; set; }
        public int nHistory { get; set; }
        public string strStopChars { get; set; }
        public int pwdminlength { get; set; }
        public int userid { get; set; }
        public int nStatus { get; set; }
        public int FstLgn { get; set; }
        public string command { get; set; }
        public int CurrentStatus { get; set; }
        public int Returnvalue { get; set; }
        public string UserIp { get; set; }
        public string Noofattempts { get; set; }
        public string Otpexpiry { get; set; }
        public string Freezetime { get; set; }
       // public int updatedby { get; set; }
    }

    public class PSRCheckerRequest
    {
        public int Id { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectNote { get; set; }
        public int UpdatedBy { get; set; }
        public int ReturnValue { get; set; }
    
    }


}
