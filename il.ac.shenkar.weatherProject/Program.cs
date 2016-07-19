using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace il.ac.shenkar.weatherProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Location location = new Location();
            location.cityName = "Tel Aviv";
            location.countryName = "IL";

            IWeatherDataService service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.ServiceType.OPEN_WEATHER_MAP);
            WeatherData weatherData = service.GetWeatherData(location);
            Console.WriteLine(weatherData.ToString());

            service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.ServiceType.WORLD_WEATHER_ONLINE);
            weatherData = service.GetWeatherData(location);
            Console.WriteLine(weatherData.ToString());
        }
    }
}
