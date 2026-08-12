using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Requests.Synapse.AdminOperation
{
    public class FilterWordsReq
    {
        public int nId { get; set; }
        public int nStatus { get; set; }
        public string strSearchFor { get; set; }
        public int nReturn { get; set; }
        public string UserIp { get; set; }
    }

    public class SetFWReq
    {
        public int nId { get; set; }
        public string strWord { get; set; }
        public string strReplaceWord { get; set; }
        public int nStatus { get; set; }
        public string strDupliWords { get; set; }
        public int nReturn { get; set; }
        public int CreatedBy { get; set; }
        public int CurrentStatus { get; set; }
        public string RejectNote { get; set; }
        public string command { get; set; }
        public string UserIp { get; set; }
    }

    public class ImportFWReq
    {
        public string xmlFilter { get; set; }
        public string FilePath { get; set; }
        //public string GROUPIDS { get; set; }
        public int CREATEDBY { get; set; }
        public int FILETYPE { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int nretval { get; set; }
        // public string FWList { get; set; }
        public List<ImportFW> FWList { get; set; }

    }
    public class ImportFW
    {
        //public string xmlFilter { get; set; }
        public string Word { get; set; }
        public string ReplaceWord { get; set; }
        public string FilePath { get; set; }
        public List<ImportFW> FilterwordList { get; set; }

    }

    public class CheckerFilterWordsRequest
    {
        public string ID { get; set; }
        public int CURRENTSTATUS { get; set; }
        public int RETURNVALUE { get; set; }
        public int UPDATEDBY { get; set; }
        public string REJECTNOTE { get; set; }
        public string Word { get; set; }
        public string inputparam { get; set; }

    }



}
