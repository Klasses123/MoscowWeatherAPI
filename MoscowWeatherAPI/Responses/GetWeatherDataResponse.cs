using MoscowWeatherAPI.ViewModels;

namespace MoscowWeatherAPI.Responses
{
    public class GetWeatherDataResponse
    {
        public int Count { get; set; }
        public IEnumerable<WeatherViewModel> Data { get; set; }
    }
}
