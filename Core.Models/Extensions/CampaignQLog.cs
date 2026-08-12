using System;
using System.Configuration;
using Apache.NMS;
using Core.Models.Helpers;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using log4net;
using System.Net.Mail;
using System.Net;
using Core.Utilities.Helpers;
using Microsoft.Extensions.Configuration;


namespace Core.Models.Extensions
{
    public class CampaignQLog : IDisposable
    {      
        ILog _logger = LogManager.GetLogger(typeof(CampaignQLog));
        internal int recursieveCount = 0;
        public bool PushMessageToQ(string message, string qname = "")
        {
            var isValid = false;
            if(recursieveCount >= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QRecuressiveCount"]))
            {
                new QMail().sendMail(message);
                return false;
            }
            try
            {
                Logger.InfoFormat("PushMessageToQ Started");
                var factory = new ConnectionFactory(System.Configuration.ConfigurationManager.AppSettings["ACTMQ_CONN"]);
                using (IConnection connection = factory.CreateConnection())
                {
                    using (ISession session = connection.CreateSession())
                    {
                        using (
                            IMessageProducer producer =
                                session.CreateProducer(
                                    new ActiveMQQueue(!string.IsNullOrWhiteSpace(qname)
                                        ? System.Configuration.ConfigurationManager.AppSettings["EmailPushNotificationQ"]
                                        : System.Configuration.ConfigurationManager.AppSettings["QUEUE_ALERTS"])))
                        {
                            connection.Start();
                            ITextMessage request =
                                session.CreateTextMessage(message);
                            producer.Send(request,
                                (System.Configuration.ConfigurationManager.AppSettings["QUEUE_PERSISTENT"].Equals("Y",
                                    StringComparison.OrdinalIgnoreCase)
                                    ? MsgDeliveryMode.Persistent
                                    : MsgDeliveryMode.NonPersistent), MsgPriority.High, TimeSpan.MinValue);
                            isValid = true;
                        }
                    }
                }
                _logger.InfoFormat("PushMessageToQ completed");
            }
            catch (Exception ex)
            {
                if (recursieveCount >= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QRecuressiveCount"]))
                {
                    new QMail().sendMail(message);
                    return false;
                }
                recursieveCount++;
                _logger.InfoFormat("Trying to Connect Active MQ: " + System.Configuration.ConfigurationManager.AppSettings["ACTMQ_CONN"] + " - " + recursieveCount + " times");
                this.PushMessageToQ(message);
                _logger.ErrorFormat("While pushing message into Q for campaing manager, Fatal error thouging :: {0}",
                    ex.StackTrace);
            }
            return isValid;
        }

        public void Dispose()
        {

        }
    }


    public class QMail
    {
        private readonly IConfiguration _configuration;
        public QMail() { }
        public QMail(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void sendMail(string msg) 
        {
            try
            {
                var tMail = System.Configuration.ConfigurationManager.AppSettings["ToMailQ"];
                var fMail = System.Configuration.ConfigurationManager.AppSettings["FromMailQ"];
                var PMail = System.Configuration.ConfigurationManager.AppSettings["PassMailQ"]?.ToString();
                MailMessage message = new MailMessage(new MailAddress(fMail, "System"), new MailAddress(tMail));
                message.Subject = System.Configuration.ConfigurationManager.AppSettings["SubjectQ"];
                message.IsBodyHtml = true;
                message.Body = System.Configuration.ConfigurationManager.AppSettings["bodyQ"] + Environment.NewLine + msg;
                SmtpClient client = new SmtpClient();
                if (fMail.Contains(".com"))
                {
                    client.EnableSsl = true;
                }
                if (fMail.Contains(".in"))
                {
                    client.EnableSsl = false;
                }
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(fMail, AppInternalEncKey.Decrypt(PMail, false));
                client.Host = System.Configuration.ConfigurationManager.AppSettings["smtpHost"];
                client.Port = !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]) ? Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]) : 0;
                client.Send(message);

                Logger.InfoFormat("Campaign notification failed Mail Successfully sent to - " + tMail);
            }
            catch (Exception ex)
            {
                Logger.InfoFormat("Exception " + ex.Message);
            }
        }
    }
}
