namespace Core.Models
{
    public class UserContextDetails
    {
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
    }

    public class UserActions
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public bool IsCheckerRequired { get; set; }
    }
}
