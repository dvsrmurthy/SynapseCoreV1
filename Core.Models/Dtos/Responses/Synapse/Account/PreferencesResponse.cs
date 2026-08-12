using System.Collections.Generic;
using Core.Models.Enums;

namespace Core.Models.Dtos.Responses.Synapse.Account
{
    public class Preferences
    {
        public string? PwdAlphabetsCount { get; set; }

        public string? PwdchngeOnfstlgn { get; set; }

        public string? PwdDigitsCount { get; set; }

        public string? PwdExpiry { get; set; }

        public string? PwdLength { get; set; }

        public string? PwdStopCharacters { get; set; }

        public int CustomerId { get; set; }

        public bool ChangeOnFirstLogin { get; set; }
    }

    public class PreferencesResponse
    {
        public List<Preferences> Preferences { get; set; }

        public ActionStatus ActionStatus { get; set; }
    }
}
