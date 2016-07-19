namespace il.ac.shenkar.weatherProject
{
    public interface IWeatherDataService
    {
        WeatherData GetWeatherData(Location location);
    }
}
