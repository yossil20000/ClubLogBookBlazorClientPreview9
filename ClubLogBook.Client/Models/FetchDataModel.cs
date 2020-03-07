using ClubLogBook.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClubLogBook.Client.Models
{
	public interface IFetchDataModel
	{
		WeatherDotGovForecast RealWeatherForecast { get; }
		IWeatherForcast[] WeatherForcasts { get; }
		Task RetrieveForcastAsync();
		Task RetrieveRealForecastAsync();
	}
	public class FetchDataModel : ModelBase, IFetchDataModel
	{
		private IWeatherForcast[] _weatherForcasts;
		private WeatherDotGovForecast _realWeatherForecast;
		public IWeatherForcast[] WeatherForcasts
		{
			get => _weatherForcasts;
			private set => _weatherForcasts = value;
		}

		public WeatherDotGovForecast RealWeatherForecast { get => _realWeatherForecast; private set => _realWeatherForecast = value; }

		public FetchDataModel(HttpClient http)
		{
			_http = http;
		}

		public async Task RetrieveForcastAsync()
		{
			_weatherForcasts=  await _http.GetJsonAsync<WeatherForecast[]>("api/SampleData/WeatherForecasts");
		}
		public async Task RetrieveRealForecastAsync()
		{
			_realWeatherForecast = await _http.GetJsonAsync<WeatherDotGovForecast>("https://api.weather.gov/gridpoints/ALY/59,14/forecast");
		}
	}
}
