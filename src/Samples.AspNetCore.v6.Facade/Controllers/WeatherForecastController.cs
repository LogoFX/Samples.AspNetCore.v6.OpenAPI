using Microsoft.AspNetCore.Mvc;

namespace Samples.AspNetCore.v6.Facade.Controllers
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

        private readonly List<WeatherForecast> _data;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
            _logger = logger;

            var now = DateTime.Now;

            _data = Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList();
        }

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return _data.ToArray();
		}

        [HttpGet("{date}", Name = "GetWeatherForecastByDate")]
        public WeatherForecast? GetByDate(DateTime date)
        {
            return _data.FirstOrDefault(w => Math.Abs((w.Date.Date - date.Date).TotalDays) == 0);
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public void Add([FromBody] WeatherForecast weatherForecast)
        {
            _data.Add(weatherForecast);
        }

        [HttpDelete(Name = "DeleteWeatherForecast")]
        public void Delete([FromBody] DateTime date)
        {
            var wf = GetByDate(date);
            if (wf != null)
            {
                _data.Remove(wf);
            }
        }

        [HttpPut(Name = "PutWeatherForecast")]
        public void Update([FromBody] WeatherForecast weatherForecast)
        {
            var wf = GetByDate(weatherForecast.Date);
            if (wf == null)
            {
                Add(weatherForecast);
            }
            else
            {
                var index = _data.IndexOf(wf);
                _data[index] = weatherForecast;
            }
        }
    }
}