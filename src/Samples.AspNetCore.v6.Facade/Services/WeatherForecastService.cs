using Samples.AspNetCore.v6.Facade.Controllers;

namespace Samples.AspNetCore.v6.Facade.Services;

public interface IWeatherForecastService
{
    WeatherForecast[] Get();

    WeatherForecast? GetByDate(DateTimeOffset date);

    void Add(WeatherForecast weatherForecast);

    void Update(WeatherForecast weatherForecast);

    void Delete(DateTimeOffset date);
}

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly List<WeatherForecast> _data;

    public WeatherForecastService()
    {
        var now = DateTime.Now;

        _data = Enumerable.Range(1, 10).Select(index =>
        {
            var temperatureC = Random.Shared.Next(-20, 55);
            return new WeatherForecast
            {
                Date = now.AddDays(index),
                TemperatureC = temperatureC,
                TemperatureF = 32 + (int)(temperatureC / 0.5556),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }).ToList();

    }

    public WeatherForecast[] Get()
    {
        return _data.ToArray();
    }

    public WeatherForecast? GetByDate(DateTimeOffset date)
    {
        return _data.FirstOrDefault(w => Math.Abs((w.Date.Date - date.Date).TotalDays) == 0);
    }

    public void Add(WeatherForecast weatherForecast)
    {
        _data.Add(weatherForecast);
    }

    public void Update(WeatherForecast weatherForecast)
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

    public void Delete(DateTimeOffset date)
    {
        var wf = GetByDate(date);
        if (wf != null)
        {
            _data.Remove(wf);
        }
    }
}