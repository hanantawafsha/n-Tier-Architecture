using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTierArchitecture.BLL.Services.Utilities;
using QuestPDF.Fluent;

namespace NTierArchitecture.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet(Name = "GeneratePdf")]
        public async Task<IResult> GeneratePdf()
        {
            // Await async document creation
            var document = await _reportService.CreateDocumentAsync();

            // Generate PDF bytes
            var pdf = document.GeneratePdf();

            // Return file
            return Results.File(pdf, "application/pdf", "products.pdf");
        }


    }
}
