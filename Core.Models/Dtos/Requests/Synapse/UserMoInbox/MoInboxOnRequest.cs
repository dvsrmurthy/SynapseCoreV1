using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.UserMoInbox
{
    public class MoInboxOnRequest
    {
        public string? Id { get; set; }
        public int UserId { get; set; }
        public string? MoShortCode { get; set; }       
        public string? MoSearch { get; set; }
        public string? MoKeyword { get; set; }
        public string? MoMessage { get; set; }
        public int NReturnValue { get; set; }
        public string? SearchBy { get; set; }
        public int Days { get; set; }
        public string? ReplyMessage { get; set; }
        public string? CustomerIds { get; set; }
        public string? UserIds { get; set; }
        public string? UserIp { get; set; }
        public int IsEncrypt { get; set; }
    }

    #region Load Methods
    public class MoInboxSenderIDsOnRequest
    {
        public int CUSTID { get; set; }
        public int USERID { get; set; }
        public int STATUS { get; set; }
    }
    #endregion

  #region for MoForward
    public class MoForwardOnRequest
    {
        public string? Id { get; set; }
        public int UserId { get; set; }
        public string? ShortCode { get; set; }
        public int ReturnValue { get; set; }
        public int Days { get; set; }
        public string? SearchBy { get; set; }
        public string? CustomerIds { get; set; }
        public string? UserIds { get; set; }
        public string? UserIp { get; set; }
    }
  #endregion for MoForward

    #region for MoSentBox
    public class MoSentBoxOnrequest
    {
        public int UserId { get; set; }
        public string? MoShortCode { get; set; }
        public string? MoSearch { get; set; }
        public string? MoKeyword { get; set; }
        public string? MoMessage { get; set; }
        public string? SearchBy { get; set; }
        public int NReturnValue { get; set; }
        public int Days { get; set; }
    }
    #endregion for MoSentBox
}
