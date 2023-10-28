using MistProject.UI;
using MistProject.UI.JsonData;

namespace MistProject.Utils.Context
{
    public class WeatherDataContext : ContextBase
    {
        public WeatherData WeatherData { get; private set; }
        
        public WeatherDataContext(WeatherData weatherData)
        {
            WeatherData = weatherData;
        }
    }
}