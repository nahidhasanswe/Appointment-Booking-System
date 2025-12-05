using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace AppointmentBooking.Core.Storage.S3;

public class S3FileStorageService : IFileStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly string _baseUrl;

        public S3FileStorageService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["Storage:S3:BucketName"] ?? string.Empty;
            _baseUrl = configuration["Storage:S3:BaseUrl"] ?? string.Empty; // e.g., "https://{bucketName}.s3.{region}.amazonaws.com"
        }

        // Helper to prepend the container name if provided
        private string GetS3Key(string fileName, string? containerName)
        {
            // S3 uses '/' as a path separator, regardless of OS
            return containerName != null 
                ? $"{containerName.TrimEnd('/')}/{fileName.TrimStart('/')}" 
                : fileName;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string? containerName = null)
        {
            var key = GetS3Key(fileName, containerName);
            fileStream.Seek(0, SeekOrigin.Begin); // Ensure stream is at the start

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = fileStream,
                // Add any necessary metadata like ContentType here
                CannedACL = S3CannedACL.PublicRead // Set appropriate access (e.g., Private, PublicRead)
            };

            await _s3Client.PutObjectAsync(request);
            
            return key; // Return the S3 key
        }

        public async Task<Stream?> GetFileStreamAsync(string fileName, string? containerName = null)
        {
            try
            {
                var key = GetS3Key(fileName, containerName);
                var request = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                var response = await _s3Client.GetObjectAsync(request);
                
                // The stream returned by AWS SDK must be disposed by the consumer.
                // Note: The caller must be responsible for closing the stream.
                return response.ResponseStream;
            }
            catch (AmazonS3Exception e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName, string? containerName = null)
        {
            var key = GetS3Key(fileName, containerName);
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(request);
            return true; // S3 DeleteObject is idempotent; it succeeds even if the object doesn't exist.
        }
        
        public string GetFileUrl(string fileName, string? containerName = null)
        {
            var key = GetS3Key(fileName, containerName);
            // Construct the URL using the base URL and the key
            return $"{_baseUrl.TrimEnd('/')}/{key}";
        }
    }