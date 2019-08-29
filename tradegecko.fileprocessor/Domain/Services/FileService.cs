using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace tradegecko.fileprocessor.Domain.Services
{
	public class FileService : IFileService
	{
        private static string _azurePath = "tradegecko";

        private IConfiguration _config;

        public FileService(IConfiguration config)
        {
            _config = config;
        }

        public async Task DeleteFileAsync(string fileFullPath)
        {
            CloudStorageAccount cloudStorage = null;
            if (CloudStorageAccount.TryParse(_config["AzureBlob:ConnectionString"], out cloudStorage))
            {
                CloudBlobClient cloudBlobClient = cloudStorage.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_config["AzureBlob:BlobContainerName"]);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference($"{_azurePath}/{fileFullPath}");
                await cloudBlockBlob.DeleteAsync();
            }
        }

        public async Task<Stream> GetFileAsync(string fullFilePath)
        {
            CloudStorageAccount cloudStorage = null;
            var ms = new MemoryStream();
            if (CloudStorageAccount.TryParse(_config["AzureBlob:ConnectionString"], out cloudStorage))
            {
                CloudBlobClient cloudBlobClient = cloudStorage.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_config["AzureBlob:BlobContainerName"]);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference($"{_azurePath}/{fullFilePath}");
                await cloudBlockBlob.DownloadToStreamAsync(ms);
            }
            return ms;
        }

        public async Task UploadFileAsync(Stream file, string fileFullPath)
		{
            CloudStorageAccount cloudStorage = null;
            if (CloudStorageAccount.TryParse(_config["AzureBlob:ConnectionString"], out cloudStorage))
            {
                CloudBlobClient cloudBlobClient = cloudStorage.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_config["AzureBlob:BlobContainerName"]);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference($"{_azurePath}/{fileFullPath}");
                await cloudBlockBlob.UploadFromStreamAsync(file);
            }
        }
	}
}
