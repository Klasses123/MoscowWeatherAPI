using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MoscowWeatherAPI;
using MoscowWeatherAPI.Interfaces;
using MoscowWeatherAPI.Repositories;
using MoscowWeatherAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJsonReader, JsonReader>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();

builder.Services.AddDbContext<DbMainContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WeatherMoscowDb"),
                    b => b.MigrationsAssembly("MoscowWeatherAPI")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions 
{ 
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"ExcelFiles")),
    RequestPath = new PathString("/ExcelFiles")
});

app.Run();
