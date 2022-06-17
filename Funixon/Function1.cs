using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Funixon
{
/*    public static class FunctionTopic
    {
        [FunctionName("FunctionTopic")]
        public static void Run([ServiceBusTrigger("messagecreated", "secondfunction", Connection = "SBConnection")]string mySbMsg, ILogger logger)
        {
            logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }*/
    public static class FunctionQueue
    {
        [FunctionName("FunctionQueue")]
        public static void Run([ServiceBusTrigger("bus-stop", Connection = "BSConnection")] string mySbMsg, ILogger logger)
        {
            logger.LogInformation($"BS Queue Trigger {mySbMsg}");
        }
    }
}
