using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Core.DBAccess;
using Core.Models.Dtos.Requests.Synapse.StatusMonitor;
using Core.Models.Dtos.Responses.Synapse.StatusMonitor;
using Core.Models.Enums;
using Core.Models.Extensions;
using Core.Utilities.Helpers;
using Synapse.Web.Helpers.SecureAccess;


namespace Synapse.Web.Models
{
    public class StatusDLRPercentageMain
    {
        public StatusDLRPercentage StatusDLRPercentage { get; set; }
        public List<OperatorTableOne> OperatorTableOnes { get; set; }
        public List<OperatorTableTwo> OperatorTableTwos { get; set; }
        public List<OperatorTableThree> OperatorTableThrees { get; set; }

        public List<CountryTableOne> CountryTableOnes { get; set; }
        public List<CountryTableTwo> CountryTableTwos { get; set; }
        public List<CountryTableThree> CountryTableThrees { get; set; }
    }

    public class StatusDLRPercentage
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string sender { get; set; }
       
        public StatusDLRPercentageMain buildmodel(string fromDate, string toDate, string sender, int DLRCountryOperator)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                StatusDLRPercentageMain statusDLRPercentageMain = new StatusDLRPercentageMain();
                statusDLRPercentageMain.StatusDLRPercentage = new StatusDLRPercentage()
                {
                    fromDate = fromDate,
                    toDate = toDate,
                    sender = sender
                };
                //Operator wise
                var response = clientAcces.GetDlrPercentageAsync(new DLRPercentageSearch
                {
                    fromDate = fromDate, toDate = toDate, sender = sender
                });
                try
                {
                    statusDLRPercentageMain.OperatorTableOnes = new List<OperatorTableOne>();
                    if (response.Result == null) return statusDLRPercentageMain;
                    statusDLRPercentageMain.OperatorTableOnes =  response.Result.OperatorTableOnes.Select(x => new OperatorTableOne
                    {
                        IncomingUserId = x.IncomingUserId,
                        UserName = x.UserName,
                        InboundSender = x.InboundSender,
                        countrycode = x.countrycode,
                        CountryName = x.CountryName,
                        SMSCount = x.SMSCount
                    }).ToList();
                    statusDLRPercentageMain.OperatorTableTwos = new List<OperatorTableTwo>();
                    statusDLRPercentageMain.OperatorTableTwos = response.Result.OperatorTableTwos.Select(x => new OperatorTableTwo
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        Sender = x.Sender,
                        countrycode = x.countrycode,
                        CountryName = x.CountryName,
                        Series = x.Series,
                        Delivrd = x.Delivrd,
                        Undeliv = x.Undeliv,
                        DeliveryPercent = x.DeliveryPercent
                    }).ToList();

                    statusDLRPercentageMain.OperatorTableThrees = new List<OperatorTableThree>();
                    statusDLRPercentageMain.OperatorTableThrees = response.Result.OperatorTableThrees.Select(x => new OperatorTableThree
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        Sender = x.Sender,
                        countrycode = x.countrycode,
                        CountryName = x.CountryName,
                        Series = x.Series,
                        DlvrdStatus = x.DlvrdStatus,
                        DLRCount = x.DLRCount,
                    }).ToList();
                    return statusDLRPercentageMain;                    
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }
        public StatusDLRPercentageMain buildmodelC(string fromDate, string toDate, string sender, int DLRCountryOperator)
        {
            using (var clientAcces = new AuthenticateSecurityClient())
            {
                StatusDLRPercentageMain statusDLRPercentageMain = new StatusDLRPercentageMain();
                statusDLRPercentageMain.StatusDLRPercentage = new StatusDLRPercentage()
                {
                    fromDate = fromDate,
                    toDate = toDate,
                    sender = sender
                };
                //Country wise
                var response = clientAcces.GetDlrPercentageCAsync(new DLRPercentageSearch
                {
                    fromDate = fromDate, toDate = toDate, sender = sender
                });
                try
                {
                    statusDLRPercentageMain.CountryTableOnes = new List<CountryTableOne>();
                    if (response.Result == null) return statusDLRPercentageMain;
                    statusDLRPercentageMain.CountryTableOnes = response.Result.CountryTableOnes.Select(x => new CountryTableOne
                    {
                        IncomingUserId = x.IncomingUserId,
                        UserName = x.UserName,
                        InboundSender = x.InboundSender,
                        countrycode = x.countrycode,
                        CountryName = x.CountryName,
                        SMSCount = x.SMSCount
                    }).ToList();
                    statusDLRPercentageMain.CountryTableTwos = new List<CountryTableTwo>();
                    statusDLRPercentageMain.CountryTableTwos = response.Result.CountryTableTwos.Select(x => new CountryTableTwo
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        Sender = x.Sender,
                        countrycode = x.countrycode,
                        CountryName = x.CountryName,                        
                        Delivrd = x.Delivrd,
                        Undeliv = x.Undeliv,
                        DeliveryPercent = x.DeliveryPercent
                    }).ToList();

                    statusDLRPercentageMain.CountryTableThrees = new List<CountryTableThree>();
                    statusDLRPercentageMain.CountryTableThrees = response.Result.CountryTableThrees.Select(x => new CountryTableThree
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        Sender = x.Sender,
                        countrycode = x.countrycode,
                        CountryName = x.CountryName,                        
                        DlvrdStatus = x.DlvrdStatus,
                        DLRCount = x.DLRCount,
                    }).ToList();
                    return statusDLRPercentageMain;
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }

    }
}