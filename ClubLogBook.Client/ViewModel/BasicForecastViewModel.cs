using ClubLogBook.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubLogBook.Client.ViewModel
{
	public interface IBasicForecastViewModel
	{
		IWeatherForcast[] WeatherForecasts { get; set; }
		
		string DisplayTemperatureScaleShort { get; }
		string DisplayTemperatureScaleLong { get; }
		int DisplayTemperature(int temperature);
		void ToggleTemperatureScale();
	}
	public class BasicForecastViewModel : IBasicForecastViewModel
	{
		private IWeatherForcast[] _weatherForecasts { get; set; }
		private bool _displayFarenheit = true;
		public IWeatherForcast[] WeatherForecasts { get => _weatherForecasts; set => _weatherForecasts = value; }
		

		public string DisplayTemperatureScaleShort
		{
			get { return !_displayFarenheit ? "C" : "F"; }
		}

		public string DisplayTemperatureScaleLong
		{
			get
			{
				return !_displayFarenheit ? "Faharenheit" : "Celsius";
			}
		}

		public int DisplayTemperature(int temperature)
		{
			return !_displayFarenheit ? temperature : 32 + (int)(temperature / 0.5556);
		}

		public void ToggleTemperatureScale()
		{
			_displayFarenheit = !_displayFarenheit;
		}
	}
}
