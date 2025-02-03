using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

public static class SaveFileToStorage
{
    [FunctionName("SaveFileToStorage")]
    public static async Task Run(
        [BlobTrigger("uploads/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
        string name,
        [Blob("uploaded-files/{name}", FileAccess.Write, Connection = "AzureWebJobsStorage")] CloudBlockBlob outputBlob,
        ILogger log)
    {
        log.LogInformation($"Processing file: {name}");

        // Save the file to a new blob location
        await outputBlob.UploadFromStreamAsync(myBlob);
        log.LogInformation($"File saved to uploaded-files/{name}");
    }
}
