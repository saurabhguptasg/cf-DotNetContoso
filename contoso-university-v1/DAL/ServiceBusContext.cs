using ContosoUniversity.Models;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DAL
{
    public class ServiceBusContext
    {
        private static ServiceBusContext instance;

        private String serviceBusConnectionString = String.Empty;
        private QueueClient queueClient;

        private ServiceBusContext()
        {
            serviceBusConnectionString = CloudQueueConnection.ConnectionString;

            queueClient = QueueClient.CreateFromConnectionString(serviceBusConnectionString, "contoso");
        }

        public static ServiceBusContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceBusContext();
                }
                return instance;
            }
        }

        public void SendMessage(ContosoMessage message)
        {
            BrokeredMessage bm = new BrokeredMessage();
            bm.Properties["timestamp"] = message.MessageTimestamp;
            bm.Properties["message"] = message.MessageString;
            bm.MessageId = message.MessageID;
            queueClient.Send(bm);
        }

        public List<ContosoMessage> ReceiveMessages()
        {
            List<ContosoMessage> messageList = new List<ContosoMessage>();

            int i = 0;
            BrokeredMessage m = null;
            do
            {
                i++;
                try
                {
                    m = queueClient.Receive(TimeSpan.FromSeconds(2));
                    ContosoMessage cm = new ContosoMessage();
                    cm.MessageID = m.MessageId;
                    Stream stream = m.GetBody<Stream>();
                    StreamReader reader = new StreamReader(stream);
                    string body = reader.ReadToEnd();
                    var bodyDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<String, String>>(body);
                    cm.MessageID = bodyDict["messageId"];
                    cm.MessageTimestamp = bodyDict["timestamp"];
                    cm.MessageString = bodyDict["message"];
                    messageList.Add(cm);
                    m.Complete();
                }
                catch (Exception e)
                {
                    m = null;
                }

            } while (i < 7 && m != null);

            /*
            IEnumerable<BrokeredMessage> messages = queueClient.ReceiveBatch(10);
            foreach(BrokeredMessage m in messages)
            {
            }
            /* */
            return messageList;
        }
    }
}