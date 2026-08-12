using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.Configuration;
using System.Threading.Tasks;
using Core.Models.Enums;
using Core.Models.Helpers;
using Core.Models.Dtos.CommonDtos;

namespace SynapseAPI.Controllers
{
    public class VitisRefreshConfigController : ServicesBaseController, IDisposable
    {
        private IConnection connection;
        private IConnectionFactory connectionFactory;
        private Apache.NMS.ISession session;

        private string TOPIC_NAME = System.Configuration.ConfigurationManager.AppSettings["TOPIC_NAME"];
        private string BROKER = System.Configuration.ConfigurationManager.AppSettings["BROKER"];
        private string CLIENT_ID = System.Configuration.ConfigurationManager.AppSettings["CLIENT_ID"];
        private string _filePath = AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["FILE_PATH"];
        public VitisRefreshConfigController()
        {
            if (connectionFactory == null)
            {
                connectionFactory = new ConnectionFactory(BROKER, CLIENT_ID);
                if (connection == null)
                {
                    connection = connectionFactory.CreateConnection();
                }
            }

            try
            {
                if (!connection.IsStarted)
                    connection.Start();
                session = connection.CreateSession();
            }
            catch (Exception ex)
            {
                var exp = ex;
                Logger.Info(exp);
            }
        }

        public void Dispose()
        {
            //if (disposed) return;
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
            if (connectionFactory != null)
                connectionFactory = null;
            if (session != null)
                session.Dispose();
        }

        [HttpPost]
        [Route("RefreshConfiguration")]
        public async Task<IActionResult> RefreshConfiguration(ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.type))
            {
                return BadRequest("Invalid request");
            }

            var response = await _contextSynapseCore.BuildConfigurationData(request.type);
            return Ok(response);
        }


        [HttpPost]
        [Route("RefreshConfigurationws")]
        //public async Task<IActionResult> RefreshConfigurationws(string type, string smscid = null, string groupname = null)
        public async Task<IActionResult> RefreshConfigurationws(ReUsableRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.type))
            {
                return BadRequest("Invalid request");
            }
            if (request.type.Equals(GsmData.Bind, StringComparison.OrdinalIgnoreCase) ||
                request.type.Equals(GsmData.Unbind, StringComparison.OrdinalIgnoreCase) || request.type.Equals(GsmData.Restart, StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(request.smscid))
                {
                    return BadRequest("SMSC ID should not be null or Empty.");
                }
            }
            var response = await _contextSynapseCore.BuildConfigurationData(request.type, request.smscid, request.groupname);
            return Ok(response);
        }
    }


}
