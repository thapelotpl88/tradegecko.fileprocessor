using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace tradegecko.fileprocessor.Domain.Services
{
    public class FileImportProcessor : IFileImportProcessor
    {
        private readonly IFileService _fileService;
        private readonly IObjectStateService _objectStateService;

        public FileImportProcessor(IFileService fileService, IObjectStateService objectStateService)
        {
            _fileService = fileService;
            _objectStateService = objectStateService;
        }

        public async Task ProcessObjectStateFile(Stream stream, string fileName)
        {
            // await _fileService.UploadFileAsync(stream, fileName);
            using (TextReader tr = new StreamReader(stream))
            {
                while (tr.Peek() >= 0)
                {
                   var strLine = await tr.ReadLineAsync();
                }
            }

        }
    }
}
