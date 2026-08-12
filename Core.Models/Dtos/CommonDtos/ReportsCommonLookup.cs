using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos.CommonDtos
{
    public class ReportsCommonLookups
    {
        public List<AccountManagers> AccountManagers { get; set; }

        public List<ReportCustomers> ReportCustomers { get; set; }

        public List<ReportUsers> ReportUsers { get; set; }

        public List<AllCountries> AllCountries { get; set; }

        public List<AllOperators> AllOperators { get; set; }
    }

    public class AccountManagers
    {
        public int AccountManagerId { get; set; }

        public string? AccountManagerName { get; set; }
    }

    public class ReportCustomers
    {
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }
    }

    public class ReportUsers
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }
    }

    public class AllCountries
    {
        public int CountryId { get; set; }

        public string? CountryName { get; set; }
    }

    public class AllOperators
    {
        public int OperatorId { get; set; }

        public string? OperatorName { get; set; }
    }
}
