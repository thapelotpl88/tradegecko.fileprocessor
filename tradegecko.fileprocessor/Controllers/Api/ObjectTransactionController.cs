using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tradegecko.fileprocessor.Domain.Services;

namespace tradegecko.fileprocessor.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectTransactionController : ControllerBase
    {
        private readonly IFileImportProcessor _fileImportProcessor;
        private readonly IObjectStateService _objectStateService;

        public ObjectTransactionController(IFileImportProcessor fileImportProcessor, IObjectStateService objectStateService)
        {
            _fileImportProcessor = fileImportProcessor;
            _objectStateService = objectStateService;
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
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SearchObjectState(int? objectid, string objectType, int? timestamp)
        {
            if (!objectid.HasValue || objectType == null || !timestamp.HasValue)
            {
                return BadRequest();
            }

            var results = await _objectStateService.GetObjectTransaction(obj => obj.ObjectId == objectid && obj.ObjectType == objectType && obj.Timestamp == timestamp);

            if (!results.Any())
            {
                return NotFound();
            }
            return Ok(results);
        }
    }
}