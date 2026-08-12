using Core.Data.Data.Services;
using Core.Data.IDataInterfaces.Account;
using Core.Data.Utilities;
using Core.Models.Dtos.CommonDtos;
using Core.Models.Dtos.Responses.Synapse.Account;
using Core.Models.Dtos.Responses.Synapse.SMSCSettings;
using Core.Models.Dtos.Responses.Synapse.UserManagement;
using Core.Models.Enums;
using Core.Models.Extensions;
using Core.Models.Helpers;
using Core.Utilities.Helpers;
using Elmah;
using Microsoft.Extensions.Configuration;

//using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data.Data.Account
{
    public class AccountCoreData : ThirdPartyServiceConsumption, IAccountCoreData
    {
        private static IConfiguration? _configuration;
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<LogOnRespons> AuthenticateUser(LogOnRequest request)
        {
            Logger.InfoFormat("Authentication Started :: Core.Data :: {0}", request.UserName);
            try
            {               
                string otpExpireMins = "1";
                Logger.Info("Otp at Accountcoredate ::" + request.otp);
                using (var dbConsumer = new CoreDBConsumer())
                {
                    request.UserName = AppInternalEncKey.Decrypt(request.UserName, false);
                    var user = await dbConsumer.DbConsumer<LogOnRespons>("ValidateUser", SqlEventTypes.Select,
                        new Dictionary<string, object>
                        {
                            {"@strUsername", request.UserName},
                            {"@strPassword", request.Password},
                            {"@strMacAddress", AppInternalEncKey.Decrypt(request.MacAddress, false)},
                            {"@strIPAddress", AppInternalEncKey.Decrypt(request.IpAddress, false)},
                            {"@strUserSessionID", AppInternalEncKey.Decrypt(request.UserSessionId, false)},
                            {"@otp", AppInternalEncKey.Decrypt(request.otp == null ? "0" :request.otp ,false)},
                            {"@otpExpireMins", int.Parse(otpExpireMins)},
                            {"@ReturnValue", DBNull.Value}
                        });
                    Logger.Info("Web Login User Ip Address requested :: " + request.IpAddress);
                    Logger.Info("Procedure Excution Done :: return value " + user.ReturnValue);
                    switch (user.ReturnValue)
                    {
                        case (int)ProcReturnType.UserNotExisted:
                            return new LogOnRespons { ActionResult = ActionStatus.InvalidUser };
                        case (int)ProcReturnType.CustomerExpired:
                            return new LogOnRespons { ActionResult = ActionStatus.CustomerExpired };
                        case (int)ProcReturnType.UserSessionsExceeded:
                            return new LogOnRespons { ActionResult = ActionStatus.UserSessionsExceeded };
                        case (int)ProcReturnType.UserNotLdapUser:
                            return new LogOnRespons { ActionResult = ActionStatus.InvalidUser };
                        case (int)ProcReturnType.NetworkInstenceError:
                            return new LogOnRespons { ActionResult = ActionStatus.NetworkInstenceError };
                        case (int)ProcReturnType.CustomerPendingOrRejectStatus:
                            return new LogOnRespons { ActionResult = ActionStatus.CustomerPendingOrRejectStatus };
                        case (int)ProcReturnType.UserLocked:
                            return new LogOnRespons { ActionResult = ActionStatus.Locked };
                        case (int)ProcReturnType.IncompleteConfiguration:
                            return new LogOnRespons { ActionResult = ActionStatus.IncompleteConfiguration };
                        case (int)ProcReturnType.OTP:
                            //return new LogOnRespons { ActionResult = ActionStatus.OTP };
                            UpdateUnFreezUserHistory(user.UserHistoryIdentity, user.Id, dbConsumer);
                            return user;
                        case (int)ProcReturnType.Freeze:
                            //return new LogOnRespons { ActionResult = ActionStatus.Freeze };
                            //UpdateUnFreezUserHistory(user.UserHistoryIdentity, user.Id, dbConsumer);
                            return user;
                        case (int)ProcReturnType.InvalidIpAddress:
                            return new LogOnRespons
                            {
                                ActionResult = ActionStatus.InvalidIpAddress,
                                MobileNo = user.MobileNo,
                                UserId = user.UserId,
                                CustomerId = user.CustomerId,
                                OTPTime = user.OTPTime,
                            };
                        case (int)ProcReturnType.OtpExpire:
                            return new LogOnRespons { ActionResult = ActionStatus.OtpExpire };
                        case (int)ProcReturnType.InvalidUser:
                            UpdateUserHistory(user.UserHistoryIdentity, user.Id, dbConsumer);
                            return new LogOnRespons { ActionResult = ActionStatus.InvalidUser };
                        default:
                            Logger.Info("Result Success");
                            user.ActionResult = ActionStatus.Success;
                            if (!request.IsWebRequest.Equals(user.Web))
                            {
                                return new LogOnRespons { ActionResult = ActionStatus.InValidRequest };
                            }
                            if (user.LDap)
                            {
                                //To Fix Warning:'System.Configuration.ConfigurationSettings.AppSettings' is obsolete: 'This method is obsolete, it has been replaced by System.Configuration!System.Configuration.ConfigurationManager.AppSettings'
                                //ConfigurationSettings changed to ConfigurationManager on 13Jan2020
                                if (IsValidLdapUser(request.UserName,
                                    AppInternalEncKey.Decrypt(request.Password, false),
                                    _configuration["ADServiceConnString"]))
                                {
                                    UpdateUserHistory(user.UserHistoryIdentity, user.Id, dbConsumer, 1);
                                    return user;
                                }
                                else
                                {
                                    return UpdateUserHistory(user.UserHistoryIdentity, user.Id, dbConsumer) ?? user;
                                }
                            }
                            else
                            {
                                if (user.Password.Equals(request.Password))
                                {
                                    UpdateUserHistory(user.UserHistoryIdentity, user.Id, dbConsumer, 1);
                                    return user;
                                }
                                else
                                {
                                    UpdateUserHistory(user.UserHistoryIdentity, user.Id, dbConsumer);
                                    Logger.Info("Reason :: Invalid Password :: UserName :" + user.UserName);
                                    return new LogOnRespons { ActionResult = ActionStatus.InvalidUser };
                                }

                                //if (user.RoleId == 1)
                                //{
                                //    if (user.Password.Equals(request.Password))
                                //        return user;
                                //    else
                                //        return new LogOnRespons { ActionResult = ActionStatus.InvalidUser };
                                //}
                                //else
                                //{
                                //    return new LogOnRespons { ActionResult = ActionStatus.InvalidUser };
                                //}
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorString = ex.Message;
                Logger.ErrorFormat("Authenticate User :: User Name :- {1} :: Error - {0}", request.UserName,
                    ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new LogOnRespons { ActionResult = ActionStatus.Errored };
        }


        private LogOnRespons UpdateUserHistory(int historyId, int userId, CoreDBConsumer dbConsumer, int IsSuccess = 0)
        {
            Logger.InfoFormat("UpdateUserHistory :: Started :: User Id :: {0}", userId);
            try
            {
                var HistoryTable = dbConsumer.DbConsumerForMultiItems<DataTable>("UnlockUserId", SqlEventTypes.Select,
                    new Dictionary<string, object>
                    {
                        {"@Id", historyId},
                        {"@UserId", userId},
                        {"@IsSuccess", IsSuccess}
                    });
                if (HistoryTable.Result != null && HistoryTable.Result[0] != null &&
                    HistoryTable.Result[0].Rows.Count > 2)
                {
                    return new LogOnRespons { ActionResult = ActionStatus.Locked };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("UpdateUserHistory :: userId :- {1} :: Error - {0}", ex.ToString(), userId);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }

        private LogOnRespons UpdateUnFreezUserHistory(int historyId, int userId, CoreDBConsumer dbConsumer, int IsSuccess = 0)
        {
            Logger.InfoFormat("UpdateUserHistory :: Started :: User Id :: {0}", userId);
            try
            {
                var HistoryTable = dbConsumer.DbConsumerForMultiItems<DataTable>("UnFreezUserId", SqlEventTypes.Select,
                    new Dictionary<string, object>
                    {
                        {"@Id", historyId},
                        {"@UserId", userId},
                        {"@IsSuccess", IsSuccess}
                    });
                if (HistoryTable.Result != null && HistoryTable.Result[0] != null &&
                    HistoryTable.Result[0].Rows.Count > 2)
                {
                    return new LogOnRespons { ActionResult = ActionStatus.Freeze };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("UpdateUserHistory :: userId :- {1} :: Error - {0}", ex.ToString(), userId);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }

        public async Task<LogOnRespons> AuthenticateUserLogout(LogOnRequest request)
        {
            Logger.InfoFormat("AuthenticateUserLogout :: Core.Data :: {0}", request.UserId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var user = await dbConsumer.DbConsumer<LogOnRespons>("Logout", SqlEventTypes.Select,
                        new Dictionary<string, object>
                        {
                            {"@UserId", request.UserId},
                            {"@strMacAddress", AppInternalEncKey.Decrypt(request.MacAddress, false)},
                            {"@ipAddress", AppInternalEncKey.Decrypt(request.IpAddress, false)},
                            {"@ReturnValue", DBNull.Value}
                        });
                    Logger.InfoFormat("AuthenticateUserLogout :: End ::LogOut :: {0} ", user.ReturnValue);
                }
            }
            catch (Exception ex)
            {
                var errorString = ex.Message;
                Logger.ErrorFormat("AuthenticateUserLogout :: User Id :- {1} :: Error - {0}", request.UserId,
                    ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new LogOnRespons { ActionResult = ActionStatus.Errored };
        }

        public async Task<PreferencesResponse> GetAllPreferencesAsync(ReUsableRequest request)
        {
            Logger.InfoFormat("GetAllPreferencesAsync :: Started :: {0}", request.CustomerId);
            var prefrencesResponse = new PreferencesResponse();
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allPreferences =
                        await dbConsumer.DbConsumerForMultiItems<Preferences>("passwordpreferences",
                            SqlEventTypes.Select, new Dictionary<string, object> { { "@custid", request.CustomerId } });
                    Logger.InfoFormat("GetAllPreferencesAsync :: End ::Preferences :: {0} ", allPreferences.Count);

                    return new PreferencesResponse
                    {
                        Preferences = allPreferences,
                        ActionStatus = allPreferences != null ? ActionStatus.Success : ActionStatus.Fail
                    };
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.ErrorFormat("GetAllPreferencesAsync :: {0} :: Error :: {1}", request.CustomerId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new PreferencesResponse { ActionStatus = ActionStatus.Errored };
        }

        public async Task<GlobalUsageProperties> GetAllRolesAsync(ReUsableRequest request)
        {
            Logger.InfoFormat("GetAllRolesAsync :: Started :: {0}", request.CustomerId);
            // if(request.CustomerId < 0)

            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allRoles =
                        await
                            dbConsumer.DbConsumerForMultiItems<Roles>("GETROLES", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@CustomerId", request.CustomerId } });
                    Logger.InfoFormat("GetAllRolesAsync :: End ::Roles :: {0} ", allRoles.Count);
                    return new GlobalUsageProperties
                    {
                        Roles = allRoles
                    };
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.ErrorFormat("GetAllRolesAsync :: {0} :: Error :: {1}", request.CustomerId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }
        //Prefered Route
        #region Prefered Route start
        public async Task<GlobalUsageProperties> GetAllPreferred()
        {
            Logger.Info("GetAllPreferred :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allPrefered =
                        await
                            dbConsumer.DbConsumerForMultiItems<PreferedCountryList>("GetPreferedCountry",
                                SqlEventTypes.Select);
                    Logger.Info("GetAllPreferred :: End  ");
                    return new GlobalUsageProperties
                    {
                        PreferedCountryList = allPrefered
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllPreferred :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetAllPreferredCountry()
        {
            Logger.Info("GetAllPreferredCountry :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allPreferred =
                        await dbConsumer.DbConsumerForMultiItems<PreferedList>("PreferedDetails", SqlEventTypes.Select);
                    Logger.Info("GetAllPreferredCountry :: End  ");
                    return new GlobalUsageProperties
                    {
                        PreferedList = allPreferred
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllPreferredCountry :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }


        #endregion

        public async Task<List<CountryGlobalTable>> GetAllCountriesAsync()
        {
            Logger.Info("GetAllCountriesAsync :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allCountries =
                        await
                            dbConsumer.DbConsumerForMultiItems<CountryGlobalTable>("GetCountrys", SqlEventTypes.Select);
                    Logger.Info("GetAllCountriesAsync :: End  ");
                    return allCountries;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Errore :: GetAllCountriesAsync :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<CountryGlobalTable>();
        }

        public async Task<List<ConnectionList>> GetAllSMSCAsync()
        {
            Logger.Info("GetAllSMSCAsync :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allSMSC = await dbConsumer.DbConsumerForMultiItems<ConnectionList>("GetSMSC", SqlEventTypes.Select);
                    Logger.Info("GetAllSMSCAsync :: End  ");
                    return allSMSC;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Errore :: GetAllSMSCAsync :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<ConnectionList>();
        }
        public async Task<List<ConnectionListRate>> GetAllSMSCAsyncRate()
        {
            Logger.Info("GetAllSMSCAsync Rate :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allSMSC = await dbConsumer.DbConsumerForMultiItems<ConnectionListRate>("GetSMSC_Rate", SqlEventTypes.Select);
                    Logger.Info("GetAllSMSCAsync Rate :: End  ");
                    return allSMSC;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Errore :: GetAllSMSCAsync Rate :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<ConnectionListRate>();
        }


        public async Task<GlobalUsageProperties> GetAllDivisionsColomnsAsync(ReUsableRequest request)
        {
            Logger.InfoFormat("GetAllDivisionsColomnsAsync :: Started :: {0}", request.CustomerId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allUserDivisions =
                        await dbConsumer.DbConsumerForMultiItems<DivisionTable>("GetUserDivision", SqlEventTypes.Select,
                            new Dictionary<string, object> { { "@CustomerId", request.CustomerId } });
                    Logger.InfoFormat("GetAllDivisionsColomnsAsync :: End ::Divisions :: {0} ", allUserDivisions.Count);
                    return new GlobalUsageProperties
                    {
                        DivisionTable = allUserDivisions
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetAllDivisionsColomnsAsync :: {0} :: Error :: {1}", request.CustomerId,
                    ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetAllMailboxAsync()
        {
            Logger.Info("GetAllMailboxAsync :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var MailUser =
                        await dbConsumer.DbConsumerForMultiItems<MailList>("GetMailboxDetails", SqlEventTypes.Select);
                    Logger.Info("GetAllMailboxAsync :: End  ");
                    return new GlobalUsageProperties
                    {
                        MailList = MailUser
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllMailboxAsync", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetAllCustomerColomnsAsync(ReUsableRequest request)
        {
            Logger.InfoFormat("GetAllCustomerColomnsAsync :: Started :: {0}", request.ParentId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allUserCustomers =
                        await
                            dbConsumer.DbConsumerForMultiItems<CustomerList>("CustomerDetails", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@ParentId", request.ParentId } });
                    Logger.InfoFormat("GetAllCustomerColomnsAsync :: End ::AllCustomerColomn :: {0} ",
                        allUserCustomers.Count);
                    return new GlobalUsageProperties
                    {
                        CustomerList = allUserCustomers
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetAllCustomerColomnsAsync :: {0} :: Error :: {1}", request.ParentId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetAllCustomerColomnsAsyncRep(ReUsableRequest request)
        {
            Logger.InfoFormat("GetAllCustomerColomnsAsyncRep :: Started :: {0}", request.ParentId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allUserCustomers =
                        await
                            dbConsumer.DbConsumerForMultiItems<CustomerList>("CustomerDetailsReports", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@ParentId", request.ParentId } });
                    Logger.InfoFormat("GetAllCustomerColomnsAsyncRep :: End ::AllCustomerColomn :: {0} ",
                        allUserCustomers.Count);
                    return new GlobalUsageProperties
                    {
                        CustomerList = allUserCustomers
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetAllCustomerColomnsAsyncRep :: {0} :: Error :: {1}", request.ParentId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetCustomersForRoles(ReUsableRequest request)
        {
            Logger.InfoFormat("GetCustomersForRoles :: Started :: {0}", request.ParentId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allUserCustomers =
                        await
                            dbConsumer.DbConsumerForMultiItems<CustomerList>("CustomersForRoles", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@ParentId", request.ParentId } });
                    Logger.InfoFormat("GetCustomersForRoles :: End ::AllCustomerColomn :: {0} ",
                        allUserCustomers.Count);
                    return new GlobalUsageProperties
                    {
                        CustomerList = allUserCustomers
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetCustomersForRoles :: {0} :: Error :: {1}", request.ParentId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<List<CustomerList>> GetCustomersByAccountManagerId(ReUsableRequest request)
        {
            Logger.InfoFormat("GetCustomersByAccountManagerId :: Started :: {0}", request.AccountManagerId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var accounmgrCustomers =
                        await
                            dbConsumer.DbConsumerForMultiItems<CustomerList>("GetAccountMgrCustomers",
                                SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@AccountMgrId", request.AccountManagerId } });
                    Logger.InfoFormat("GetCustomersByAccountManagerId :: End ::CustomersByAccountManagerId :: {0} ",
                        accounmgrCustomers.Count);
                    return accounmgrCustomers;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat(
                    "Account Core :: GetCustomersByAccountManagerId :: Account Manager Id :- {0} & Error :: {1}",
                    request.AccountManagerId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<CustomerList>();
        }

        public async Task<GlobalUsageProperties> GetAllUsersAsync()
        {
            Logger.Info("GetAllUsersAsync :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allUsers =
                        await dbConsumer.DbConsumerForMultiItems<UsersList>("UserDetails", SqlEventTypes.Select);
                    Logger.Info("GetAllUsersAsync :: End  ");
                    return new GlobalUsageProperties
                    {
                        UsersList = allUsers
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllUsersAsync :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }


        public async Task<GlobalUsageProperties> GetAllSendersAsync()
        {
            Logger.Info("GetAllSendersAsync :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allSenders =
                        await dbConsumer.DbConsumerForMultiItems<SendersList>("SenderDetails", SqlEventTypes.Select);
                    Logger.Info("GetAllSendersAsync :: End  ");
                    return new GlobalUsageProperties
                    {
                        SendersList = allSenders
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllSenderAsync :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<int> GetDashBoardTypeAsync(ReUsableRequest request)
        {
            Logger.Info("GetDashBoardType (MO/MT) :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var utype = await dbConsumer.DbConsumer<ReUsableResponse>("GetDashboardInfo", SqlEventTypes.Select,
                        new Dictionary<string, object> { { "@UserId", request.UserId }, { "@NRETVAL", DBNull.Value } });
                    Logger.Info("GetDashBoardTypeAsync  (MO/MT) :: End  ");
                    return utype.DashboardInfo;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetDashBoardTypeAsync", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return 0;
        }

        public async Task<int> GetAvailableCreditsAsync(ReUsableRequest request)
        {
            Logger.Info("GetAvailableCreditsAsync :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var AvailCredits =
                        await dbConsumer.DbConsumer<LogOnRespons>("GetCustomerCreditDetails", SqlEventTypes.Select,
                            new Dictionary<string, object> { { "@CustomerId", request.CustomerId } });
                    Logger.Info("GetAvailableCreditsAsync :: End  ");
                    return AvailCredits == null ? 0 : AvailCredits.AvailableCredits;

                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAvailableCreditsAsync :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return 0;
        }



        public async Task<GlobalUsageProperties> GetAllVendorsRate()
        {
            Logger.Info("GetAllVendorsRate  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allVendors =
                        await dbConsumer.DbConsumerForMultiItems<VendorsNameList>("VendorDetails", SqlEventTypes.Select);
                    Logger.Info("GetAllVendorsRate :: End  ");
                    return new GlobalUsageProperties
                    {
                        VendorsNameList = allVendors
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllVendorsRate :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }
        public async Task<GlobalUsageProperties> GetAllOperatorsRate()
        {
            Logger.Info("GetOperatorsRate :: Started :: {0}");
            try
            {
                using (var dbconsumer = new CoreDBConsumer())
                {
                    var allOperators = await dbconsumer.DbConsumerForMultiItems<OperatorListRate>
                        ("GetOperators_Rate", SqlEventTypes.Select);
                    Logger.Info("GetOperatorsRate :: End  ");
                    return new GlobalUsageProperties
                    {
                        OperatorListRate = allOperators
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fata Error occured while exectuing GetOperatorsRate", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return null;
        }
        public async Task<GlobalUsageProperties> GetAllPackageRate()
        {
            Logger.Info("GetAllPackageRate  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allPackages =
                        await
                            dbConsumer.DbConsumerForMultiItems<PackageList>("GetPackagesByVendor", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@NVendorid", 0 } });
                    Logger.Info("GetAllPackageRate :: End  ");
                    return new GlobalUsageProperties
                    {
                        PackageList = allPackages
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllPackageRate :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetAllPrefLists(ReUsableRequest request)
        {
            Logger.Info("GetAllCustPrefList  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allCustPrefList =
                        await
                            dbConsumer.DbConsumerForMultiItems<CustomerPrefList>("CustomerDetailsPref",
                                SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@ParentId", request.ParentId } });
                    Logger.Info("GetAllCustPrefList :: End  ");
                    return new GlobalUsageProperties
                    {
                        CustomerPrefList = allCustPrefList
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllPrefLists :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetAdminResellers(ReUsableRequest request)
        {
            Logger.Info("GetAdminResellers  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var GetAdminResellers =
                        await
                            dbConsumer.DbConsumerForMultiItems<AdminResellers>("GetAdminResellers", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@ParentId", request.ParentId } });
                    Logger.Info("GetAdminResellers :: End  ");
                    return new GlobalUsageProperties
                    {
                        AdminResellers = GetAdminResellers
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAdminResellers :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        private bool IsValidLdapUser(string userName, string password, string domain)
        {
            var isValidUser = false;
            try
            {
                using (var pContext = new PrincipalContext(ContextType.Domain, domain, userName, password))
                {
                    isValidUser = pContext.ValidateCredentials(userName, password);
                    //var userPrincipal = UserPrincipal.FindByIdentity(pContext, IdentityType.SamAccountName, (domain + "\\" + userName));
                    //var temp = userPrincipal;
                }
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("IsValidLdapUser Started :: Core.Data :: {0}", userName);
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return isValidUser;
        }

        public LdapUseDetailsResponse IsUserExists(string userName)
        {
            Logger.Info("IsUserExists  :: Started :: {0}");
            try
            {
                using (
                    var domain = new PrincipalContext(ContextType.Domain,
                        System.Configuration.ConfigurationManager.AppSettings["ADServiceConnString"],
                        System.Configuration.ConfigurationManager.AppSettings["ADUser"], 
                        System.Configuration.ConfigurationManager.AppSettings["ADPassword"]))
                {
                    return
                        GetLdapUserInformation(UserPrincipal.FindByIdentity(domain, IdentityType.SamAccountName,
                            (domain + "\\" + userName)));
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: IsUserExists :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new LdapUseDetailsResponse { IsValidUser = false };
        }

        private List<string> GetLdapUserGroups(UserPrincipal lDapUser)
        {
            Logger.Info("GetLdapUserGroups  :: Started :: {0}");
            var userRoles = new List<string>();
            try
            {
                if (lDapUser != null)
                {
                    var de = (lDapUser.GetUnderlyingObject() as DirectoryEntry);
                    if (de != null)
                    {
                        var memberPropertyCollection = de.Properties.Cast<PropertyValueCollection>()
                            .FirstOrDefault(x => x.PropertyName.Equals("memberOf"));
                        if (memberPropertyCollection != null &&
                            memberPropertyCollection.Value != null)
                        {
                            var memberProperty = memberPropertyCollection.Value;
                            if (memberProperty.GetType() == typeof(object[]))
                            {
                                var objArray = ((object[])memberProperty).ToList();
                                userRoles.AddRange((from n in objArray
                                                    let nValue = n.ToString().Split(',')[0].Split('=')[1]
                                                    select nValue).ToList());
                                if (userRoles.Any())
                                {
                                    foreach (var o in userRoles)
                                    {
                                        // "Group Name : {0}", o);
                                    }
                                }
                            }
                            else
                            {
                                var strString = ((string)memberProperty).Split(',')[0].Split('=')[1];
                                userRoles.Add(strString);
                            }
                        }
                        else
                        {
                            // "Group Name :: Doesn't existed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetLdapUserGroups :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return userRoles;
        }

        private LdapUseDetailsResponse GetLdapUserInformation(UserPrincipal lDapUser)
        {
            try
            {
                if (lDapUser != null)
                {
                    return new LdapUseDetailsResponse
                    {
                        EmailAddress = lDapUser.EmailAddress,
                        GivenName = lDapUser.GivenName,
                        MiddleName = lDapUser.MiddleName,
                        Surname = lDapUser.Surname,
                        PhoneNo = lDapUser.VoiceTelephoneNumber,
                        IsValidUser =
                            !string.IsNullOrWhiteSpace(lDapUser.EmailAddress) ||
                            !string.IsNullOrWhiteSpace(lDapUser.GivenName) ||
                            !string.IsNullOrWhiteSpace(lDapUser.MiddleName) ||
                            !string.IsNullOrWhiteSpace(lDapUser.Surname) ||
                            !string.IsNullOrWhiteSpace(lDapUser.VoiceTelephoneNumber)
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetLdapUserInformation :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new LdapUseDetailsResponse { IsValidUser = false };
        }

        public async Task<List<UserMenuItems>> GetUserMenuItemsByUser(ReUsableRequest request)
        {
            Logger.InfoFormat("GetUserMenuItemsByUser :: Started :: {0}", request.UserId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var menItems =
                        await
                            dbConsumer.DbConsumerForMultiItems<UserMenuItems>("GetFeaturesByUser", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@userId", request.UserId } });
                    Logger.InfoFormat("GetUserMenuItemsByUser :: End :: UserMenuItems :: {0} ", menItems.Count());
                    return menItems;
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetUserMenuItemsByUser :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<UserMenuItems>();
        }

        public async Task<ApplicationGlobalVariables> GetAppGobalKeys()
        {
            Logger.Info("GetAppGobalKeys  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var dtResponse =
                        await dbConsumer.DbConsumerForMultiItems<DataTable>("GetAppGlobalKeys", SqlEventTypes.Select);
                    if (dtResponse != null && dtResponse[0] != null && dtResponse[0].Rows.Count > 0)
                    {
                        var response = new ApplicationGlobalVariables
                        {
                            FilterByDepartment =
                                getDtValueByProperty(dtResponse[0], "FilterByDepartment")
                                    .Equals("yes", StringComparison.OrdinalIgnoreCase),
                            AdDetails =
                                new AdDetails
                                {
                                    ServerName = getDtValueByProperty(dtResponse[0], "ADServer"),
                                    UserName = getDtValueByProperty(dtResponse[0], "ADUser"),
                                    Password = getDtValueByProperty(dtResponse[0], "ADPassword"),
                                },
                            PageGridSize = Convert.ToInt32(getDtValueByProperty(dtResponse[0], "PageSize")),
                            OutBoxQueSize = Convert.ToInt32(getDtValueByProperty(dtResponse[0], "ChunkValue")),
                            NoOfAttemptSmscRetry =
                                Convert.ToInt32(getDtValueByProperty(dtResponse[0], "RetryConnection")),
                            SmscRetryFrequencyInterval =
                                Convert.ToInt32(getDtValueByProperty(dtResponse[0], "RetryFrequency")),
                            CustomerCreditMargin = Convert.ToInt32(getDtValueByProperty(dtResponse[0], "CreditMargin")),
                            CustomerExpiryDateMargin =
                                Convert.ToInt32(getDtValueByProperty(dtResponse[0], "ExpiryMargin")),
                            CustomerCcMailAddress = getDtValueByProperty(dtResponse[0], "CCEmail"),
                            CustomerToMailAddress = getDtValueByProperty(dtResponse[0], "ToEmail")
                        };
                        Logger.Info("GetAppGobalKeys :: End  ");
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetUserMenuItemsByUser :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new ApplicationGlobalVariables();
        }

        private string getDtValueByProperty(DataTable dt, string columnValue)
        {
            try
            {
                var v_row = from DataRow row in dt.Rows
                            where (string)row["PropertyName"] == columnValue
                            select (string)row["PropertyValue"];
                return (v_row != null && v_row.FirstOrDefault() != null) ? v_row.FirstOrDefault().ToString() : "";
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: getDtValueByProperty :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return string.Empty;
        }

        //Added for AccountManager
        public async Task<GlobalUsageProperties> GetAccountManager()
        {
            Logger.Info("GetAccountManager  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var Account =
                        await
                            dbConsumer.DbConsumerForMultiItems<AccountManagerList>("AccountManager",
                                SqlEventTypes.Select);
                    Logger.Info("GetAccountManager :: End  ");
                    return new GlobalUsageProperties
                    {
                        AccountManagerList = Account
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAccountManager :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<List<CustomerAccountList>> GetCustomerAccountdrop(ReUsableRequest request)
        {
            Logger.InfoFormat("GetCustomerAccount :: Started :: {0}", request.AccMgrId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var customers =
                        await
                            dbConsumer.DbConsumerForMultiItems<DataTable>("AccountCustomerDropdown",
                                SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@AcmId", request.AccMgrId } });
                    if (customers != null)
                    {
                        var allCustomers = customers[0].ToList<CustomerAccountList>();
                        if (customers.Count > 0 && !string.IsNullOrWhiteSpace(request.AccMgrId))
                        {
                            var assignedCustomers =
                                await
                                    dbConsumer.DbConsumerForMultiItems<CustomerAccountList>(
                                        "accountcustomersecondresultset", SqlEventTypes.Select,
                                        new Dictionary<string, object> { { "@AcmId", request.AccMgrId } });
                            // var assignedCustomers = customers[1].ToList<CustomerAccountList>();
                            foreach (
                                var assignedItem in
                                    assignedCustomers.Select(item => allCustomers.FirstOrDefault(x => x.Id == item.Id))
                                        .Where(assignedItem => assignedItem != null))
                            {
                                assignedItem.IsAssigned = true;
                            }
                        }
                        return allCustomers;
                    }

                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.ErrorFormat("GetCustomerAccountdrop :: {0} :: Error :: {1}", request.ParentId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<CustomerAccountList>();
        }

        public async Task<List<CustomerAccountList>> GetCustomerAccount(ReUsableRequest request)
        {
            Logger.InfoFormat("GetCustomerAccount :: Started :: {0}", request.AccMgrId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var customers =
                        await
                            dbConsumer.DbConsumerForMultiItems<DataTable>("AccountCustomer", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@AcmId", request.AccMgrId } });
                    if (customers != null)
                    {
                        var allCustomers = customers[0].ToList<CustomerAccountList>();
                        if (customers.Count > 0 && !string.IsNullOrWhiteSpace(request.AccMgrId))
                        {
                            var assignedCustomers =
                                await
                                    dbConsumer.DbConsumerForMultiItems<CustomerAccountList>(
                                        "accountcustomersecondresultset", SqlEventTypes.Select,
                                        new Dictionary<string, object> { { "@AcmId", request.AccMgrId } });
                            // var assignedCustomers = customers[1].ToList<CustomerAccountList>();
                            foreach (
                                var assignedItem in
                                    assignedCustomers.Select(item => allCustomers.FirstOrDefault(x => x.Id == item.Id))
                                        .Where(assignedItem => assignedItem != null))
                            {
                                assignedItem.IsAssigned = true;
                            }
                        }
                        return allCustomers;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.ErrorFormat("GetCustomerAccount :: {0} :: Error :: {1}", request.ParentId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<CustomerAccountList>();

        }

        //SmsTracking

        //public async Task<GlobalUsageProperties> GetCountryAccount()
        //{
        //    try
        //    {
        //        using (var dbConsumer = new CoreDBConsumer())
        //        {
        //            var allcountry = await dbConsumer.DbConsumerForMultiItems<CountryList>("GetReportCountrys", SqlEventTypes.Select);
        //            return new GlobalUsageProperties()
        //            {
        //                CountryList = allcountry
        //            };
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }
        //    return new GlobalUsageProperties();
        //}

        //public async Task<GlobalUsageProperties> GetOperatorAccount()
        //{
        //    try
        //    {
        //        using (var dbConsumer = new CoreDBConsumer())
        //        {
        //            var allOprator = await dbConsumer.DbConsumerForMultiItems<CountryList>("GetReportOperators", SqlEventTypes.Select);
        //            return new GlobalUsageProperties()
        //            {
        //                CountryList = allOprator
        //            };
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var error = ex.Message;
        //    }
        //    return new GlobalUsageProperties();
        //}

        public async Task<GlobalUsageProperties> GetReportcustomer(ReUsableRequest request)
        {
            Logger.Info("GetReportcustomer  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allcustomer =
                        await
                            dbConsumer.DbConsumerForMultiItems<CustomerViewlist>("ReportCustomers", SqlEventTypes.Select,
                                new Dictionary<string, object>
                                {
                                    {"@CustId", request.CustomerId},
                                    {"@UserId", request.UserId},
                                    {"@RoleId", request.RoleId}
                                });
                    Logger.Info("GetReportcustomer :: End  ");
                    return new GlobalUsageProperties()
                    {
                        CustomerViewlist = allcustomer
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetReportcustomer :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetVendor()
        {
            Logger.Info("GetVendor  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allVendor =
                        await dbConsumer.DbConsumerForMultiItems<VendorViewlist>("ReportVendors", SqlEventTypes.Select);
                    Logger.Info("GetVendor :: End  ");
                    return new GlobalUsageProperties()
                    {
                        VendorViewlist = allVendor
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetVendor :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetMobility()
        {
            Logger.Info("GetMobility  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var Mobility =
                        await dbConsumer.DbConsumerForMultiItems<MobilityList>("GetMobility", SqlEventTypes.Select);
                    Logger.Info("GetMobility :: End  ");
                    return new GlobalUsageProperties()
                    {
                        MobilityList = Mobility
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetMobility :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }


        public async Task<List<MOShortCodeList>> GetAllMOShortcode()
        {
            Logger.Info("GetAllMOShortcode  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var MOShortCodeList =
                        await
                            dbConsumer.DbConsumerForMultiItems<MOShortCodeList>("GetMOShortcode", SqlEventTypes.Select);
                    Logger.Info("GetAllMOShortcode :: End  ");
                    return MOShortCodeList;

                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetAllMOShortcode :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new List<MOShortCodeList>();
        }

        public async Task<GlobalUsageProperties> GetModule()
        {
            Logger.Info("GetModule  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var Module =
                        await dbConsumer.DbConsumerForMultiItems<ModuleList>("GetActiveModules", SqlEventTypes.Select);
                    Logger.Info("GetModule :: End  ");
                    return new GlobalUsageProperties()
                    {
                        ModuleList = Module
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetModule :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetStages()
        {
            Logger.Info("GetStages  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var Stages =
                        await dbConsumer.DbConsumerForMultiItems<StagesList>("GetSMPPStages", SqlEventTypes.Select);
                    Logger.Info("GetStages :: End  ");
                    return new GlobalUsageProperties()
                    {
                        StagesList = Stages
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetStages :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetSMPPIDAsync()
        {
            Logger.Info("GetSMPPIDAsync  :: Started :: {0}");
            try
            {
                //GetSMPPID  GetSMPPIdByUserId
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var SMPPID = await dbConsumer.DbConsumerForMultiItems<SMPPIDList>("GetSMPPID", SqlEventTypes.Select);
                    Logger.Info("GetSMPPIDAsync :: End  ");
                    return new GlobalUsageProperties()
                    {
                        SMPPIDList = SMPPID
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetSMPPIDAsync :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetOutboundSender()
        {
            Logger.Info("GetOutboundSender  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var Outbound =
                        await
                            dbConsumer.DbConsumerForMultiItems<OutboundSenderList>("GetOutboundSender",
                                SqlEventTypes.Select);
                    Logger.Info("GetOutboundSender :: End  ");
                    return new GlobalUsageProperties()
                    {
                        OutboundSenderList = Outbound
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetOutboundSender :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }

        public async Task<GlobalUsageProperties> GetShortCode()
        {
            Logger.Info("GetShortCode  :: Started :: {0}");
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var ShortCode =
                        await
                            dbConsumer.DbConsumerForMultiItems<ShortCodeList>("GetShortcodeByOutbound",
                                SqlEventTypes.Select);
                    Logger.Info("GetShortCode :: End  ");
                    return new GlobalUsageProperties()
                    {
                        ShortCodeList = ShortCode
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Fatal Error :: GetShortCode :: {0}", ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }
        public async Task<GlobalUsageProperties> GetAllStatusBoardCustomers(ReUsableRequest request)
        {
            Logger.InfoFormat("GetAllStatusBoardCustomers :: Started :: {0}", request.ParentId);
            try
            {
                using (var dbConsumer = new CoreDBConsumer())
                {
                    var allUserCustomers =
                        await
                            dbConsumer.DbConsumerForMultiItems<CustomerList>("StatusBoardCustomers_Sales", SqlEventTypes.Select,
                                new Dictionary<string, object> { { "@UserId", request.ParentId } });
                    Logger.InfoFormat("GetAllStatusBoardCustomers :: End ::AllCustomerColomn :: {0} ",
                        allUserCustomers.Count);
                    return new GlobalUsageProperties
                    {
                        CustomerList = allUserCustomers
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("GetAllStatusBoardCustomers :: {0} :: Error :: {1}", request.ParentId, ex.ToString());
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return new GlobalUsageProperties();
        }
    }
}
