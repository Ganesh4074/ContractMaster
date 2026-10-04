using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ContractMaster.Services.Interfaces;

namespace ContractMaster.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService()
    {
        var connectionString =
            Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

        var containerName =
            Environment.GetEnvironmentVariable(
                "AZURE_STORAGE_CONTAINER_NAME");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Azure Storage connection string is not configured.");
        }

        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new InvalidOperationException(
                "Azure Storage container name is not configured.");
        }

        var blobServiceClient =
            new BlobServiceClient(connectionString);

        _containerClient =
            blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<string> UploadPdfAsync(
        byte[] pdf,
        string contractId,
        int version)
    {
        await _containerClient.CreateIfNotExistsAsync();

        var blobName = $"contracts/{contractId}/v{version}.pdf";

        var blobClient =
            _containerClient.GetBlobClient(blobName);

        using var stream = new MemoryStream(pdf);

        await blobClient.UploadAsync(
            stream,
            new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "application/pdf"
                }
            });

        return blobClient.Uri.ToString();
    }
}