using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.HMS.HealthAlerts.AdminOnResponse
{
    public class CustomerOnResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class TransMsgTypesOnResponse
    {
        public int AUTOID { get; set; }
        public string? TRANSACTIONTYPE { get; set; }
        public string? NAME { get; set; }
        public string? MSGTYPE { get; set; }
        public int STATUS { get; set; }
    }

    public class SelectedTransMsgTypesOnResponse
    {
        public string? TRANSACTIONTYPE { get; set; }
        public string? MSGTYPE { get; set; }
    }

    public class GetCustomerTriggersOnResponse
    {
        public int AUTOID { get; set; }
        public int TRIGGERID { get; set; }
        public string? MSGTYPE { get; set; }
        public int CUSTID { get; set; }
        public string? CUSTNAME { get; set; }
        public string? STATUS { get; set; }
    }

    public class CustomerPerferenceCountOnRespone
    {
        public int AUTOID { get; set; }
        public int CUSTID { get; set; }
        public string? PACKAGE { get; set; }
        public int ALERTS { get; set; }
        public int TRIGGERS { get; set; }
        public int STATUS { get; set; }
    }

    public class CustomerExistingTriggersCountOnResponse
    {
        public int EXISTINGTRGCOUNT { get; set; }
    }


}
