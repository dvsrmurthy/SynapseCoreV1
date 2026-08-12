namespace Core.Models.Dtos.CommonDtos
{
    public class LogOnRequest
    {
        /// <summary>
        /// User Name Property
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password Property
        /// </summary>
        public string Password { get; set; }
        public string useremail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserSessionId { get; set; }

        public bool IsWebRequest { get; set; }

        public int UserId { get; set; }

        public string otp { get; set; }
    }
    public class IpWhiteListRequest
    {
        public string Username { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string UserSessionId { get; set; }
        public string mobileNo { get; set; }
        public string otpvalue { get; set; }
    }
    public class IpWhiteListResponse
    {
        public int returnValue { get; set; }
        public string ActionResult { get; set; }
        public string returnMessage { get; set; }
        public int NoOfAttempts { get; set; }
        public int OTPTime { get; set; }
    }
}
