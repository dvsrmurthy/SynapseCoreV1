using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.SMSCSettings
{
   public class MOMapSenderRequest
    {
       public string ID { get; set; }
       public string NSHORTCODE { get; set; }
       public string NSTATUS { get; set; }
       public string NCUSTID { get; set; }
       public string NUSERID { get; set; }
       public string NSID { get; set; }
       public string DispShortCode { get; set; }
       public string ShortCodeType { get; set; }
       public int Status { get; set; }
       public int NCREATEDBY { get; set; }
       public string command { get; set; }
       public int Updatedby { get; set; }
       public string UserIp { get; set; }
    }
}
