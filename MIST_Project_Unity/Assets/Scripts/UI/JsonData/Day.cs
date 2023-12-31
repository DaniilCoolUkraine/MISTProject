﻿namespace MistProject.UI.JsonData
{
    public class Day
    {
        public float maxtemp_c { get; set; }
        public float maxtemp_f { get; set; }

        public float mintemp_c { get; set; }
        public float mintemp_f { get; set; }

        public float avgtemp_c { get; set; }
        public float avgtemp_f { get; set; }

        public float maxwind_mph { get; set; }
        public float maxwind_kph { get; set; }

        public float totalprecip_mm { get; set; }
        public float totalprecip_in { get; set; }

        public float totalsnow_cm { get; set; }

        public float avgvis_km { get; set; }
        public float avgvis_miles { get; set; }

        public float avghumidity { get; set; }

        public int daily_will_it_rain { get; set; }
        public int daily_chance_of_rain { get; set; }

        public int daily_will_it_snow { get; set; }
        public int daily_chance_of_snow { get; set; }

        public Condition condition { get; set; }
        public float uv { get; set; }
    }
}