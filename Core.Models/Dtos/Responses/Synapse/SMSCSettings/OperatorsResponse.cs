using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class OperatorsResponse
    {
        public int OPRID { get; set; }
        public string? OPRCNTRY { get; set; }
        public string? OPRNAME { get; set; }
        public string? OPRDESC { get; set; }
        public bool OPRSTS { get; set; }
        public string? OPRADON { get; set; }
        public int CNTID { get; set; }
        public string? CNTNAME { get; set; }
        public int CurrentStatus { get; set; }

        public string? RejectedReason { get; set; }


       

    }




    //opeartor series response
    public class OperatorSeriesResponse
    {
        public int oprid { get; set; }
        public string? mseries { get; set; }
        public int mobilelen { get; set; }
    }

    public class DtoBulkOperatorsResponse
    {
        public int CountryCode { get; set; }

        public string? OperatorName { get; set; }

        public DtoBulkSeriesResponse Series { get; set; }
    }

    public class DtoBulkSeriesResponse
    {
        public string? Series { get; set; }

        public int MobileLength { get; set; }
    }

    public class DtoBulkOperatorsMainResponse
    {
        public List<DtoBulkOperatorsResponse> SuccessResponse { get; set; }

        public List<DtoBulkOperatorsResponse> FailedResponse { get; set; }
    }
    public class OperatorRateCardResponse
    {
        public int oprid { get; set; }
        public int OPRCNTRY { get; set; }
        public int CNTID { get; set; }
        public string? CNTNAME { get; set; } 
        public string? OPRNAME { get; set; }
        public int OPRSTS { get; set; }
        public string? OPRADON { get; set; }
        public int CurrentStatus { get; set; }
        public string? RejectedReason { get; set; }
    }
}
