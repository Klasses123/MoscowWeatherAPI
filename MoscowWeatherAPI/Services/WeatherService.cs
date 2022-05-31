using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoscowWeatherAPI.Interfaces;
using MoscowWeatherAPI.Models;
using MoscowWeatherAPI.Responses;
using MoscowWeatherAPI.ViewModels;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using System.Net.Http.Headers;

namespace MoscowWeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        readonly IBaseRepository<WeatherData> _weatherRepository;
        readonly IJsonReader _jsonReader;
        public WeatherService(IBaseRepository<WeatherData> weatherRepository, IJsonReader reader)
        {
            _weatherRepository = weatherRepository;
            _jsonReader = reader;
        }

        public Task<GetWeatherDataResponse> GetRangeByYear(int rangeCount, int page, int year)
        {
            var res = GetMoscowWeatherData()
                .AsNoTracking()
                .Where(x => x.DateTime.Year == year)
                .ApplyPagination(page, rangeCount);

            return Task.FromResult(res);
        }

        public Task<GetWeatherDataResponse> GetRangeByMonth(int rangeCount, int page, int month)
        {
            var res = GetMoscowWeatherData()
                .AsNoTracking()
                .Where(x => x.DateTime.Month == month)
                .ApplyPagination(page, rangeCount);

            return Task.FromResult(res);
        }

        public Task<GetWeatherDataResponse> GetRangeByYearAndMonth(int rangeCount, int page, int year, int month)
        {
            var res = GetMoscowWeatherData()
                .AsNoTracking()
                .Where(x => x.DateTime.Year == year && x.DateTime.Month == month)
                .ApplyPagination(page, rangeCount);

            return Task.FromResult(res);
        }


        public Task<IEnumerable<UploadFilesResponse>> AddRangeFromFiles(IFormFileCollection files)
        {
            var folderName = Path.Combine("ExcelFiles", "Saved");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var result = new List<UploadFilesResponse>();

            foreach (var f in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(f.ContentDisposition).FileName.Trim('"');

                if (f.Length == 0)
                {
                    result.Add(new UploadFilesResponse { FileName = fileName, IsSuccess = false});
                    break;
                }

                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);

                f.CopyTo(stream);
                bool res;
                try
                {
                    res = AddRangeFromFile(stream);
                }
                catch (Exception ex)
                {
                    res = false;
                }
                finally
                {
                    File.Delete(fullPath);
                    stream.Close();
                }
                result.Add(new UploadFilesResponse { FileName = fileName, IsSuccess = res });
            }

            return Task.FromResult(result.AsEnumerable());
        }


        private bool AddRangeFromFile(FileStream stream)
        {
            stream.Position = 0;
            var wb = new XSSFWorkbook(stream);

            var weatherDataList = new List<WeatherData>();

            for (int i = 0; i < 12; i++)
            {
                var sheet = wb.GetSheetAt(i);

                for (int j = 4; j < sheet.LastRowNum; j++)
                {
                    var row = sheet.GetRow(j);

                    var date = row.Cells[0].StringCellValue;
                    var time = row.Cells[1].StringCellValue;
                    string pattern = "dd.MM.yyyy/HH:mm:ss/K";
                    var timeString = date + "/" + time + ":00/" + $"{_jsonReader.MoscowDataTimezone}";

                    DateTime.TryParseExact(timeString, pattern, null, DateTimeStyles.None, out DateTime parsedDateTime);
                    parsedDateTime = parsedDateTime.ToUniversalTime();

                    var temperature = ReadDoubleCell(row.Cells[2]);
                    var relativeHumidity = ReadDoubleCell(row.Cells[3]);
                    var dewPoint = ReadDoubleCell(row.Cells[4]);
                    var atmPressure = ReadIntCell(row.Cells[5]);
                    var windDirection = ReadStringCell(row.Cells[6]);
                    var windSpeed = ReadIntCell(row.Cells[7]);
                    int cloudiness = ReadIntCell(row.Cells[8]);
                    var cloudBase = ReadIntCell(row.Cells[9]);
                    var horizontalVisability = ReadStringCell(row.Cells[10]);

                    var weatherConditions = "";
                    if (row.Cells.Count > 11)
                    {
                        weatherConditions = ReadStringCell(row.Cells[11]);
                    }

                    var weatherData = new WeatherData
                    {
                        Id = Guid.NewGuid(),
                        DateTime = parsedDateTime,
                        Temperature = temperature,
                        RelativeHumidity = relativeHumidity,
                        DewPoint = dewPoint,
                        AtmospherePressure = atmPressure,
                        WindDirection = windDirection,
                        WindSpeed = windSpeed,
                        CloudBase = cloudBase,
                        Cloudiness = cloudiness,
                        HorizontalVisibility = horizontalVisability,
                        WeatherConditions = weatherConditions
                    };

                    weatherDataList.Add(weatherData);
                }
            }

            _weatherRepository.CreateRange(weatherDataList);
            _weatherRepository.Save();
            return true;
        }


        private IQueryable<WeatherData> GetMoscowWeatherData() => 
            from y in _weatherRepository.Get()
            let dateTime = y.DateTime.AddHours(_jsonReader.MoscowDataTimezoneHrs)
            orderby y.DateTime
            select new WeatherData
            {
                Temperature = y.Temperature,
                AtmospherePressure = y.AtmospherePressure,
                WeatherConditions = y.WeatherConditions,
                RelativeHumidity = y.RelativeHumidity,
                CloudBase = y.CloudBase,
                Cloudiness = y.Cloudiness,
                DewPoint = y.DewPoint,
                WindSpeed = y.WindSpeed,
                WindDirection = y.WindDirection,
                HorizontalVisibility = y.HorizontalVisibility,
                DateTime = dateTime,
                Id = y.Id
            };

        private int ReadIntCell(ICell cell) => 
            string.IsNullOrWhiteSpace(cell.ToString()) ? 0 : Convert.ToInt32(cell.NumericCellValue);

        private string ReadStringCell(ICell cell) 
        {
            var cellValue = cell.ToString();
            return string.IsNullOrEmpty(cellValue) ? "" : cellValue;
        }

        private double ReadDoubleCell(ICell cell) =>
            string.IsNullOrWhiteSpace(cell.ToString()) ? 0 : Convert.ToDouble(cell.NumericCellValue);

    }
}
