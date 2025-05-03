using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.Service;

namespace InventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("low-stock")]
        public IActionResult GetLowStockReport()
        {
            var report = _reportService.GenerateLowStockReport();
            return Ok(report);
        }

        [HttpGet("transactions")]
        public IActionResult GetTransactionHistory([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? transactionType)
        {
            var report = _reportService.GetTransactionHistory(fromDate, toDate, transactionType);
            return Ok(report);
        }
    }
}
