using System;

namespace il.ac.shenkar.weatherProject
{
    public class WeatherDataServiceFactory
    {
        public enum ServiceType { OPEN_WEATHER_MAP, WORLD_WEATHER_ONLINE };

        public static IWeatherDataService GetWeatherDataService(ServiceType type)
        {
          
            switch(type)
            {
                case ServiceType.OPEN_WEATHER_MAP:
                    return WeatherDataServiceTypeOpenWeatherMap.Instance;
                    

                case ServiceType.WORLD_WEATHER_ONLINE:
                    return WeatherDataServiceTypeWorldWeatherOnline.Instance;

                default:
                    return  null;
                  

            }
                 
        }

    }
}
