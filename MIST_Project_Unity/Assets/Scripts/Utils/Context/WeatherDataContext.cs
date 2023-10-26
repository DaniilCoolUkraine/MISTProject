using MistProject.UI;

namespace MistProject.Utils.Context
{
    public class WeatherDataContext : ContextBase
    {
        public MainWeatherData WeatherData { get; private set; }
        
        public WeatherDataContext(MainWeatherData weatherData)
        {
            WeatherData = weatherData;
        }
    }
}