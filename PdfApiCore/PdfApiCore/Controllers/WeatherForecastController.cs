using Microsoft.AspNetCore.Mvc;
using PdfApiCore.Service;

namespace PdfApiCore.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPdfService pdfService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPdfService pdfService)
        {
            _logger = logger;
            this.pdfService = pdfService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost(Name = "api/v1/CreatePdf")]
        public async Task<ActionResult> CreatePdf()
        {
            await pdfService.CreatePdf();
            return Ok();
        }

        
    }
}