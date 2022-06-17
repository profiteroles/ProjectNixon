using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FuncBus
{
    public static class FunctionTopic
    {//Update service bus 5.2 and above to use __fullyQualifiedNamespace, 
        [FunctionName("FunctionTopic")]
        public static void Run([ServiceBusTrigger("messagecreated","firstfunction", Connection = "BSConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus TOPIC trigger message: {myQueueItem}");
        }
    }
    public static class FunctionQueue
    { 
        [FunctionName("FunctionQueue")]
        public static void Run([ServiceBusTrigger("bus-stop", Connection = "BSConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"Queue trigger function processed message: {myQueueItem}");
            
        }
    }
}
