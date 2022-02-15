using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfApiCore.Service;

namespace PdfApiCore
{
    [Route("api/[controller]")]
    [ApiController]
    public class MergeController : ControllerBase
    {
        private readonly IPdfService pdfService;

        public MergeController(IPdfService pdfService)
        {
            this.pdfService = pdfService;
        }

        [HttpPost(Name = "api/v2/MergePdf")]
        public async Task<ActionResult> MergePdf()
        {
            await pdfService.MergedPdf();
            return Ok();
        }
    }
}
