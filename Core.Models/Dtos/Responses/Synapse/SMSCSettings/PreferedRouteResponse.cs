using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.Responses.Synapse.SMSCSettings
{
    public class PreferedRouteResponse
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string? Name { get; set; }//customer
        public string? UserName { get; set; }
        public string? Operator { get; set; }//operator
        public string? CountryName { get; set; }
        public int CreatedBy { get; set; }
        public bool Status { get; set; }

        //public int id { get; set; }//customer
        //public int ID { get; set; }//router
        //public int iD { get; set; }//smscmaster,user,operator
        //public int NaMe { get; set; }//operator
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int Countrycode { get; set; }
        //public int CountryCode { get; set; }//ROUTES
        //public int countrycode { get; set; }//country
        public int SeriesId { get; set; }
        public int VendorId { get; set; }
        public int SMSCId { get; set; }
        public string? SMSCName { get; set; }
        public bool DefaultStatus { get; set; }
        public string? RouteName { get; set; }

        public int CurrentStatus { get; set; }
        public int Fstatus { get; set; }

        public string? requestedby { get; set; }
        public string? Rejectnote { get; set; }

        public string? VendorName { get; set; }
    }
}
    
