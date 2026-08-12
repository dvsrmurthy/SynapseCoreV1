using System.Collections.Generic;

namespace Core.Models.Dtos.Requests.HL7
{
    public class Hl7Request
    {
        public string? SegmentName { get; set; }        

        public List<string> SegmentNames { get; set; }

        public string? FileName { get; set; }
    }
}
