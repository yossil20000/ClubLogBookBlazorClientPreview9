using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Client.Models;
using ClubLogBook.Shared;


namespace ClubLogBook.Client.ViewModel
{
	public interface IFetchDataViewModel
	{
		
		IBasicForecastViewModel BasicForecastViewModel { get;  }
		Task RetrieveForcastAsync();
		string DisplayPremiumToggleMessage { get; }
		string DisplayTemperatureScaleLong { get; }
		
		void ToggleTemperatureScale();
		Task TogglePremiumMembership();
		WeatherDotGovForecast RealWeatherForecast { get; }
	}
	public class FetchDataViewModel : IFetchDataViewModel
	{
		private WeatherDotGovForecast _realWeatherForecast;
		private IWeatherForcast[] _weatherForecasts { get; set; }
		private IFetchDataModel _fetchDataModel;
		private bool _displayFarenheit = true;
		private bool _isPremiumMember = false;
		private IBasicForecastViewModel _basicForecastViewModel;
		public WeatherDotGovForecast RealWeatherForecast
		{
			get => _realWeatherForecast;
			private set => _realWeatherForecast = value;
		}
		public IWeatherForcast[] WeatherForecasts
		{
			get => _weatherForecasts;
			set => _weatherForecasts = value;
		}
		public IBasicForecastViewModel BasicForecastViewModel
		{
			get => _basicForecastViewModel;
			private set => _basicForecastViewModel = value;
		}
		public FetchDataViewModel(IFetchDataModel fetchDataModel, IBasicForecastViewModel basicForecastViewModel)
		{
			_fetchDataModel = fetchDataModel;
			_basicForecastViewModel = basicForecastViewModel;
			System.Diagnostics.Debug.WriteLine("FetchDataViewModel Constructor Executing");
		}

		public async Task RetrieveForcastAsync()
		{
			List<IWeatherForcast> newForecast = new List<IWeatherForcast>();
			if(_isPremiumMember)
			{
				await PopulateEnhancedForecastData(newForecast);
			}
			else
			{
				await PopulateStandardForecastAsync(newForecast);
			}
			
			System.Diagnostics.Debug.WriteLine("FetchDataViewModel Forecasts Retrieved");
			_basicForecastViewModel.WeatherForecasts = newForecast.ToArray();
		}

		private async Task PopulateStandardForecastAsync(List<IWeatherForcast> newForecast)
		{
			await _fetchDataModel.RetrieveForcastAsync();
			
			foreach (IWeatherForcast forcast in _fetchDataModel.WeatherForcasts)
			{
				IWeatherForcast newForcast = new WeatherForecast();
				newForcast.Date = forcast.Date;
				newForcast.Summary = forcast.Summary;
				newForcast.TemperatureC = forcast.TemperatureC;
				newForecast.Add(newForcast);
			}
			
		}
		private async Task PopulateEnhancedForecastData(List<IWeatherForcast> newForecasts)
		{
			await _fetchDataModel.RetrieveRealForecastAsync();
			foreach (Period forecast in _fetchDataModel.RealWeatherForecast.properties.periods)
			{
				IWeatherForcast newForecast = new WeatherForecast();
				newForecast.Date = forecast.startTime;
				newForecast.Summary = forecast.shortForecast;
				newForecast.TemperatureC = (int)((forecast.temperature - 32) * 0.556);
				newForecasts.Add(newForecast);
			}
		}
		public string DisplayTemperatureScaleLong
		{
			get{
				return !_displayFarenheit ? "Faharenheit" : "Celsius";
			}
		}
		

		public void ToggleTemperatureScale()
		{
			_displayFarenheit = !_displayFarenheit;
			_basicForecastViewModel.ToggleTemperatureScale();
		}

		string IFetchDataViewModel.DisplayPremiumToggleMessage
		{
			get
			{
				
					return _isPremiumMember ? "Change to Basic" : "Change to Premium";
			}
		}

		public async Task TogglePremiumMembership()
		{
			_isPremiumMember = !_isPremiumMember;
			await RetrieveForcastAsync();
		}
	}
}
