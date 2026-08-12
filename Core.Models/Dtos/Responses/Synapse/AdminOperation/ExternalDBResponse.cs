using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class ExternalDBResponse
    {
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? IncomeGroup { get; set; }
        public string? CityofResidence { get; set; }
    }
}
