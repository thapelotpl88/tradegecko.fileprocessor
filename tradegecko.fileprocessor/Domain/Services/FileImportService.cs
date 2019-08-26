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
	public class FileImportService : IFileImportService
	{
        private IConfiguration _config;

        public FileImportService(IConfiguration config)
        {
            _config = config;
        }

        //  Upload file to Azure Blob Storage
        public async Task UploadFileAsync(Stream file, string fileFullPath)
		{
            CloudStorageAccount cloudStorage = null;
            if (CloudStorageAccount.TryParse(_config["AzureBlob:ConnectionString"], out cloudStorage))
            {
                CloudBlobClient cloudBlobClient = cloudStorage.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_config["AzureBlob:BlobContainerName"]);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference($"tradegecko/{fileFullPath}");
                await cloudBlockBlob.UploadFromStreamAsync(file);
            }
        }
	}
}
