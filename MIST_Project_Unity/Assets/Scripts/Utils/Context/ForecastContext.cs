using MistProject.UI.JsonData;

namespace MistProject.Utils.Context
{
    public class ForecastContext : ContextBase
    {
        public ForecastData ForecastData { get; private set; }

        public ForecastContext(ForecastData data)
        {
            ForecastData = data;
        }
    }
}