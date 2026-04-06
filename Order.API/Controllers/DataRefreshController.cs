using Microsoft.AspNetCore.Mvc;
using Order.Services.Services;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/refresh")]
    public class DataRefreshController : ControllerBase
    {
        private readonly IDataRefreshService _refreshService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DataRefreshController> _logger;

        public DataRefreshController(IDataRefreshService refreshService, IConfiguration configuration, ILogger<DataRefreshController> logger)
        {
            _refreshService = refreshService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("trigger")]
        public async Task<IActionResult> TriggerRefresh()
        {
            var csvPath = _configuration.GetValue<string>("CsvFilePath");
            if (string.IsNullOrEmpty(csvPath) || !System.IO.File.Exists(csvPath))
                return BadRequest(new { error = "CSV file path not configured or file not found" });

            _logger.LogInformation("Manual data refresh triggered");
            var result = await _refreshService.TriggerRefresh(csvPath);

            if (!result.Success)
                return StatusCode(500, result);

            return Ok(result);
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetRefreshLogs([FromQuery] int count = 10)
        {
            var result = await _refreshService.GetRefreshLogs(count);
            return Ok(result);
        }
    }
}
