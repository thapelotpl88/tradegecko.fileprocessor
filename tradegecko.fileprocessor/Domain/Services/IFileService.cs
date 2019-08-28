using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace tradegecko.fileprocessor.Domain.Services
{
	public interface IFileService
	{
		Task UploadFileAsync(Stream file, string fileFullPath);
        Task DeleteFileAsync(string fileFullPath);
        Task<Stream> GetFileAsync(string fullFilePath);
    }
}
