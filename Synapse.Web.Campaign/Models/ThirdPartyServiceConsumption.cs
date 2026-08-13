using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Core.Models.Helpers;
using Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Synapse.Web.CampaignPlugin.Models
{
    public class ThirdPartyServiceConsumption
    {
        private readonly IConfiguration _configuration;
        public ThirdPartyServiceConsumption(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CampaignNofication(string url)
        {
            var retVal = string.Empty;
            try
            {
                var myRequest = WebRequest.Create(url);
                var myWebResponse = myRequest.GetResponse();
                var strResponseCode = (int) ((HttpWebResponse) myWebResponse).StatusCode;
                var myStream = myWebResponse.GetResponseStream();
                if (myStream != null)
                {
                    var sr = new StreamReader(myStream);
                    var readBuff = new Char[257];
                    var count = sr.Read(readBuff, 0, 256);
                    while (count > 0)
                    {
                        var str = new String(readBuff, 0, count);
                        retVal = retVal + url;
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
                Logger.InfoFormat("Campaign :: ThirdPartyServiceConsumption :: CampaignNofication :: {0}", ex.ToString());
            }
            return retVal;
        }

        public string? SmppApiCaller(string uname, string pass, string message, string mobnos, string senderId)
        {
            var retValue = string.Empty;
            var url = AppInternalEncKey.Decrypt(_configuration["smppapi"]?.ToString(), false);
            try
            {
                var cons = new HttpClient
                {
                    BaseAddress = new Uri(url)
                };
                cons.DefaultRequestHeaders.Accept.Clear();
                cons.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string request = url;// + "/" + param;
                

                var requestObject = new RRequest
                {
                    userName = uname,
                    priority = "1",
                    referenceId = "",
                    dlrUrl = "",
                    msgType = "1",
                    senderId = senderId,
                    message = message,
                    mobileNumbers = mobnos,
                    password = pass
                };
                var requestContent = JsonConvert.SerializeObject(requestObject);
                var buffer = System.Text.Encoding.UTF8.GetBytes(requestContent);
                var byteContent = new ByteArrayContent(buffer);
                var res = cons.PostAsync(url, byteContent);
                res.Result.EnsureSuccessStatusCode();

                if (res.Result.IsSuccessStatusCode)
                {
                    retValue = res.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    // if not success
                }
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("Campaign :: ThirdPartyServiceConsumption :: Smppapicaller :: {0}", ex.ToString());
            }
            return retValue;
        }
    }
}