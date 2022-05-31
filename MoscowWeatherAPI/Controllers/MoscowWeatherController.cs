using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoscowWeatherAPI.Interfaces;
using MoscowWeatherAPI.Responses;

namespace MoscowWeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoscowWeatherController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IWeatherService _weatherService;

        public MoscowWeatherController(IMapper mapper, IWeatherService weatherService)
        {
            _mapper = mapper;
            _weatherService = weatherService;
        }

        [HttpGet(nameof(GetByMonth))]
        public async Task<ActionResult<GetWeatherDataResponse>> GetByMonth(
            [FromQuery(Name = "page")] int page, 
            [FromQuery(Name = "month")] int month,
            [FromQuery(Name = "pageDataCount")] int pageDataCount)
        {
            return new JsonResult(
                await _weatherService.GetRangeByMonth(pageDataCount, page, month));
        }

        [HttpGet(nameof(GetByYear))]
        public async Task<ActionResult<GetWeatherDataResponse>> GetByYear(
            [FromQuery(Name = "page")] int page,
            [FromQuery(Name = "year")] int year,
            [FromQuery(Name = "pageDataCount")] int pageDataCount)
        {
            return new JsonResult(
                await _weatherService.GetRangeByYear(pageDataCount, page, year));
        }

        [HttpGet(nameof(GetByYearAndMonth))]
        public async Task<ActionResult<GetWeatherDataResponse>> GetByYearAndMonth(
            [FromQuery(Name = "page")] int page, 
            [FromQuery(Name = "year")] int year, 
            [FromQuery(Name = "month")] int month,
            [FromQuery(Name = "pageDataCount")] int pageDataCount)
        {
            return new JsonResult(
                await _weatherService.GetRangeByYearAndMonth(pageDataCount, page, year, month));
        }

        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<ActionResult<IEnumerable<UploadFilesResponse>>> UploadExcelData()
        {
            var files = Request.Form.Files;
            return new JsonResult(
                await _weatherService.AddRangeFromFiles(files));
        }
    }
}
