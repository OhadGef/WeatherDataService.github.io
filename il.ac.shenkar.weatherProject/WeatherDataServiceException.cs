using System;
using System.Runtime.Serialization;

namespace il.ac.shenkar.weatherProject
{
    [Serializable]
    internal class WeatherDataServiceException : Exception
    {
        public WeatherDataServiceException()
        {
        }

        public WeatherDataServiceException(string message) : base(message)
        {
        }

        public WeatherDataServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WeatherDataServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}