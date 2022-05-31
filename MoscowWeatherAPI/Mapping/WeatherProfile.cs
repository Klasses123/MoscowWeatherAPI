using AutoMapper;
using MoscowWeatherAPI.Models;
using MoscowWeatherAPI.ViewModels;

namespace MoscowWeatherAPI.Mapping
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherData, WeatherViewModel>()
                .ForMember(set => set.Date, opt => opt.MapFrom(src => src.DateTime.Date.ToString("dd.MM.yyyy")))
                .ForMember(set => set.Time, opt => opt.MapFrom(src => src.DateTime.TimeOfDay.ToString("HH:mm")));
        }

        
    }
}
