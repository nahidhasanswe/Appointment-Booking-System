using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace AppointmentBooking.Core.Storage.Blob;

public class AzureBlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _defaultContainerName;
        private readonly string _storageAccountBaseUrl;

        public AzureBlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            // Get base config, e.g., "Storage:AzureBlob:ContainerName"
            _defaultContainerName = configuration["Storage:AzureBlob:ContainerName"] ?? string.Empty;
            
            // Get the base URL for public access, e.g., "https://[accountname].blob.core.windows.net/"
            _storageAccountBaseUrl = configuration["Storage:AzureBlob:BaseUrl"] ?? string.Empty; 
        }

        private BlobContainerClient GetContainerClient(string? containerName)
        {
            // Use the provided container name or fall back to the default
            var name = containerName ?? _defaultContainerName;
            
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("A container name must be provided or configured.");
            }
            
            return _blobServiceClient.GetBlobContainerClient(name);
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string? containerName = null)
        {
            var containerClient = GetContainerClient(containerName);
            
            // 1. Ensure the container exists (optional, based on deployment strategy)
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob); // Use PublicAccessType.None for private
            
            // 2. Get a client for the specific blob
            var blobClient = containerClient.GetBlobClient(fileName);
            
            // 3. Reset stream position and upload
            fileStream.Seek(0, SeekOrigin.Begin);
            
            // UploadAsync will automatically handle large files (block blobs)
            await blobClient.UploadAsync(fileStream, true); // 'true' overwrites if the blob exists

            return fileName; // Return the key/name
        }

        public async Task<Stream?> GetFileStreamAsync(string fileName, string? containerName = null)
        {
            var containerClient = GetContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            try
            {
                // Download content and return the stream. 
                // Note: The stream returned is a network stream and should be disposed of by the consumer.
                var response = await blobClient.DownloadContentAsync();
                return response.Value.Content.ToStream();
            }
            catch (Azure.RequestFailedException e) when (e.Status == 404)
            {
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName, string? containerName = null)
        {
            var containerClient = GetContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            
            // DeleteIfExistsAsync returns true if the blob was deleted or false if it didn't exist
            return await blobClient.DeleteIfExistsAsync(); 
        }

        public string GetFileUrl(string fileName, string? containerName = null)
        {
            // This assumes the container is publicly accessible, or you might need to 
            // generate a Shared Access Signature (SAS) token for private containers.
            var container = containerName ?? _defaultContainerName;
            
            return $"{_storageAccountBaseUrl.TrimEnd('/')}/{container.TrimEnd('/')}/{fileName}";
        }
    }