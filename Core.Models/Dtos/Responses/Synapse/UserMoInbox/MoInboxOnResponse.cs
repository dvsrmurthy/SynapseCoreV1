using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.UserMoInbox
{
    public class MoInboxOnResponse
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string ReplyMessage { get; set; }
        public string ReceivedDate { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string DAY { get; set; }
        public string PBName { get; set; }
    }

    #region Load Methods
    public class MoInboxSenderIDsOnResponse
    {
        public int ID { get; set; }
        public string Code { get; set; }
    }
    #endregion

    #region for MoForwardbox
    public class MoForwardOnResponse
    {
        public int Id { get; set; }
        public string ShortCode { get; set; }
        public int ForwordType { get; set; }
        //public string ForwaredEmail { get; set; }
        public string ForwaredURL { get; set; }
        public string ReceivedDate { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }

        public string ForwaredDate { get; set; }
        public int ForwaredStatus { get; set; }

        public string HttpForwardedUrl { get; set; }
        public string HttpResponse { get; set; }
        public string SmppForwardUser { get; set; }
        public string SmppForwardResult { get; set; }

    }
    #endregion for MoForwardbox

    #region for MoSentBox

    public class MoSentBoxOnResponse
    {
        public string ShortCode { get; set; }
        public string MobileNo { get; set; }
        public string ReplyMessage { get; set; }
        public string Message { get; set; }
        public string ReceivedDate { get; set; }
        public string Name { get; set; }
        public string MoReply { get; set; }
        public int Status { get; set; }
    }
    #endregion for MoSentbox

    #region for MoSelectByDropDownSearch
    public class MoSearchOnResponse
    {
        public int DAY { get; set; }
        public string ShortCode { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
    }
    #endregion for MoSelectByDropDownSearch
}
