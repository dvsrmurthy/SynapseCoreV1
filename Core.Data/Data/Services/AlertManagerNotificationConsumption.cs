using Core.Models.Helpers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Data.Services
{
    public class AlertManagerNotificationConsumption
    {
        /// <summary>
        /// Not in Use.
        /// </summary>
        /// <returns></returns>
        /// 

        public string PushAlertNotif(string userid, string alertid, string actionName)
        {
            var retVal = "";

            //    http://192.168.7.121:8888/service?action=add&userid=124&alertid=334
            var url = string.Format(System.Configuration.ConfigurationManager.AppSettings["AlertNotificationURL"] + "?action={0}&userid={1}&alertid={2}", actionName, userid, alertid);

            Logger.InfoFormat("Alert URL: {0}", url);
            try
            {
                var myRequest = WebRequest.Create(url);
                var myWebResponse = myRequest.GetResponse();
                var strResponseCode = (int)((HttpWebResponse)myWebResponse).StatusCode;
                var myStream = myWebResponse.GetResponseStream();
                if (myStream != null)
                {
                    var sr = new StreamReader(myStream);
                    var readBuff = new Char[257];
                    var count = sr.Read(readBuff, 0, 256);
                    while (count > 0)
                    {
                        var str = new String(readBuff, 0, count);
                        retVal = retVal + str;
                        count = sr.Read(readBuff, 0, 256);
                    }
                    sr.Close();
                    myStream.Close();
                    myStream.Dispose();
                }
                myWebResponse.Close();
                myWebResponse.Dispose();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Core.Data :: PushAlertNotif :: PushAlertNotif :: {0}", ex.ToString());
            }
            return retVal;

        }

        public object GetThirdPartyServiceInfo()
        {
            var ThirdPartyUri = "";
            var ThirdPartyOperationname = "";
            object[] ThirdPartyParameters;

            var oResult = "";
            ThirdPartyUri = System.Configuration.ConfigurationManager.AppSettings["AlertNotificationURL"];
            ThirdPartyOperationname = System.Configuration.ConfigurationManager.AppSettings["AlertNotificationOperationName"];
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

    }//class
}
