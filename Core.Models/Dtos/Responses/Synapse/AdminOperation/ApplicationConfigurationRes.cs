using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.AdminOperation
{
    public class ApplicationConfigurationRes
    {
        public string KeyName { get; set; }
        public int value { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
    }
}
