using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class ExternalDBRequest
    {
        //public int nId { get; set; }
        //public string? UserIp { get; set; }
        public int UserId { get; set; }
        public string? UserIp { get; set; }
        public string? SearchText { get; set; }
        public string? FilePath { get; set; }
        public int Createdby { get; set; }
        public int FILETYPE { get; set; }
        public List<ImportED> Importedlist { get; set; }
    }

    public class ImportED
    {
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public string? Nationality { get; set; }
        public string? IncomeGroup { get; set; }
        public string? CityofResidence { get; set; }
        public string? VMDB { get; set; }
        //public string? FilePath { get; set; }
        //public List<ImportED> ExternalDBList { get; set; }

    }
}
