using Core.Models.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.Administration;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using UriBuilder = ClientHTTPConsuming.Utilities.UriBuilder;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace Core.Data.Data.Services
{
    public class ThirdPartyServiceConsumption
    {        
        public string? BaseServiceHostUrl
        {
            get
            {
                return !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["AdminNotificationUri"])
                    ? System.Configuration.ConfigurationManager.AppSettings["AdminNotificationUri"]
                    : "http://localhost/vitisrefreshapp/";
            }
        }

        public UriBuilder GetUriBuilderForServiceMethod(string suffix)
        {
            return new UriBuilder(BaseServiceHostUrl + suffix);
        }

        /// <summary>
        /// Send the request to third party services to update resource information for consuming engine services.
        /// </summary>        
        /// <param name="param"></param>
        /// <returns></returns>
        public string? GenerateCacheXmlCallOne(string param)
        {
            //BaseAddress = new Uri("http://localhost/VitisRefreshAppApi/")
            var resString = "";
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["AdminNotificationUri"];

                var cons = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                cons.DefaultRequestHeaders.Accept.Clear();
                cons.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //var res = cons.GetAsync("RefreshConfiguration/Server");
                var res = cons.GetAsync(System.Configuration.ConfigurationManager.AppSettings["NotificationOne"] + param);
                res.Result.EnsureSuccessStatusCode();
                if (res.Result.IsSuccessStatusCode)
                {
                    resString = res.Result.Content.ReadAsStringAsync().Result;                    
                }
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("Core.Data :: ThirdPartyServiceConsumption :: GenerateCacheXmlCallOne :: {0}",
                    ex.ToString());
            }
            return resString;           

        }

        public string? DNDNotification(string action, string tableName, string[] contacts)
        {
            //BaseAddress = new Uri("http://localhost/store/api/all/")
            Logger.InfoFormat("DNDNotification :: start ", action, tableName, contacts);
            var resString = "";
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["DNDNotificationURL"];
                var cons = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                var requestModel = new DndPushNotificationRequest { action = action, tableName = tableName, msisdns = contacts };
                cons.DefaultRequestHeaders.Accept.Clear();
                cons.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var res = cons.PostAsync(System.Configuration.ConfigurationManager.AppSettings["DNDNotification"], new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json"));
                res.Result.EnsureSuccessStatusCode();
                if (res.Result.IsSuccessStatusCode)
                {
                    resString = res.Result.Content.ReadAsStringAsync().Result;
                }
                Logger.InfoFormat("DNDNotification :: ends :: {0}", action, tableName, contacts);
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("Core.Data :: ThirdPartyServiceConsumption :: DNDNotification :: {0}",
                    ex.ToString());
            }
            return resString;
        }

        
        /// <summary>
        /// Send the request to third party services to update resource information for consuming engine services.
        /// </summary>        
        /// <param name="param"></param>
        /// <returns></returns>
        public string? AdminNotificationCallOne(string param)
        {
            //BaseAddress = new Uri("http://localhost/VitisRefreshAppApi/")

            var resString = "";
            //try
            //{
            //    var url = System.Configuration.ConfigurationManager.AppSettings["AdminNotificationUri"];

            //    var cons = new HttpClient
            //    {
            //        BaseAddress = new Uri(url)
            //    };
            //    cons.DefaultRequestHeaders.Accept.Clear();
            //    cons.DefaultRequestHeaders.Accept.Add(
            //        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //    //var res = cons.GetAsync("RefreshConfiguration/Server");
            //    var res = cons.GetAsync(System.Configuration.ConfigurationManager.AppSettings["NotificationOne"] + param);
            //    res.Result.EnsureSuccessStatusCode();
            //    if (res.Result.IsSuccessStatusCode)
            //    {
            //        resString = res.Result.Content.ReadAsStringAsync().Result;
            //        //Console.WriteLine(resString);
            //        //Console.ReadLine();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.InfoFormat("Core.Data :: ThirdPartyServiceConsumption :: AdminNotificationCallOne :: {0}",
            //        ex.ToString());
            //}
            return resString;

            # region Commented

            //var retVal = "";
            //var url = string.Format(System.Configuration.ConfigurationManager.AppSettings["AdminNotificationUri"] + "?Type={0}", param);            
            //try
            //{
            //    var myRequest = WebRequest.Create(url);
            //    var myWebResponse = myRequest.GetResponse();
            //    var strResponseCode = (int)((HttpWebResponse)myWebResponse).StatusCode;
            //    var myStream = myWebResponse.GetResponseStream();
            //    if (myStream != null)
            //    {
            //        var sr = new StreamReader(myStream);
            //        var readBuff = new Char[257];
            //        var count = sr.Read(readBuff, 0, 256);
            //        while (count > 0)
            //        {
            //            var str = new String(readBuff, 0, count);
            //            retVal = retVal + str;
            //            count = sr.Read(readBuff, 0, 256);
            //        }
            //        sr.Close();
            //        myStream.Close();
            //        myStream.Dispose();
            //    }
            //    myWebResponse.Close();
            //    myWebResponse.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    Logger.ErrorFormat("Core.Data :: ThirdPartyServiceConsumption :: AdminNotificationCallOne :: {0}", ex.ToString());
            //}
            // return retVal;

            # endregion 
            
        }

        /// <summary>
        /// Send the request to third party services to update resource information for consuming engine services.
        /// </summary>        
        /// <param name="param"></param>
        /// <param name="smscid"></param>
        /// <returns></returns>
        public string? AdminNotificationCallTwo(string param, string smscid, string groupname)
        {
            //BaseAddress = new Uri("http://localhost/VitisRefreshAppApi/")

                var resString = "";
                try
                {
                    var url = System.Configuration.ConfigurationManager.AppSettings["AdminNotificationUri"];

                    var cons = new HttpClient
                    {
                        BaseAddress = new Uri(url)
                    };
                    cons.DefaultRequestHeaders.Accept.Clear();
                    cons.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //var res = cons.GetAsync("RefreshConfiguration/Server");
                    var res = cons.GetAsync(System.Configuration.ConfigurationManager.AppSettings["NotificationTwo"] + param + "/" + smscid + "/" + groupname);
                    res.Result.EnsureSuccessStatusCode();
                    if (res.Result.IsSuccessStatusCode)
                    {
                        resString = res.Result.Content.ReadAsStringAsync().Result;
                        //Console.WriteLine(resString);
                        //Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Logger.InfoFormat("Core.Data :: ThirdPartyServiceConsumption :: AdminNotificationCallTwo :: {0}",
                    ex.ToString());
                }
                return resString;

                # region Commented

            //// var retVal;
            ////try
            ////{
            ////    var url = string.Format(System.Configuration.ConfigurationManager.AppSettings["AdminNotificationUri"] + "?Type={0}&smscid={1}", param, smscid);
            ////    var myRequest = WebRequest.Create(url);
            ////    var myWebResponse = myRequest.GetResponse();
            ////    var strResponseCode = (int)((HttpWebResponse)myWebResponse).StatusCode;
            ////    var myStream = myWebResponse.GetResponseStream();
            ////    if (myStream != null)
            ////    {
            ////        var sr = new StreamReader(myStream);
            ////        var readBuff = new Char[257];
            ////        var count = sr.Read(readBuff, 0, 256);
            ////        while (count > 0)
            ////        {
            ////            var str = new String(readBuff, 0, count);
            ////            retVal = retVal + str;
            ////            count = sr.Read(readBuff, 0, 256);
            ////        }
            ////        sr.Close();
            ////        myStream.Close();
            ////        myStream.Dispose();
            ////    }
            ////    myWebResponse.Close();
            ////    myWebResponse.Dispose();
            ////}
            ////catch (Exception ex)
            ////{
            ////    Logger.InfoFormat("Core.Data :: ThirdPartyServiceConsumption :: AdminNotificationCallTwo :: {0}", ex.ToString());
            ////}
           ////  return retVal;

                # endregion
            
        }

        
        /// <summary>
        /// Not in Use.
        /// </summary>
        /// <returns></returns>
        public object GetThirdPartyServiceInfo()
        {
            var ThirdPartyUri = "";
            var ThirdPartyOperationname = "";
            object[] ThirdPartyParameters;

            var oResult = "";
            ThirdPartyUri = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyService"];
            ThirdPartyOperationname = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyOperationName"];
            ThirdPartyParameters = new object[] { 
                System.Configuration.ConfigurationManager.AppSettings["ThirdPartyParam"] 
            };
            oResult = ThirdPartyServiceCall(ThirdPartyUri, ThirdPartyOperationname, ThirdPartyParameters);
            return oResult;
        }

        /// <summary>
        /// Not in use.
        /// </summary>
        /// <param name="ThirdPartyUri"></param>
        /// <param name="operationName"></param>
        /// <param name="operationParameters"></param>
        /// <returns></returns>
        private dynamic ThirdPartyServiceCall(string ThirdPartyUri, string operationName, object[] operationParameters)
        {
            dynamic retVal = "";
            try
            {
                Uri mexAddress = new Uri(ThirdPartyUri);
                //MetadataExchangeClientMode mexMode = MetadataExchangeClientMode.HttpGet;
                //string contractName = "";
                //MetadataExchangeClient mexClient = new MetadataExchangeClient(mexAddress, mexMode);
                //mexClient.ResolveMetadataReferences = true;
                //MetadataSet metaSet = mexClient.GetMetadata();

                //WsdlImporter importer = new WsdlImporter(metaSet);
                //Collection<ContractDescription> contracts = importer.ImportAllContracts();
                //ServiceEndpointCollection allEndpoints = importer.ImportAllEndpoints();

                //ServiceContractGenerator generator = new ServiceContractGenerator();
                //var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

                //if (contracts.Count > 0)
                //    contractName = contracts[0].Name;

                //foreach (ContractDescription contract in contracts)
                //{
                //    generator.GenerateServiceContractType(contract);
                //    endpointsForContracts[contract.Name] = allEndpoints.Where(se => se.Contract.Name == contract.Name).ToList();
                //}

                //if (generator.Errors.Count != 0)
                //    return "Errors during uri code compilation.";

                //CodeGeneratorOptions options = new CodeGeneratorOptions();
                //options.BracingStyle = "C";
                //CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");

                //CompilerParameters compilerParameters = new CompilerParameters(
                //    new string[] { "System.dll", "System.ServiceModel.dll", "System.Runtime.Serialization.dll" });
                //compilerParameters.GenerateInMemory = true;

                //CompilerResults results = codeDomProvider.CompileAssemblyFromDom(compilerParameters, generator.TargetCompileUnit);

                //if (results.Errors.Count > 0)
                //    return "Errors during generated code compilation";

                //else
                //{
                //    Type clientProxyType = results.CompiledAssembly.GetTypes().FirstOrDefault(
                //        t => t.IsClass && t.GetInterface(contractName) != null &&
                //            t.GetInterface(typeof(ICommunicationObject).Name) != null);

                //    ServiceEndpoint se = endpointsForContracts[contractName].FirstOrDefault();

                //    object instance = results.CompiledAssembly.CreateInstance(clientProxyType.Name, false,
                //        System.Reflection.BindingFlags.CreateInstance, null,
                //        new object[] { se.Binding, se.Address }, CultureInfo.CurrentCulture, null);

                //    retVal = instance.GetType().GetMethod(operationName).Invoke(instance, operationParameters);
                //}
            }
            catch (Exception exp)
            {
                retVal = exp.Message; 
            }

            return retVal;
        }


    }

    public class DndPushNotificationRequest
    {
        /// <summary>
        /// Add | Remove
        /// </summary>
        public string? action { get; set; }

        /// <summary>
        /// DND | DNS | VIP
        /// </summary>
        public string? tableName { get; set; }

        /// <summary>
        /// ["xxxxxxxxxxxx","xxxxxxxxxxxx","xxxxxxxxxxxx"]
        /// </summary>
        public string?[] msisdns { get; set; }
    }
}
