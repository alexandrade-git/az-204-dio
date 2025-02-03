using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

public static class ListCosmosDB
{
    [FunctionName("ListCosmosDB")]
    public static async Task Run(
        [CosmosDB(
            databaseName: "YourDatabaseName", 
            collectionName: "YourCollectionName", 
            ConnectionStringSetting = "CosmosDbConnectionString")] IQueryable<dynamic> items,
        ILogger log)
    {
        var allItems = items.ToList();
        foreach (var item in allItems)
        {
            log.LogInformation($"Item: {item}");
        }
    }
}
