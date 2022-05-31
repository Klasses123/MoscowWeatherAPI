using MoscowWeatherAPI.Models;
using MoscowWeatherAPI.Responses;
using MoscowWeatherAPI.ViewModels;

namespace MoscowWeatherAPI.Services
{
    public static class WeatherExtensions
    {
        public static GetWeatherDataResponse ApplyPagination(this IQueryable<WeatherData> data, int page, int rangeCount)
        {
            var query = from w in data
                        let date = w.DateTime.Date.ToString("dd.MM.yyyy")
                        let time = w.DateTime.TimeOfDay.ToString(@"hh\:mm")
                        select new WeatherViewModel
                        {
                            Temperature = w.Temperature,
                            AtmospherePressure = w.AtmospherePressure,
                            WeatherConditions = w.WeatherConditions,
                            RelativeHumidity = w.RelativeHumidity,
                            CloudBase = w.CloudBase,
                            Cloudiness = w.Cloudiness,
                            DewPoint = w.DewPoint,
                            WindSpeed = w.WindSpeed,
                            WindDirection = w.WindDirection,
                            HorizontalVisibility = w.HorizontalVisibility,
                            Date = date,
                            Time = time
                        };
            var count = query.Count();
            var weatherData = query.Skip(rangeCount * (page - 1)).Take(rangeCount).AsEnumerable();

            return new GetWeatherDataResponse 
            { 
                Data = weatherData,
                Count = count
            };
        }
    }
}
