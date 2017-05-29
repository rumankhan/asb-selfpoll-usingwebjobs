using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Configuration;
using System.IO;

namespace ASBSelfPollUsingWebJobs
{
    public class StartUp
    {
        private static string _connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
        private static string _storageString = ConfigurationManager.AppSettings["Microsoft.WebJobs.StorageString"];
        public const string QUEUENAME = "WebJobQueue";

        static void Main(string[] args)
        {
            Microsoft.Azure.WebJobs.ServiceBus.ServiceBusConfiguration asbconfig = new Microsoft.Azure.WebJobs.ServiceBus.ServiceBusConfiguration();
            asbconfig.ConnectionString = _connectionString;

            JobHostConfiguration jobHostConfig = new JobHostConfiguration();
            jobHostConfig.UseServiceBus(asbconfig); //This indicates you will be using ServiceBusTrigger
            jobHostConfig.UseTimers(); //This indicates you will be using TimerTrigger. If this is not set, even though you use the attribute, the method will not be triggered
            jobHostConfig.StorageConnectionString = _storageString;
            jobHostConfig.DashboardConnectionString = null; //If you dont need dashboard, you need to set this as empty or null 
            
            CreateQueueIfNotExists(QUEUENAME);
            
            JobHost jobHost = new JobHost(jobHostConfig);
            jobHost.RunAndBlock();
            
        }

        private static void CreateQueueIfNotExists(string queueName)
        {
            NamespaceManager nmManager = NamespaceManager.CreateFromConnectionString(_connectionString);
            if (!nmManager.QueueExists(queueName))
            {
                Console.WriteLine("Creating queue as it doesnt exist!");
                nmManager.CreateQueue(queueName);
            }
        }
    }
}
