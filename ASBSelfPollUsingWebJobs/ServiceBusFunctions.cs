using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASBSelfPollUsingWebJobs
{
    public class ServiceBusFunctions
    {
        public static void SendMessageToQueue([ServiceBus(StartUp.QUEUENAME)] out BrokeredMessage message, [TimerTrigger("00:00:10", RunOnStartup = true)] TimerInfo timerInfo,
            TextWriter log)
        {
            message = new BrokeredMessage("polling: " + DateTime.Now);
            message.Properties.Add("Ignore", "True");
            log.WriteLine("Message Sent End!");
        }

        public static void ReceiveMessageFromQueue([ServiceBusTrigger(StartUp.QUEUENAME)] BrokeredMessage receivedMessage, TextWriter log)
        {
            log.WriteLine("ServiceBus Message Receive Triggered:");
            log.WriteLine(receivedMessage.GetBody<string>());
            foreach (var item in receivedMessage.Properties)
            {
                log.WriteLine("Key:" + item.Key + " Value:" + item.Value);
            }
            log.WriteLine("Message Receive End!");
        }
    }
}
