using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

public static class FilterCosmosDB
{
    [FunctionName("FilterCosmosDB")]
    public static async Task Run(
        [CosmosDB(
            databaseName: "YourDatabaseName", 
            collectionName: "YourCollectionName", 
            ConnectionStringSetting = "CosmosDbConnectionString")] IQueryable<dynamic> items,
        ILogger log)
    {
        var filteredItems = items.Where(item => item.Data.Contains("specificValue")).ToList();
        foreach (var item in filteredItems)
        {
            log.LogInformation($"Filtered item: {item}");
        }
    }
}
