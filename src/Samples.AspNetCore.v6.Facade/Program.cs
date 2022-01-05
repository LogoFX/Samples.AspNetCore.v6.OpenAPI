using Samples.AspNetCore.v6.Facade.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("AllowAny", cp =>
{
    cp
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

// Add services to the container.
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAny");

app.UseAuthorization();

app.MapControllers();

app.Run();
