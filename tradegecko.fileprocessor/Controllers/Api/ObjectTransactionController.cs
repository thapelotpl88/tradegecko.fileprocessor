using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tradegecko.fileprocessor.Domain.Services;

namespace tradegecko.fileprocessor.Controllers.Api
{
    [Route("[controller]")]
    [ApiController]
    public class ObjectTransactionController : ControllerBase
    {
        private readonly IFileImportProcessor _fileImportProcessor;

        public ObjectTransactionController(IFileImportProcessor fileImportProcessor)
        {
            _fileImportProcessor = fileImportProcessor;
        }

        [HttpPost]
        public async Task<IActionResult> FileUploadSave(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    await _fileImportProcessor.ProcessObjectStateFile(stream, file.FileName);
                }
            }

            return Ok();
        }
    }
}