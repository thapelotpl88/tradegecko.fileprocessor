using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace tradegecko.fileprocessor.Domain.Services
{
    public interface IFileImportProcessor
    {
        Task ProcessObjectStateFile(Stream stream, string fileName);
    }
}
