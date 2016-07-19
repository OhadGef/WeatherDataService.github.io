using System;
using System.Linq;
using System.Xml.Linq;

namespace il.ac.shenkar.weatherProject
{
    public class WeatherDataServiceTypeWorldWeatherOnline : IWeatherDataService
    {
        //sigleton instance
        private static WeatherDataServiceTypeWorldWeatherOnline instance;

        //constractor
        private WeatherDataServiceTypeWorldWeatherOnline() { }

        //singelton implementation
        public static WeatherDataServiceTypeWorldWeatherOnline Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WeatherDataServiceTypeWorldWeatherOnline();
                }
                return instance;
            }
        }


        //Address to the site and building the weatherdata sturcture
        public WeatherData GetWeatherData(Location location)
        {
            XDocument xdoc = null;
            WeatherData data = new WeatherData();
            string addr = "";

            try
            {
                if (location.countryName == null || location.cityName == null)
                {
                    throw (new WeatherDataServiceException("WeatherDataServiceException : location.countryName or location.countryName are null"));
                }

                addr = "http://api.worldweatheronline.com/premium/v1/weather.ashx?query=" + location.cityName + "," + location.countryName + "format=xml&key=93df2479cae048a1aa6145850161407";
                xdoc = XDocument.Load(addr);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                //Parsing the XML recieved by its unique stucture
                var list = from item in xdoc.Descendants("data")
                           select new
                           {
                               Name = item.Element("request").Element("query").Value,
                               Temp = item.Element("current_condition").Element("temp_C").Value,
                               Pressure = item.Element("current_condition").Element("pressure").Value,
                               Humidity = item.Element("current_condition").Element("humidity").Value,
                               WindSpeed = item.Element("current_condition").Element("windspeedKmph").Value,
                               WindDirection = item.Element("current_condition").Element("winddir16Point").Value,
                               time = item.Element("current_condition").Element("observation_time").Value,
                               date = item.Element("weather").Element("date").Value


                           };
                foreach (var item in list)
                {
                    //Building the weatherdata structure
                    data.cityName = item.Name;
                    data.temp = double.Parse(item.Temp);
                    data.pressure = double.Parse(item.Pressure);
                    data.humidity = item.Humidity + "%";
                    data.windSpeed = double.Parse(item.WindSpeed);
                    data.windDirection = item.WindDirection;
                    data.lastupdate = Convert.ToDateTime( item.time+ item.date);
                }
                data.webSite = "http://www.worldweatheronline.com/";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }

    }
}