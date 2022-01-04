namespace Samples.AspNetCore.v6.Facade.Controllers
{
	public partial class WeatherForecastController : IWeatherForecastController
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

        Task<ICollection<WeatherForecast>> IWeatherForecastController.GetWeatherForecastAsync()
        {
            return Task.Run<ICollection<WeatherForecast>>(() => _data.ToArray());
        }

        Task IWeatherForecastController.PostWeatherForecastAsync(WeatherForecast body)
        {
            return Task.Run(() => { _data.Add(body); });
        }

        async Task IWeatherForecastController.DeleteWeatherForecastAsync(DateTimeOffset? body)
        {
            if (body.HasValue)
            {
                var wf = await ((IWeatherForecastController)this).GetWeatherForecastByDateAsync(body.Value);
                if (wf != null)
                {
                    _data.Remove(wf);
                }
            }
            else
            {
                _data.Clear();
            }
        }

        async Task IWeatherForecastController.PutWeatherForecastAsync(WeatherForecast body)
        {
            var wf = await ((IWeatherForecastController)this).GetWeatherForecastByDateAsync(body.Date);
            if (wf == null)
            {
                await ((IWeatherForecastController)this).PostWeatherForecastAsync(body);
            }
            else
            {
                var index = _data.IndexOf(wf);
                _data[index] = body;
            }
        }

        Task<WeatherForecast> IWeatherForecastController.GetWeatherForecastByDateAsync(DateTimeOffset date)
        {
#pragma warning disable CS8619
            return Task.Run(() => _data.FirstOrDefault(w => Math.Abs((w.Date.Date - date.Date).TotalDays) == 0));
#pragma warning restore CS8619
        }
    }
}