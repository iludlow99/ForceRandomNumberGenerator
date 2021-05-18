using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Queues;

namespace Company.Function
{
    public static class GenerateRandomNumber
    {
        [FunctionName("GenerateRandomNumber")]
        public static int Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=randomnumbergenerator7;AccountKey=Q5Gdq0nDNxTlx0of1OcmykvOhXhE6DEIWLLMlw75CA7+ntU9quIlTSLzig69E4sNnMqr/bcABPNBJZ1dN2mP9w==;EndpointSuffix=core.windows.net";
            var queueClient = new QueueClient(connectionString, "force-number");
            var peekedMessage = queueClient.PeekMessage().Value;

            int randomNumber;

            if (peekedMessage != null && peekedMessage?.Body != null)
            {
                var message = queueClient.ReceiveMessage().Value;
                int.TryParse(message.Body.ToString(), out randomNumber);
                queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
            }
            else
            {
                var rand = new Random();
                randomNumber = rand.Next(1, 11);
            }

            return randomNumber;
        }
    }
}
