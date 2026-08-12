using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.HMS.HealthAlerts.AdminOnRequest
{
    public class CustomerOnRequest
    {
       // public int UserId { get; set; }
    }

    public class TransMsgTypesOnRequest
    {
        public int UserId { get; set; }
    }

    public class SelectedTransMsgTypesOnRequest
    {
        public int CustId { get; set; }
        public int AddedBy { get; set; }
    }

    public class GetCustomerTriggersOnRequest
    {
        public int userId { get; set; }
    }
    public class CustomerTriggerChangeStatusOnRequest
    {
        public int AutoId { get; set; }
        public int Status { get; set; }
        public int AddedBy { get; set; }
    }

    public class CustomerPerferenceCountOnRequest
    {
        public int CustId { get; set; }
    }
    public class CustomerExistingTriggersCountOnRequest
    {
        public int CustId { get; set; }
    }
    public class SetCustomerTriggersOnRequest
    {
        public int CustId { get; set; }
        public int TransId { get; set; }
        public int Status { get; set; }
        public int AddedBy { get; set; }
    }
    public class DeleteExistingCustomerTriggersOnRequest
    {
        public int CustId { get; set; }
    }
    public class UpdateCustomeTriggersOnRequest
    {
        public int CustId { get; set; }
        public int TransId { get; set; }
        public int Status { get; set; }
        public int AddedBy { get; set; }
    }
}
