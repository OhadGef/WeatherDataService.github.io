using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using il.ac.shenkar.weatherProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace il.ac.shenkar.weatherTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Testing GetWeatherData(Location) method with empty value in country
        /// Expected exception so we assert fail 
        /// </summary>
        /// 
        [TestMethod]
        public void GetWeatherDataEmptyValueTest()
        {
            IWeatherDataService service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.ServiceType.WORLD_WEATHER_ONLINE);
            Location location = new Location();
            location.cityName = "Paris";
            location.countryName = "";
            try
            {
                WeatherData weatherDataToTest = service.GetWeatherData(location);
                Assert.Fail("Expected exception");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Testing GetWeatherData(Location) method with null value in city and country
        /// Expected exception so we assert fail 
        /// </summary>
        /// 
        [TestMethod]
        public void GetWeatherDataNullValueTest()
        {
            IWeatherDataService service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.ServiceType.WORLD_WEATHER_ONLINE);
            Location location = new Location();
            location.cityName = null;
            location.countryName = null;
            try
            {
                WeatherData weatherDataToTest = service.GetWeatherData(location);
                Assert.Fail("Expected exception");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Testing GetWeatherData(Location) method with null value in location
        /// Expected exception so we assert fail 
        /// </summary>
        [TestMethod()]
        public void GetWeatherDataNullLocationTest()
        {
            IWeatherDataService service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.ServiceType.WORLD_WEATHER_ONLINE);
            try
            {
                WeatherData weatherDataToTest = service.GetWeatherData(null);
                Assert.Fail("Expected exception");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Testing GetWeatherData(Location) method with not exist city X or country Y
        /// Expected exception so we assert fail 
        /// </summary>
        /// 
        [TestMethod]
        public void GetWeatherDataNotExistTest()
        {
            IWeatherDataService service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.ServiceType.WORLD_WEATHER_ONLINE);
            Location location = new Location();
            location.cityName = ";";
            location.countryName =";";
            try
            {
                WeatherData weatherDataToTest = service.GetWeatherData(location);
                Assert.Fail("Expected exception");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Testing GetWeatherData(Location) method in London,UK
        /// Expected same data so the assert will be true
        /// </summary>
        [TestMethod]
        public void GetWeatherDataAfterParse()
        {
            // get the weather data in Paris using the GetWeatherData function
            Location locationTest = new Location();
            locationTest.cityName = "Paris";
            locationTest.countryName = "fr";
            IWeatherDataService service = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.ServiceType.OPEN_WEATHER_MAP);
            WeatherData weatherDataTest = service.GetWeatherData(locationTest);
            // get the weather data in Paris from the website
            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + locationTest.cityName + "," + locationTest.countryName + "&appid=e32747cd0d056bdc97e6361f9d1fa53f" + "&mode=xml";
            WeatherData weatherSrc = ParseXDocToWDStructure(url);
            Assert.IsTrue(weatherSrc.Equals(weatherDataTest));
        }
        private WeatherData ParseXDocToWDStructure(string url)
        {
            XDocument doc = null;
            WeatherData weatherData = new WeatherData();
            doc = XDocument.Load(url);
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
                weatherData.cityName = item.Name;
                weatherData.temp = (double.Parse(item.Temp) - 273.15); // Convert from Kelvin to Celcius
                weatherData.pressure = int.Parse(item.Pressure);
                weatherData.humidity = item.Humidity + "%";
                weatherData.windSpeed = double.Parse(item.WindSpeed) * 3.6; //Convert Wind Speed
                weatherData.windDirection = item.WindDirection;
                weatherData.lastupdate = Convert.ToDateTime(item.lastupdate);
                weatherData.webSite = "http://www.openweathermap.org/";
            }
            return weatherData;
        }

    }
}
