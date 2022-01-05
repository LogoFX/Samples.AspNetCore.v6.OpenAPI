namespace Samples.AspNetCore.v6.Facade.Controllers;

public partial class WeatherForecastController
{
    private static readonly string[] Summaries = {
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

    private partial Task<ICollection<WeatherForecast>> GetWeatherForecastImplementation()
    {
        return Task.Run<ICollection<WeatherForecast>>(() => _data.ToArray());
    }

    private partial Task PostImplementation(WeatherForecast body)
    {
        return Task.Run(() => { _data.Add(body); });
    }

    private async partial Task PutWeatherForecastImplementation(WeatherForecast body)
    {
        var wf = await GetWeatherForecastByDateImplementation(body.Date);
        if (wf == null)
        {
            await PostImplementation(body);
        }
        else
        {
            var index = _data.IndexOf(wf);
            _data[index] = body;
        }
    }

    private partial Task<WeatherForecast> GetWeatherForecastByDateImplementation(DateTimeOffset date)
    {
#pragma warning disable CS8619
        return Task.Run(() => _data.FirstOrDefault(w => Math.Abs((w.Date.Date - date.Date).TotalDays) == 0));
#pragma warning restore CS8619
    }

    private async partial Task DeleteImplementation(DateTimeOffset date)
    {
        var wf = await GetWeatherForecastByDateImplementation(date);
        if (wf != null)
        {
            _data.Remove(wf);
        }
    }
}