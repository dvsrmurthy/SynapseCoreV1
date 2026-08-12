using System.Collections.Generic;
using System.Security.Policy;

namespace Core.Models.Dtos.CommonDtos
{
    public class UserMenuItems
    {
        public int FeatureId { get; set; }

        public string? MenuName { get; set; }

        public string? ArabicName { get; set; }

        public string? MenuIcon { get; set; }

        public int SubFeatureId { get; set; }

        public string? SubFeature { get; set; }

        public string? SubFeatureArabicName { get; set; }

        public string? ActionName { get; set; }

        public string? ControllerName { get; set; }

        public string? AreaName { get; set; }

        public bool IsCheckerRequired { get; set; }

        public string? PageType { get; set; }
        public int UserRole { get; set; }
        public int RateCardRoleId { get; set; } 
        public int ParentCustomerId { get; set; }
    }
}
