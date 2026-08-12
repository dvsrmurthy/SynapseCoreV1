using System;

namespace Core.Models.Dtos.CommonDtos
{
    public class ApplicationGlobalVariables
    {
        public bool FilterByDepartment { get; set; }

        public AdDetails AdDetails { get; set; }

        public int PageGridSize { get; set; }

        public int OutBoxQueSize { get; set; }

        public int NoOfAttemptSmscRetry { get; set; }

        public int SmscRetryFrequencyInterval { get; set; }

        public int CustomerCreditMargin { get; set; }

        public int CustomerExpiryDateMargin { get; set; }

        public string CustomerToMailAddress { get; set; }

        public string CustomerCcMailAddress { get; set; }

        public bool SusperndMessage { get; set; }
    }

    public class AdDetails
    {
        public string ServerName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
