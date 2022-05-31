namespace MoscowWeatherAPI.Interfaces
{
    public interface IJsonReader
    {
        public double MoscowDataTimezoneHrs { get; }
        public string MoscowDataTimezone { get; }
    }
}
