using System.Collections.Generic;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class RouteResponseNew
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? UserId { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public int SenderId { get; set; }
        public string? DisplaySender { get; set; }
        public string? DispatchedSID { get; set; }
        public string? RouteInfo { get; set; }
        public string? RouteId { get; set; }
        public bool DNDByPass { get; set; }
        public bool DNSByPass { get; set; }
        public int routeAlreadyExist { get; set; } = 0;
    }
    public class SenderIdMapperResponse
    {
        
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string? Name { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public int SenderId { get; set; }

        public string? Code { get; set; }

        public string? DisplaySender { get; set; }

        public int Countrycode { get; set; }

        public string? CountryName { get; set; }

        public int CreatedBy { get; set; }

        public string? AddedByUser { get; set; }

        public bool Status { get; set; }

        public string? DespatchType { get; set; }

        public int CurrentStatus { get; set; }

        public string? RejectNote { get; set; }
        public string? PreferredRoute { get; set; }
        public string? Shortcode { get; set; }

        public string? CreatedByname { get; set; }

        public string? SMSCName { get; set; }
        public bool DNSBYPASS { get; set; }
        public bool DNDBYPASS { get; set; }
        public string? Network { get;set; }
        public bool CheckHLR { get; set; } = false;
        public string? Category { get; set; }
    }

    public class MapSenderShortCodesNRoutes
    {
        public List<ShortCode> ShortCodes { get; set; }

        public List<Route> Routes { get; set; }
    }

    public class ShortCode
    {
        public int Id { get; set; }

        public string? Code { get; set; }
    }

    public class Route
    {
        public int Id { get; set; }

        public string? RouteName { get; set; }
        public int COUNTRYCODE { get; set; }    //added by Murty
    }
}
