using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class WhitelistRequest
    {
        public string? strMobileNo { get; set; }
        public bool nStatus { get; set; }
        public int bSearch { get; set; }
        public string? requestedby { get; set; }
        public string? UserIp { get; set; }
        public string? SearchText { get; set; }
    }

    public class InsertWhitelist
    {
        public int nId { get; set; }
        public string? strMobileNo { get; set; }
        public string? command { get; set; }
        public int CreatedBy { get; set; }
        public string? UserIp { get; set; }
    }

    public class ExportWhitelistRequest
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
    }

    public class ImportWlistnoRequest
    {
        public int Id { get; set; }
        public string? FILEPATH { get; set; }
        public int Createdby { get; set; }
        public int FILETYPE { get; set; }
        public int CurrentStatus { get; set; }
        public List<ImportWhitelistReq> ImpWlist { get; set; }
    }

    public class ImportWhitelistReq
    {
        public string? MobileNo { get; set; }
    }
}
