using Microsoft.AspNetCore.Mvc;
using Order.Dto.Dtos;
using Order.Services.Services;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/analysis")]
    public class SalesAnalysisController : ControllerBase
    {
        private readonly ISalesAnalysisService _analysisService;
        private readonly ILogger<SalesAnalysisController> _logger;

        public SalesAnalysisController(ISalesAnalysisService analysisService, ILogger<SalesAnalysisController> logger)
        {
            _analysisService = analysisService;
            _logger = logger;
        }

        [HttpPost("top-products")]
        public async Task<IActionResult> GetTopProducts(TopProductRequest request)
        {
            if (request.StartDate == null || request.EndDate == null)
                return BadRequest(new { error = "start_date and end_date query params are required" });
            var result = await _analysisService.GetTopNProducts(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result);
        }

        [HttpGet("top-products/by-category")]
        public async Task<IActionResult> GetTopProductsByCategory(TopProductByCategoryRequest request)
        {
            if (request.StartDate == null || request.EndDate == null || string.IsNullOrWhiteSpace(request.Category))
                return BadRequest(new { error = "start_date, end_date, and category query params are required" });

          

            var result = await _analysisService.GetTopNByCategory(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result);
        }

        [HttpPost("top-products/by-region")]
        public async Task<IActionResult> GetTopProductsByRegion(TopProductByRegionRequest request)
        {
            if (request.StartDate == null || request.EndDate == null || string.IsNullOrWhiteSpace(request.Region))
                return BadRequest(new { error = "start_date, end_date, and region query params are required" });

            var result = await _analysisService.GetTopNByRegion(request);

            if (!result.Success)
                return BadRequest(new { error = result.Error });

            return Ok(result);
        }
    }
}
