using Core.DBAccess;
using Core.Models.Enums;
using Core.Models.Extensions;
using Core.Models.Helpers;
using Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Core.Data.Utilities
{
    public class CoreDBConsumer : DisposeBaseClass
    {
        private static IConfiguration? _configuration;
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }
        public static string GetConfiguration(string param)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Sets look-up folder to application directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            return configuration[param].ToString();
        }
        public async Task<T> DbConsumer<T>(string spName, SqlEventTypes sqlEvent,
            Dictionary<string, object> parameters = null, bool IsCustome = false, DBs dbName = DBs.Synapse) where T : new()
        {
            try
            {
                Logger.InfoFormat("DbConsumer :: {0}", "Going to call DB consumer {1} :: {2}", DateTime.Now.ToString("dd/MM/yyyy hh.mm.ss.fff"), spName);
                using (
                    var dbManager =
                        new DBManager(
                            IEnumerableExtension.ParseEnum<DataProvider>(
                                GetConfiguration("ADOProvider")))
                        {
                            ConnectionString = BuildConnection(dbName)//ConfigurationSettings.AppSettings["ConnectionString"]
                        })
                {
                    try
                    {
                        dbManager.Open(sqlEvent);
                    }
                    catch (Exception ex)
                    {
                        Logger.InfoFormat("Exception :: {0} DateTime:: {1}", ex.Message, DateTime.Now.ToString("dd/MM/yyyy hh.mm.ss.fff"));
                    }


                    dbManager.Parameters = GetConfiguration("ADOProvider") == "Npgsql"
                        ? buildNPGParameters(dbManager, parameters)
                        : buildParameters(dbManager, parameters, IsCustome);
                    switch (sqlEvent)
                    {
                        case SqlEventTypes.Insert:
                        case SqlEventTypes.Delete:
                        case SqlEventTypes.Update:
                            var eResult = dbManager.ExecuteNonQuery(CommandType.StoredProcedure, spName);
                            return (T)Convert.ChangeType(eResult, typeof(T));
                        case SqlEventTypes.Select:
                            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, spName);
                            if (ds != null && ds.Tables[0] != null)
                            {
                                try
                                {
                                    var result = ds.Tables[0].ToList<T>();
                                    if (result != null && result.Any())
                                        return (T)Convert.ChangeType(result.FirstOrDefault(), typeof(T));
                                }
                                catch (Exception ex)
                                {
                                    Logger.InfoFormat("Exception to convert ds to list {0} - DateTime:: {1}", ex.Message, DateTime.Now.ToString("dd/MM/yyyy hh.mm.ss.fff"));
                                }

                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (DbException exDb)
            {
                T item = new T();
                var prop = item.GetType().GetProperty("ReturnValue");
                prop.SetValue(item, 5, null);
                Logger.ErrorFormat("Authenticate User :: DB  Error - {0} & {1}", exDb.ToString(), exDb.StackTrace);
                return (T)Convert.ChangeType(item, typeof(T));
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.ErrorFormat("Authenticate User ::  Error - {0}", ex.ToString());
            }
            return await Task.Run(() => (T)Convert.ChangeType(null, typeof(T)));
        }

        // Method written on 3-May-2023
        public async Task<T> DbConsumerSelectQuery<T>(string selectQuery, SqlEventTypes sqlEvent, DBs dbName = DBs.Synapse) where T : new()
        {
            try
            {
                using (
                    var dbManager =
                        new DBManager(
                            IEnumerableExtension.ParseEnum<DataProvider>(
                                GetConfiguration("ADOProvider")))
                        {
                            ConnectionString = BuildConnection(dbName)
                        })
                {
                    try
                    {
                        dbManager.Open(sqlEvent);
                    }
                    catch (Exception ex)
                    {
                        Logger.InfoFormat("Exception :: {0} DateTime:: {1}", ex.Message, DateTime.Now.ToString("dd/MM/yyyy hh.mm.ss.fff"));
                    }

                    switch (sqlEvent)
                    {
                        case SqlEventTypes.Insert:
                        case SqlEventTypes.Delete:
                        case SqlEventTypes.Update:
                        case SqlEventTypes.Select:
                            var ds = dbManager.ExecuteDataSet(CommandType.Text, selectQuery);

                            if (ds != null && ds.Tables[0] != null)
                            {
                                try
                                {
                                    var result = ds.Tables[0].ToList<T>();

                                    if (result != null && result.Any())
                                        return (T)Convert.ChangeType(result.FirstOrDefault(), typeof(T));
                                }
                                catch (Exception ex)
                                {
                                    Logger.InfoFormat("Exception to convert ds to list {0} - DateTime:: {1}", ex.Message, DateTime.Now.ToString("dd/MM/yyyy hh.mm.ss.fff"));
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }
            }
            catch (DbException exDb)
            {
                T item = new T();
                var prop = item.GetType().GetProperty("ReturnValue");
                prop.SetValue(item, 5, null);
                Logger.ErrorFormat("Authenticate User :: DB  Error - {0} & {1}", exDb.ToString(), exDb.StackTrace);
                return (T)Convert.ChangeType(item, typeof(T));
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.ErrorFormat("Authenticate User ::  Error - {0}", ex.ToString());
            }
            return await Task.Run(() => (T)Convert.ChangeType(null, typeof(T)));
        }

        public async Task<List<T>> DbConsumerForMultiItems<T>(string spName, SqlEventTypes sqlEvent,
            Dictionary<string, object> parameters = null, bool IsCustome = false, DBs dbName = DBs.Synapse) where T : new()
        {
            try
            {
                using (
                    var dbManager =
                        new DBManager(
                            IEnumerableExtension.ParseEnum<DataProvider>(
                                GetConfiguration("ADOProvider")))
                        {
                            ConnectionString = BuildConnection(dbName)//ConfigurationSettings.AppSettings["ConnectionString"].ToString()
                        })
                {
                    dbManager.Open(sqlEvent);
                    dbManager.Parameters = buildParameters(dbManager, parameters, IsCustome);
                    var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, spName);
                    if (ds != null && ds.Tables[0] != null)
                    {
                        if (typeof(T) != typeof(DataTable))
                        {
                            var result = ds.Tables[0].ToList<T>();
                            return (List<T>)Convert.ChangeType(result, typeof(List<T>));
                        }
                        else
                        {
                            return (List<T>)Convert.ChangeType(ds.Tables.Cast<DataTable>().ToList(), typeof(List<T>));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return await Task.Run(() => (List<T>)Convert.ChangeType(null, typeof(List<T>)));
        }

        private IDbDataParameter[] buildParameters(DBManager dbManager, Dictionary<string, object> parameters = null, bool IsCustome = false)
        {
            if (parameters != null && parameters.Any())
            {
                switch ((DataProvider)
                    System.Enum.Parse(typeof(DataProvider), GetConfiguration("ADOProvider"),
                        true))
                {
                    case DataProvider.Npgsql:
                        var pgParams = parameters.ToDictionary(item => "_" + item.Key.Replace("@", ""),
                            item => item.Value);
                        var ReturnParam =
                            pgParams.Where(f => f.Key.Equals("_ReturnValue", StringComparison.OrdinalIgnoreCase));
                        if (ReturnParam.Any())
                        {
                            pgParams.Remove(ReturnParam.First().Key);
                        }
                        dbManager.CreateParameters(pgParams.Count);
                        if (IsCustome)
                        {
                            //dbManager.AddCustomParameters(pgParams);
                        }
                        else
                        {
                            dbManager.AddParametersRange(pgParams);
                        }
                        break;
                    default:
                        dbManager.CreateParameters(parameters.Count);
                        if (IsCustome)
                        {
                            //dbManager.AddCustomParameters(parameters);
                        }
                        else
                        {
                            dbManager.AddParametersRange(parameters);
                        }
                        break;
                }

                #region Commented code
                //if (
                //       (DataProvider)
                //           System.Enum.Parse(typeof(DataProvider), ConfigurationSettings.AppSettings["ADOProvider"],
                //               true) ==
                //       DataProvider.Npgsql)
                //{
                //    var pgParams = parameters.ToDictionary(item => "_" + item.Key.Replace("@", ""), item => item.Value);
                //    var ReturnParam =
                //        pgParams.Where(f => f.Key.Equals("ReturnValue", StringComparison.OrdinalIgnoreCase));
                //    if (ReturnParam.Any())
                //    {
                //        pgParams.Remove(ReturnParam.First().Key);
                //    }
                //    dbManager.CreateParameters(pgParams.Count);
                //    if (IsCustome)
                //    {
                //        //dbManager.AddCustomParameters(pgParams);
                //    }
                //    else
                //    {
                //        dbManager.AddParametersRange(pgParams);
                //    }
                //}
                //else
                //{
                //    dbManager.CreateParameters(parameters.Count);
                //    if (IsCustome)
                //    {
                //        //dbManager.AddCustomParameters(parameters);
                //    }
                //    else
                //    {
                //        dbManager.AddParametersRange(parameters);
                //    }
                //}
                #endregion

                return dbManager.Parameters;
            }
            return null;
        }

        private IDbDataParameter[] buildNPGParameters(DBManager dbManager, Dictionary<string, object> parameters = null)
        {
            if (parameters != null && parameters.Any())
            {
                switch ((DataProvider)
                    System.Enum.Parse(typeof(DataProvider), GetConfiguration("ADOProvider"), true))
                {
                    case DataProvider.Npgsql:
                        var pgParams = parameters.ToDictionary(item => "_" + item.Key.Replace("@", ""),
                            item => item.Value);
                        var ReturnParam =
                            pgParams.Where(f => f.Key.Equals("_ReturnValue", StringComparison.OrdinalIgnoreCase));
                        if (ReturnParam.Any())
                        {
                            pgParams.Remove(ReturnParam.First().Key);
                        }
                        dbManager.CreateParameters(pgParams.Count);
                        dbManager.AddParametersRange(pgParams);
                        break;
                    default:
                        dbManager.CreateParameters(parameters.Count);
                        dbManager.AddParametersRange(parameters);
                        break;
                }

                #region Commented Code
                //if (
                //    (DataProvider)
                //        System.Enum.Parse(typeof(DataProvider), ConfigurationSettings.AppSettings["ADOProvider"], true) ==
                //    DataProvider.Npgsql)
                //{
                //    var pgParams = parameters.ToDictionary(item => "_" + item.Key.Replace("@", ""), item => item.Value);
                //    var ReturnParam =
                //        pgParams.Where(f => f.Key.Equals("ReturnValue", StringComparison.OrdinalIgnoreCase));
                //    if (ReturnParam.Any())
                //    {
                //        pgParams.Remove(ReturnParam.First().Key);
                //    }
                //    dbManager.CreateParameters(pgParams.Count);
                //    dbManager.AddParametersRange(pgParams);
                //}
                //else
                //{
                //    dbManager.CreateParameters(parameters.Count);
                //    dbManager.AddParametersRange(parameters);
                //}
                #endregion

                return dbManager.Parameters;
            }
            return null;
        }

        private string BuildConnection(DBs dbName)
        {
            switch (dbName)
            {
                case DBs.ReportsEngine:
                    return AppInternalEncKey.Decrypt(GetConfiguration("ReportsConnectionString").ToString(), false);
                case DBs.DND:
                    return AppInternalEncKey.Decrypt(GetConfiguration("DNDConnectionString").ToString(), false);
                case DBs.ExternalDB:
                    return AppInternalEncKey.Decrypt(GetConfiguration("ExternalDBConnectionString").ToString(), false);
                default:
                    return AppInternalEncKey.Decrypt(GetConfiguration("ConnectionString").ToString(), false);
            }
        }

        public string? GetConnectioString(bool IsCustome = false, DBs dbName = DBs.Synapse)
        {
            try
            {
                var dbManager =
                    new DBManager(
                        IEnumerableExtension.ParseEnum<DataProvider>(
                            GetConfiguration("ADOProvider")))
                    {
                        ConnectionString = BuildConnection(dbName)
                    };
                return dbManager.ConnectionString;

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return "";
        }
    }
}
