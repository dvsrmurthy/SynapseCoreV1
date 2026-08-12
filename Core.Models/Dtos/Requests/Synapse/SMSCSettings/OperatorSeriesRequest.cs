using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

//THIS IS TESTING CLASS  WE CAN REMOVE THIS CLASS

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{

    public class OperatorSeriesRequest
    {

        public OperatorSeries ops { get; set; }
        public List<OperatorSeries> OPsall { get; set; }

    }


    public class OperatorSeries
    {
        public int opid { get; set; }
        public string series { get; set; }
        public int mlength { get; set; }

    }

    
}
