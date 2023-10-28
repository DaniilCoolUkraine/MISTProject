using UnityEngine;

namespace MistProject.Utils.Context
{
    public class LocationContext : ContextBase
    {
        public LocationInfo LocationInfo { get; private set; }

        public LocationContext(LocationInfo locationInfo)
        {
            LocationInfo = locationInfo;
        }
    }
}