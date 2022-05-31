using MoscowWeatherAPI.Interfaces;

namespace MoscowWeatherAPI.Models
{
    public class WeatherData : IDataModel
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public double RelativeHumidity { get; set; }
        public double DewPoint { get; set; }
        public int AtmospherePressure { get; set; }
        public string? WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int Cloudiness { get; set; }
        public int CloudBase { get; set; }
        public string HorizontalVisibility { get; set; }
        public string? WeatherConditions { get; set; }
    }
}
