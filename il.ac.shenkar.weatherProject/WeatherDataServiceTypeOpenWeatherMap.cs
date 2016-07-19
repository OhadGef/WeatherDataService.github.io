using System;
using System.Linq;
using System.Xml.Linq;


namespace il.ac.shenkar.weatherProject
{
    public class WeatherDataServiceTypeOpenWeatherMap:IWeatherDataService
    {
        /// <summary>
        /// Instance of the service as a Singelton.
        /// </summary>
        private static WeatherDataServiceTypeOpenWeatherMap instance;

        //constractor 
        private WeatherDataServiceTypeOpenWeatherMap() { }

        //singelton implementation
        public static WeatherDataServiceTypeOpenWeatherMap Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WeatherDataServiceTypeOpenWeatherMap();
                }
                return instance;
            }
        }

        //Address to the site and building the weatherdata sturcture
        public WeatherData GetWeatherData(Location location)
        {
            XDocument doc = null;
            WeatherData data = new WeatherData();
            string url = "";

            try
            {

                if (location.countryName == null || location.cityName == null)
                {
                    throw (new WeatherDataServiceException("WeatherDataServiceException : location.countryName or location.countryName are null"));
                }
                url = "http://api.openweathermap.org/data/2.5/weather?q=" + location.cityName + "," + location.countryName + "&appid=e32747cd0d056bdc97e6361f9d1fa53f" + "&mode=xml";
                doc = XDocument.Load(url);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                //Parsing the XML recieved by its unique stucture
                var list = from item in doc.Descendants("current")
                           select new
                           {
                               Name = item.Element("city").Attribute("name").Value,
                               Temp = item.Element("temperature").Attribute("value").Value,
                               Pressure = item.Element("pressure").Attribute("value").Value,
                               Humidity = item.Element("humidity").Attribute("value").Value,
                               WindSpeed = item.Element("wind").Element("speed").Attribute("value").Value,
                               WindDirection = item.Element("wind").Element("direction").Attribute("code").Value,
                               lastupdate = item.Element("lastupdate").Attribute("value").Value
                           };
                foreach (var item in list)
                {
                    //Building the weatherdata structure
                    data.cityName = item.Name;
                    data.temp = (double.Parse(item.Temp) - 273.15); // Convert from Kelvin to Celcius
                    data.pressure = double.Parse(item.Pressure);
                    data.humidity = item.Humidity + "%";
                    data.windSpeed = double.Parse(item.WindSpeed) * 3.6; //Convert Wind Speed
                    data.windDirection = item.WindDirection;
                    data.lastupdate = Convert.ToDateTime(item.lastupdate);

                }
                data.webSite = "http://www.openweathermap.org/";
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }

    }
}
