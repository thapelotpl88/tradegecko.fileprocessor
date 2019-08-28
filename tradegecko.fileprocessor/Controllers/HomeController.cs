using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tradegecko.fileprocessor.Domain.Services;
using tradegecko.fileprocessor.Models;

namespace tradegecko.fileprocessor.Controllers
{
	public class HomeController : Controller
	{
		private readonly IFileService _fileService;

		public HomeController(IFileService fileService)
		{
			_fileService = fileService;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult FileUpload()
		{
			return View();
		}

		public IActionResult SearchObject()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		public async Task<IActionResult> FileUploadSave(List<IFormFile> files)
		{
			var filePaths = new List<string>();

			var formFile = files[0];
			if (formFile.Length > 0)
			{
				using (var stream = formFile.OpenReadStream())
				{
					await _fileService.UploadFileAsync(stream, formFile.FileName);
				}
			}

			return Ok(new { count = files.Count, filePaths });
		}
	}
}
