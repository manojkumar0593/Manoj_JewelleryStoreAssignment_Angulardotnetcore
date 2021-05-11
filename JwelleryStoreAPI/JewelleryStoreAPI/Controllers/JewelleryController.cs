using Jewellery.ReportHelper.IReportServices;
using JewelleryStore.DataAccess;
using JewelleryStoreAPI.Helpers;
using JewelleryStoreAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JewelleryStoreAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class JewelleryController : ControllerBase
    {

        private readonly IUnitOfWork uow;
        private readonly IReportService _reportService;

        public JewelleryController(IUnitOfWork uow, IReportService reportService)
        {
            this.uow = uow;
            _reportService = reportService;
        }


        [HttpGet]
        [Route("GetJewelPrice")]
        public IActionResult GetPrice(string jewel)
        {
            var result = uow.JewelleryRepository.GetPrice(jewel);
            return Ok(result);
        }

        [HttpPost]
        [Route("DownloadSummaryPdf")]
        public IActionResult DownloadSummaryPdf(PdfContentViewModel content)
        {

           var htmlString = CommonHelper.GetHTMLString(content);

            var pdfFile = _reportService.GeneratePdfReport(htmlString);

             
            return File(pdfFile,
            "application/octet-stream", "JewelleryStorePdf.pdf");
        }

        [HttpPost]
        [Route("PrintReport")]
        public IActionResult PrintToPaper(PdfContentViewModel content)
        {
            try
            {
                var htmlString = CommonHelper.GetHTMLString(content);

                var responseData = _reportService.PrintToPaper(htmlString);
                return Ok(responseData);
            }
            catch (NotImplementedException)
            {
                return NotFound("This feature is not yet implemented and will be introduced in later release.");
            }

        }
        
        //// GET api/<JewelleryController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        //// POST api/<JewelleryController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<JewelleryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<JewelleryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
