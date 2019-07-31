namespace Tempestas.Infrastructure
{
    using System.Threading.Tasks;
    using Application.Weather;
    using Common;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class OpenWeatherMapClient : BaseWeatherClient
    {
        public OpenWeatherMapClient(IOptions<ApiKeys> options) : base(options)
        {
            ApiUrl = "http://api.openweathermap.org";
        }

        protected override string ApiKey => Options.Value.OpenWeather;

        protected override string GetRequestUrl(string city)
        {
            return $"{ApiUrl}/data/2.5/weather?q={city}&{ApiKey}";
        }

        protected override async Task<CurrentWeatherInTownModel> Convert(string json)
        {
            dynamic smth = JsonConvert.DeserializeObject(json);
            string townName = smth.name;
            decimal temperature = smth.main.temp;
            decimal pressure = smth.main.pressure;
            decimal humidity = smth.main.humidity;

            return new CurrentWeatherInTownModel
            {
                TownName = townName,
                Temperature = temperature,
                Pressure = pressure,
                Humidity = humidity
            };
        }
    }
}