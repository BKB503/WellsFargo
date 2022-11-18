using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using WellsFargo.Contracts.Exceptions;
using WellsFargo.Contracts.Helpers;
using WellsFargo.Contracts.Models;
using WellsFargo.Core.Abstractions;

namespace WellsFargo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementSystemController : ControllerBase
    {
        private readonly ICsvFileHelper csvFileHelper;
        private readonly IOrderManagementSystemService orderManagementSystemService;
        private readonly ILogger<OrderManagementSystemController> logger;
        public OrderManagementSystemController(ICsvFileHelper transactionFileValidator, IOrderManagementSystemService orderManagementSystemService, 
            ILogger<OrderManagementSystemController> logger
            )
        {
            this.csvFileHelper = transactionFileValidator;
            this.orderManagementSystemService = orderManagementSystemService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("DownloadOutput")]
        [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FileStreamResult), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadOutput([Required] IFormFile file)
        {
            
            try
            {
                logger.LogInformation("DownloadOutput request triggered");
                if (file == null)
                {
                    return BadRequest("Please upload a valid csv file");
                }

                if(file.ContentType != "text/csv")
                {
                    return BadRequest("Please upload a valid csv file");
                }
                var transactions = csvFileHelper.ReadAndValidate(ConvertToByteArray(file));
                var omsTypeResponseModelList = orderManagementSystemService.CreateOmsTransactions(transactions);

                logger.LogInformation("DownloadOutput request completed");

                return CreateAndDownloadZipFile(omsTypeResponseModelList);
            }
            catch (InvalidFileException ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        private IActionResult CreateAndDownloadZipFile(List<OmsTypeResponseModel> omsTypeResponseModelList)
        {
            var zipName = $"OmsTransactions-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
            using (MemoryStream ms = new MemoryStream())
            {
                //required: using System.IO.Compression;
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var omsTypeResponseModel in omsTypeResponseModelList)
                    {
                        var entry = zip.CreateEntry($"Transaction_{omsTypeResponseModel.OmsType}.csv.{omsTypeResponseModel.FileType}");
                        using (var fileStream = new MemoryStream(csvFileHelper.ExportToCsvData(omsTypeResponseModel)))
                        using (var entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }

                }
                return File(ms.ToArray(), "application/zip", zipName);
            }
        }

        private byte[] ConvertToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    
    }

    
}
