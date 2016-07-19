using System;
namespace il.ac.shenkar.weatherProject
{
    public class WeatherData
    {
        public string webSite { get; set; }
        public string cityName { get; set; }
        public double temp { get; set; }
        public double pressure { get; set;}
        public string humidity { get; set; }
        public double windSpeed { get; set; }
        public string windDirection { get; set; }
        public DateTime lastupdate { get; set; }



        override public string ToString()
        {
            return "\n********* Weather Data *********" +
                "\nfron url : " + webSite +
                "\nName of City : " + cityName +
                "\nTemperature : " + temp + " Celcius" +
               "\nPressure : " + pressure + " hPa"+
                "\nHumidity : " + humidity + 
                "\nWindSpeed : " + windSpeed + " km/h" +
                "\nWindDirection : " + windDirection +
                "\nLastupdate : " + lastupdate+ 
                "\n*********************************\n" ;
        }

  
        override public bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }
            // If parameter cannot be cast to WeatherData return false.
            WeatherData wd = obj as WeatherData;
            if ((System.Object)wd == null)
            {
                return false;
            }
            // Return true if the fields match:
            return AllFieldsEquals(this, wd);
        }

        private bool AllFieldsEquals(WeatherData wd1, WeatherData wd2)
        {
            if (wd1.cityName.Equals(wd2.cityName) &&
                wd1.temp == wd2.temp &&
                wd1.pressure == wd2.pressure &&
                wd1.windSpeed == wd2.windSpeed &&
                wd1.webSite.Equals(wd2.webSite) &&
                wd1.humidity.Equals(wd2.humidity) &&
                wd1.windDirection.Equals(wd2.windDirection))
                return true;
            else
                return false;
        }
    }
   
}
