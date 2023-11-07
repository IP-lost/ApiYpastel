using Microsoft.AspNetCore.Mvc;
using webDefAPI.Services;

namespace webDefAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /*[HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
        [HttpGet]
        public ActionResult<List<WeatherForecast>> GetAll() => WeatherForecastService.GetAll();
        [HttpGet("{id}")]
        public ActionResult<WeatherForecast> Get(int id)
        {
            var weather = WeatherForecastService.Get(id);
            if(weather == null)
            {
                return NotFound();
            }
            return weather;
        }

        [HttpPost]
        public IActionResult Create(WeatherForecast weather)
        {
            WeatherForecastService.Add(weather);
            return CreatedAtAction(nameof(Get), new {id = weather.Id}, weather);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,WeatherForecast weather)
        {
            if(id != weather.Id)
            {
                return BadRequest();
            }
            var existeWeather = WeatherForecastService.Get(id);
            if(existeWeather is null)
            {
                return NotFound();
            }
            WeatherForecastService.Update(weather);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var weather = WeatherForecastService.Get(id);
            if(weather is null)
            {
                return NotFound();
            }
            WeatherForecastService.Delete(id);
            return NoContent();
        }
        [HttpGet("getSummary/{summary}")]
        public ActionResult<List<WeatherForecast>> GetBySummary(string summary)
        {
            var matchingWeatherForecasts = WeatherForecastService.GetBySummary(summary);

            if (matchingWeatherForecasts.Count == 0)
            {
                return NotFound();
            }

            return matchingWeatherForecasts;
        }

    }
}