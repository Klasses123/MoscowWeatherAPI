using Microsoft.AspNetCore.Mvc;
using MoscowWeatherAPI.Responses;

namespace MoscowWeatherAPI.Interfaces
{
    public interface IWeatherService
    {
        Task<GetWeatherDataResponse> GetRangeByYear(int rangeCount, int page, int year);
        Task<GetWeatherDataResponse> GetRangeByMonth(int rangeCount, int page, int month);
        Task<IEnumerable<UploadFilesResponse>> AddRangeFromFiles(IFormFileCollection files);
        Task<GetWeatherDataResponse> GetRangeByYearAndMonth(int rangeCount, int page, int year, int month);
    }
}
