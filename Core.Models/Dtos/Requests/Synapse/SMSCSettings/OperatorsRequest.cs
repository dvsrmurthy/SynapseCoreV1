using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
    public class OperatorsRequest
    {
        //opertator get
        public int NID { get; set; }   //operator id
        public string? STRCOUNTRY { get; set; }
        public int NSTATUS { get; set; }
        public int nCreatedby { get; set; }
        public string? RequestPage { get; set; }
        public string? UserIp { get; set; }
    }

    //operator SET

    public class AddEditOperator
    {
        public int OPRID { get; set; }
        public int CNTID { get; set; }
        public string? OPRNAME { get; set; }
        public string? OPRDESC { get; set; }
        public bool OPRSTS { get; set; }
        public int CurrentStatus { get; set; }
        public string? CNTNAME { get; set; }

        public int USERID { get; set; }

        public string? command { get; set; }
        public string? UserIp { get; set; }
        public bool Ischecked { get; set; }

       public List<OperatorSeries> Series { get; set; }

       
    }

    public class DtoBulkOperatorsMainRequest
    {
        public List<DtoBulkOperatorsRequest> BulkOperators { get; set; }

        public int UserId { get; set; }

        public int CurrentStatus { get; set; }
    }

    public class DtoBulkOperatorsRequest
    {
        public int CountryCode { get; set; }

        public string? OperatorName { get; set; }

        public List<DtoBulkSeriesRequest> Series { get; set; }
    }

    public class DtoBulkSeriesRequest
    {
        public string? Series { get; set; }

        public int MobileLength { get; set; }
    }

    public class ApproveOrRejectRequest
    {
        public int Id { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectReason { get; set; }

        public int UpdatedBy { get; set; }
    }

    //public class AddOperatorSeries
    //{
    //    //operator series section properties
    //    public int opid { get; set; }
    //    public string? Series { get; set; }
    //    public int MLength { get; set; }

    //    public string? command { get; set; }
    //}


}

