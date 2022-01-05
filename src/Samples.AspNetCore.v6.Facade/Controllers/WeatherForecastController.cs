using Microsoft.AspNetCore.Mvc;
using Samples.AspNetCore.v6.Facade.Services;

namespace Samples.AspNetCore.v6.Facade.Controllers;

public partial class WeatherForecastController
{
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger)
    {
        _weatherForecastService = weatherForecastService;
        _logger = logger;
    }

    private partial Task<ActionResult<ICollection<WeatherForecast>>> GetWeatherForecastImplementation(CancellationToken cancellationToken)
    {
        return Task.Run<ActionResult<ICollection<WeatherForecast>>>(() => _weatherForecastService.Get(), cancellationToken);
    }

    private partial Task<IActionResult> PostImplementation(WeatherForecast body, CancellationToken cancellationToken)
    {
        return Task.Run<IActionResult>(() =>
        {
            _weatherForecastService.Add(body);
            return Ok();
        }, cancellationToken);
    }

    private partial Task<IActionResult> PutWeatherForecastImplementation(WeatherForecast body, CancellationToken cancellationToken)
    {
        return Task.Run<IActionResult>(() =>
        {
            _weatherForecastService.Update(body);
            return Ok();
        }, cancellationToken);
    }

    private partial Task<ActionResult<WeatherForecast>> GetWeatherForecastByDateImplementation(DateTimeOffset date, CancellationToken cancellationToken)
    {
        return Task.Run<ActionResult<WeatherForecast>>(() => _weatherForecastService.GetByDate(date) ?? throw new InvalidOperationException(), cancellationToken);
    }

    private partial Task<IActionResult> DeleteImplementation(DateTimeOffset date, CancellationToken cancellationToken)
    {

        return Task.Run<IActionResult>(() =>
        {
            _weatherForecastService.Delete(date);
            return Ok();
        }, cancellationToken);
    }
}