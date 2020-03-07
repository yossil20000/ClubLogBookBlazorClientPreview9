using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Shared
{
    public interface IWeatherForcast
    {
        DateTime Date { get; set; }

        int TemperatureC { get; set; }

        string Summary { get; set; }
    }
    public class WeatherForecast :IWeatherForcast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
