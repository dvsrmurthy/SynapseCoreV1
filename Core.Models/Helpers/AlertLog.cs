using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Core.Models.Helpers
{
    public class AlertLog : IDisposable
    {        
        public async Task PushMessageToQ(string type, string smscId)
        {
            try
            {
                var factory = new ConnectionFactory(System.Configuration.ConfigurationManager.AppSettings["ACTMQ_CONN"]);
                using (IConnection connection = factory.CreateConnection())
                {
                    using (ISession session = connection.CreateSession())
                    {
                        using (IMessageProducer producer = session.CreateProducer(new ActiveMQQueue(System.Configuration.ConfigurationManager.AppSettings["QUEUE_ALERTS"])))
                        {
                            connection.Start();
                            ITextMessage request =
                                session.CreateTextMessage("<esme id='" + smscId +
                                                          "'><type>alert</type><systemid></systemid><action>" +
                                                          type.ToLower() + "</action><time>" +
                                                          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff tt") +
                                                          "</time></esme>");
                            producer.Send(request,
                                (System.Configuration.ConfigurationManager.AppSettings["QUEUE_PERSISTENT"].Equals("Y",
                                    StringComparison.OrdinalIgnoreCase)
                                    ? MsgDeliveryMode.Persistent
                                    : MsgDeliveryMode.NonPersistent), MsgPriority.High, TimeSpan.MinValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        }

        public void Dispose()
        {

        }
    }
}
