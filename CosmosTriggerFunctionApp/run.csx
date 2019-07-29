#r "Microsoft.Azure.Devices"

using System;
using System.Collections.Generic;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "aspricedb",
            collectionName: "ProductData",
            ConnectionStringSetting = "PriceUpdateFunction",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists=true)]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
                string price = input[0].GetPropertyValue<string>("price");
                string deviceId = input[0].GetPropertyValue<string>("deviceId");

                log.LogInformation("Price = " + price);
                ServiceClient client = ServiceClient.CreateFromConnectionString("HostName=SpatIoTHubRD01.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Ln/IGORigG03L8LChvQj71LvkdqcBSHx4AHiRRQm1QY=");
                Message message = new Message(Encoding.ASCII.GetBytes(price));
                await client.SendAsync(deviceId, message);
            }
        }
    }
}
