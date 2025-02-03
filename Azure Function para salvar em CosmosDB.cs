using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

public static class SaveToCosmosDB
{
    [FunctionName("SaveToCosmosDB")]
    public static async Task Run(
        [QueueTrigger("myQueue", Connection = "AzureWebJobsStorage")] string myQueueItem,
        ILogger log)
    {
        var cosmosClient = new DocumentClient(new Uri("CosmosDbUri"), "CosmosDbKey");
        var databaseUri = UriFactory.CreateDatabaseUri("YourDatabaseName");
        var collectionUri = UriFactory.CreateDocumentCollectionUri("YourDatabaseName", "YourCollectionName");

        var document = new { Id = Guid.NewGuid(), Data = myQueueItem };
        await cosmosClient.CreateDocumentAsync(collectionUri, document);
        
        log.LogInformation($"Document saved to CosmosDB: {JsonConvert.SerializeObject(document)}");
    }
}
