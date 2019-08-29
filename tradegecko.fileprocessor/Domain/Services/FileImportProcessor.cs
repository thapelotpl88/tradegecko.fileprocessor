using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using tradegecko.fileprocessor.Domain.Entities;
using tradegecko.fileprocessor.Domain.Helpers;

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
            await _fileService.UploadFileAsync(stream, fileName);

            var dataStream = await _fileService.GetFileAsync(fileName);

            var entities = new List<ObjectTransaction>();
            dataStream.Seek(0, SeekOrigin.Begin);

            using (var streamReader = new StreamReader(dataStream))
            {
    
                string[] columnNames = null;
                while (!streamReader.EndOfStream)
                {
                    var strLine = await streamReader.ReadLineAsync();
                    if (columnNames == null)
                    {
                        columnNames = strLine.Split(',');
                    }
                    else
                    {
                        entities.Add(CSVDataHelper.GetObjectTransactionFromString(columnNames, CSVDataHelper.SplitCSV(strLine)));
                    }
                }
            }

            await _objectStateService.AddObjectTransactionsAsync(entities);

            await _fileService.DeleteFileAsync(fileName);
        }
    }
}
