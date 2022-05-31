using MoscowWeatherAPI.Interfaces;
using Newtonsoft.Json;

namespace MoscowWeatherAPI
{
    public class JsonReader : IJsonReader
    {
        private readonly dynamic _data;
        public double MoscowDataTimezoneHrs { get => _data.DataTimezone.MoscowDataTimezoneHrs; }
        public string MoscowDataTimezone { get => _data.DataTimezone.MoscowDataTimezone; }
        public JsonReader()
        {
            using var r = new StreamReader("appsettings.json");
            var json = r.ReadToEnd();
            _data = JsonConvert.DeserializeObject(json);
        }
    }
}
